// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text.RegularExpressions;

using NatsunekoLaboratory.UStyled.Compiler;
using NatsunekoLaboratory.UStyled.Configurations;
using NatsunekoLaboratory.UStyled.Rules.Interfaces;

namespace NatsunekoLaboratory.UStyled.Rules
{
    public class RawSelectorPropertyRule : IDynamicRule
    {
        /**
         * Match to the following patterns:
         * * screen:pseudo-classes:property:value
         * * pseudo-classes:property:value
         * * property:value
         *
         * ref: https://stylifycss.com/en/docs/get-started/
         */
        private static readonly Regex PropertySelectorRegex = new Regex("^(((?<screen>[a-z_-]+):)?(((?<pseudo>[a-z_-]+):)?(?<property>[a-z_-]+):(?<value>.+?)))$", RegexOptions.Compiled | RegexOptions.Multiline);

        public bool TransformHtml => true;

        public bool IsMatchToSelector(string selector)
        {
            return PropertySelectorRegex.IsMatch(selector);
        }

        public void Apply(IConfigurationProvider configuration, ClassContainer container, string selector)
        {
            var match = PropertySelectorRegex.Match(selector);

            var hasScreen = match.Groups["screen"].Success;
            var hasPseudo = match.Groups["pseduo"].Success;
            var hasProperty = match.Groups["property"].Success;
            var hasValue = match.Groups["value"].Success;

            if (hasScreen && !hasPseudo && hasProperty && hasValue)
            {
                var pseudo = match.Groups["screen"].Value;
                var property = match.Groups["property"].Value;
                var value = match.Groups["value"].Value;

                container.Add(container.GetUniqueName(selector, pseudo), new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>(property, value) }, TransformHtml);
            }

            if (hasProperty && hasValue)
            {
                var property = match.Groups["property"].Value;
                var value = match.Groups["value"].Value;

                container.Add(container.GetUniqueName(selector), new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>(property, value) }, TransformHtml);
            }
        }
    }
}