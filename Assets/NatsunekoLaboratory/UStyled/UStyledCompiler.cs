// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

using JetBrains.Annotations;

using NatsunekoLaboratory.UStyled.Configurations;
using NatsunekoLaboratory.UStyled.Rules;

using UnityEditor;

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
        private readonly ConfigurationProvider _configuration;
        private readonly Dictionary<Regex, IRule> _patterns;
        private readonly Dictionary<string, string> _rules;

        public UStyledCompiler()
        {
            _rules = new Dictionary<string, string>();
            _patterns = new Dictionary<Regex, IRule>();
        }

        private UStyledCompiler(ConfigurationProvider configuration, Dictionary<string, string> rules, Dictionary<Regex, IRule> patterns)
        {
            _configuration = configuration;
            _rules = rules;
            _patterns = patterns;
        }

        public UStyledCompiler DefineConfig(ConfigurationProvider configuration)
        {
            foreach (var rule in configuration.GetStaticRules())
            {
                if (_rules.ContainsKey(rule.Key))
                    continue;

                _rules.Add(rule.Key, CompileRule(rule.Value));
            }

            foreach (var rule in configuration.GetDynamicRules())
            {
                if (_patterns.ContainsKey(rule.Key))
                    continue;

                _patterns.Add(rule.Key, rule.Value);
            }

            return new UStyledCompiler(configuration, _rules, _patterns);
        }

        public string Compile(VisualTreeAsset asset)
        {
            return CompileAsString(asset);
        }

        public StyleSheet JitCompile(VisualTreeAsset asset)
        {
            return CompileStyleSheetFromString(CompileAsString(asset));
        }

        private string CompileAsString(VisualTreeAsset asset)
        {
            try
            {
                var selectors = GetVisualElementAssets(asset);
                var sb = new StringBuilder();

                bool SetStaticSelector(string selector)
                {
                    if (_rules.TryGetValue(selector, out var val))
                    {
                        sb.AppendLine($".{selector} {{ {val} }}");
                        return true;
                    }

                    return false;
                }

                bool SetDynamicSelector(string selector)
                {
                    foreach (var pattern in _patterns)
                    {
                        var m = pattern.Key.Match(selector);
                        if (m.Success)
                        {
                            var r = CompileDynamicRule(selector, m, pattern.Value, out var val);
                            if (r)
                            {
                                sb.AppendLine(val);
                                return true;
                            }
                        }
                    }

                    return false;
                }

                foreach (var selector in selectors.Distinct())
                {
                    var r = SetStaticSelector(selector) || SetDynamicSelector(selector);
                    if (!r) Debug.LogWarning($"Unknown Selector: {selector}");
                }

                return sb.ToString();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return string.Empty;
            }
        }

        private static string CompileRule(IRule rule, [CanBeNull] Func<string, string> converter = null)
        {
            string PassThrough(string w)
            {
                return w;
            }

            var rules = new List<string>();
            var properties = rule.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var c = converter ?? PassThrough;

            foreach (var property in properties)
            {
                if (property.Name == nameof(DynamicRule.Converter) || property.Name == nameof(DynamicRule.Validator))
                    continue;

                var name = ToKebabCase(property.Name);
                if (name.StartsWith("ext"))
                    name = name.Substring("ext".Length);

                var value = property.GetValue(rule);

                if (value == null)
                    continue;

                if (value is string s && !string.IsNullOrWhiteSpace(s))
                {
                    var r = c(s);
                    if (string.IsNullOrWhiteSpace(r))
                        return string.Empty;

                    rules.Add($"{name}: {r}");
                    continue;
                }

                if (value is float f)
                {
                    var r = c(f.ToString(CultureInfo.InvariantCulture));
                    if (string.IsNullOrWhiteSpace(r))
                        return string.Empty;

                    rules.Add($"{name}: {r}");
                    continue;
                }

                if (value is Enum)
                {
                    rules.Add($"{name}: {c(ToKebabCase(value.ToString()))}");
                    continue;
                }

                Debug.LogWarning($"Unsupported value: ${name}:${value}");
            }

            return string.Join(" ", rules.Select(w => $"{w};"));
        }

        private bool CompileDynamicRule(string selector, Match m, IRule rule, out string result)
        {
            var val = new Regex(@"\$\d+", RegexOptions.Compiled);
            if (rule is IDynamicRule d)
            {
                var properties = CompileRule(d, w =>
                {
                    var r = w;
                    var matches = val.Matches(w);
                    foreach (var match in matches.Cast<Match>())
                    {
                        var arbitrary = m.Groups[int.Parse(match.Value.Substring(1))].Value;
                        var validator = d.Validator;

                        if (validator.IsValid(arbitrary))
                        {
                            var converter = d.Converter;
                            var value = converter.ConvertValue(d, _configuration, arbitrary);

                            r = r.Replace(match.Value, value.ToString());
                        }
                        else
                        {
                            Debug.LogWarning($"Unsupported value: ${selector}:${arbitrary}");
                            return string.Empty;
                        }
                    }

                    return r;
                });

                if (!string.IsNullOrWhiteSpace(properties))
                {
                    result = $".{selector} {{ {properties} }}";
                    return true;
                }
            }

            result = string.Empty;
            return false;
        }

        private static StyleSheet CompileStyleSheetFromString(string str)
        {
            var asset = ScriptableObject.CreateInstance<StyleSheet>();
            asset.hideFlags = HideFlags.NotEditable;
            asset.name = $"UStyled-Jit-Compiled-{CalcHashString(str)}.uss";

            try
            {
                var t = typeof(AssetDatabase).Assembly.GetType("UnityEditor.StyleSheets.StyleSheetImporterImpl") ?? typeof(AssetDatabase).Assembly.GetType("UnityEditor.UIElements.StyleSheets.StyleSheetImporterImpl");
                ;
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

        private static List<string> GetVisualElementAssets(VisualTreeAsset asset)
        {
            var getter = typeof(VisualTreeAsset).GetProperty("visualElementAssets", BindingFlags.Instance | BindingFlags.NonPublic);
            var classes = new List<string>();

            if (getter?.GetValue(asset) is IList values)
            {
                var c = values[0].GetType().GetProperty("classes", BindingFlags.Instance | BindingFlags.Public);

                foreach (var value in values)
                    classes.AddRange(c?.GetValue(value) as string[] ?? Array.Empty<string>());
            }

            return classes;
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