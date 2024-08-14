// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using JetBrains.Annotations;

namespace NatsunekoLaboratory.UStyled.Compiler
{
    public class ClassContainer
    {
        private readonly Dictionary<string, string> _mappings = new();
        private readonly Dictionary<string, (string Value, bool Transform)> _selectors = new();
        private const string Alphanumerical = "abcdefghijklmnopqrstuvwxyz0123456789_-";
        private readonly Regex _htmlSafeSelector = new($"^[{Alphanumerical}]+$", RegexOptions.Compiled);

        public void Clear()
        {
            _selectors.Clear();
            _mappings.Clear();
        }

        public void Add(string selector, List<KeyValuePair<string, object>> value, bool transform = false)
        {
            if (_selectors.ContainsKey(selector))
                return;

            var sb = new StringBuilder();
            foreach (var (key, val) in value)
                sb.Append($"{key}: {val};");

            _selectors[selector] = (sb.ToString().Replace(Environment.NewLine, ""), transform);
        }

        public string TransformHtml(string source)
        {
            if (_mappings.Count == 0)
                return source;

            foreach (var (selector, unique) in _mappings)
                source = source.Replace(selector, unique.Contains(":") ? unique.Substring(0, unique.IndexOf(":", StringComparison.Ordinal)) : unique);

            return source;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var (selector, (value, _)) in _selectors)
                sb.AppendLine($".{selector} {{ {value} }}");

            return sb.ToString();
        }

        public string GetUniqueName(string original, [CanBeNull] string pseudo = null)
        {
            if (_mappings.TryGetValue(original, out var name))
                return name;

            var random = new Random();
            var sb = new StringBuilder();
            string str;

            if (_htmlSafeSelector.IsMatch(original))
            {
                _mappings.Add(original, original);
                return original;
            }

            do
            {
                sb.Clear();

                for (var i = 0; i < 10; i++)
                {
                    var n = random.Next(Alphanumerical.Length);
                    sb.Append(Alphanumerical[n]);
                }

                str = sb.ToString();
            } while (_mappings.ContainsKey(str));

            var unique = string.IsNullOrWhiteSpace(pseudo) ? str : $"{str}:{pseudo}";

            _mappings.Add(original, unique);
            return unique;
        }
    }
}