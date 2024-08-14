// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

using NatsunekoLaboratory.UStyled.Compiler;
using NatsunekoLaboratory.UStyled.Configurations;
using NatsunekoLaboratory.UStyled.Rules.Interfaces;

using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;

namespace NatsunekoLaboratory.UStyled
{
    /// <summary>
    ///     UStyled is a framework for building UIElements UIs.
    ///     This library provides the utility-first USS framework.
    /// </summary>
    public class UStyledCompiler
    {
        private static readonly Regex Splitter = new(@"[^\s""']+|""([^""]*)""|'([^']*)'", RegexOptions.Compiled);

        private readonly ConfigurationProvider _configuration;
        private readonly ClassContainer _container;
        private readonly UStyledPreprocessor _preprocessor;
        private readonly IReadOnlyCollection<IRule> _rules;

        internal UStyledCompiler(ConfigurationProvider configuration, UStyledPreprocessor preprocessor, IReadOnlyCollection<IRule> rules)
        {
            _configuration = configuration;
            _preprocessor = preprocessor;
            _rules = rules;

            _container = new ClassContainer();
        }

        public (VisualTreeAsset VisualTree, StyleSheet StyleSheet) CompileAsAsset(VisualTreeAsset asset, CompileMode mode = CompileMode.Jit)
        {
            var (uxml, uss) = CompileAsString(asset, mode);

            return (
                CompileVisualTreeFromString(uxml),
                CompileStyleSheetFromString(uss)
            );
        }

        private static VisualTreeAsset CompileVisualTreeFromString(string str)
        {
            try
            {
                var t = typeof(UxmlNamespacePrefixAttribute).Assembly.GetType("UnityEditor.UIElements.UXMLImporterImpl");
                var i = Activator.CreateInstance(t, true);
                var m = t.GetMethod("ImportXmlFromString", BindingFlags.NonPublic | BindingFlags.Instance);
                var args = new object[] { str, null };

                m?.Invoke(i, args);

                var asset = args[1] as VisualTreeAsset;

                if (asset != null)
                {
                    asset.hideFlags = HideFlags.NotEditable;
                    asset.name = $"UStyled-TransformedMarkups-{CalcHashString(str)}.uxml";
                }

                return asset;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return ScriptableObject.CreateInstance<VisualTreeAsset>();
        }

        private static StyleSheet CompileStyleSheetFromString(string str)
        {
            var asset = ScriptableObject.CreateInstance<StyleSheet>();
            asset.hideFlags = HideFlags.NotEditable;
            asset.name = $"UStyled-CompiledStyles-{CalcHashString(str)}.uss";

            try
            {
                var t = typeof(AssetDatabase).Assembly.GetType("UnityEditor.UIElements.StyleSheets.StyleSheetImporterImpl");
                var i = Activator.CreateInstance(t);
                var m = t.GetMethod("Import", BindingFlags.Public | BindingFlags.Instance);
                m?.Invoke(i, new object[] { asset, str });
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return asset;
        }


        public (string VisualTree, string StyleSheet) CompileAsString(VisualTreeAsset asset, CompileMode mode = CompileMode.Jit)
        {
            switch (mode)
            {
                case CompileMode.Static:
                    return CompileWithStaticMode(asset);

                case CompileMode.Jit:
                    return CompileWithJitMode(asset);

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        private (string VisualTree, string StyleSheet) CompileWithStaticMode(VisualTreeAsset asset)
        {
            _container.Clear();

            foreach (var rule in _rules.Where(w => w is IStaticRule).Cast<IStaticRule>())
                rule.Apply(_configuration, _container, "");

            var content = GetVisualTreeContent(asset);
            return (content, _container.ToString());
        }

        private (string VisualTree, string StyleSheet) CompileWithJitMode(VisualTreeAsset asset)
        {
            _container.Clear();

            var selectors = GetClassNamesFromVisualTreeAsset(asset);

            foreach (var selector in selectors)
            {
                var rule = _rules.FirstOrDefault(w => w.IsMatchToSelector(selector));
                if (rule == null)
                    continue;

                rule.Apply(_configuration, _container, selector);
            }


            var content = _container.TransformHtml(GetVisualTreeContent(asset));
            return (content, _container.ToString());
        }

        private List<string> GetClassNamesFromVisualTreeAsset(VisualTreeAsset asset)
        {
            switch (_preprocessor)
            {
                case UStyledPreprocessor.SerializedValue:
                    return GetClassNamesFromVisualTreeAssetString(asset);

                case UStyledPreprocessor.DeserializedValue:
                    return GetClassNamesFromVisualTreeAssetObject(asset);

                default:
                    return new List<string>();
            }
        }

        private static List<string> GetClassNamesFromVisualTreeAssetObject(VisualTreeAsset asset)
        {
            var classes = new List<string>();

            var getter = typeof(VisualTreeAsset).GetProperty("visualElementAssets", BindingFlags.Instance | BindingFlags.NonPublic);

            if (getter?.GetValue(asset) is IList values)
            {
                var c = values[0].GetType().GetProperty("classes", BindingFlags.Instance | BindingFlags.Public);

                foreach (var value in values)
                    classes.AddRange(c?.GetValue(value) as string[] ?? Array.Empty<string>());
            }

            return classes;
        }

        private static List<string> GetClassNamesFromVisualTreeAssetString(VisualTreeAsset asset)
        {
            try
            {
                var content = GetVisualTreeContent(asset);
                var document = XDocument.Parse(content);
                var values = document.Descendants()
                                     .Where(w => w.Attribute("class") != null && !string.IsNullOrWhiteSpace((string)w.Attribute("class")))
                                     .Select(w => w.Attribute("class")!.Value)
                                     .ToList();

                return values.SelectMany(w => Splitter.Matches(w).Select(m => m.Value)).Distinct().ToList().ToList();
            }
            catch (Exception e)
            {
                // ignored
            }

            return new List<string>();
        }

        private static string GetVisualTreeContent(VisualTreeAsset asset)
        {
            var path = AssetDatabase.GetAssetPath(asset);

            using var sr = new StreamReader(path);
            var content = sr.ReadToEnd();

            return content;
        }

        private static string ToKebabCase(string value)
        {
            return Regex.Replace(value, "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z0-9])", "-$1", RegexOptions.Compiled)
                        .Trim()
                        .ToLower();
        }

        private static string CalcHashString(string str)
        {
            var md5 = new MD5CryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(str);
            var hash = md5.ComputeHash(bytes);
            return BitConverter.ToString(hash).ToLowerInvariant().Replace("-", "");
        }
    }
}