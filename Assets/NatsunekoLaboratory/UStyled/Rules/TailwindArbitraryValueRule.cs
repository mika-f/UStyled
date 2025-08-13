// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Linq;
using System.Text.RegularExpressions;

using NatsunekoLaboratory.UStyled.Compiler;
using NatsunekoLaboratory.UStyled.Configurations;
using NatsunekoLaboratory.UStyled.Rules.Interfaces;

namespace NatsunekoLaboratory.UStyled.Rules
{
    public class TailwindArbitraryValueRule : IDynamicRule
    {
        private static readonly Regex TailwindArbitraryValueRegex = new(@"^((?<screen>[a-z0-9]+):)?((?<pseudo>[a-z0-9]+):)?((?<selector>[a-z0-9-]+)+)(\[(?<arbitrary>.*)\])?$", RegexOptions.Compiled);

        public bool TransformHtml => true;


        public bool IsMatchToSelector(string selector)
        {
            return TailwindArbitraryValueRegex.IsMatch(selector);
        }

        public void Apply(IConfigurationProvider configuration, ClassContainer container, string selector)
        {
            var match = TailwindArbitraryValueRegex.Match(selector);

            var hasScreen = match.Groups["screen"].Success;
            var hasPseudo = match.Groups["pseudo"].Success;
            var hasSelector = match.Groups["selector"].Success;
            var arbitraryValue = match.Groups["arbitrary"].Value;

            if (hasScreen && !hasPseudo && hasSelector)
            {
                var pseudo = match.Groups["screen"].Value;
                var actualSelector = match.Groups["selector"].Value;
                var rule = TailwindDefaultRule.TailwindSelectors.Properties.FirstOrDefault(w => w.IsMatchArbitrary(actualSelector, arbitraryValue));
                if (rule != null)
                    container.Add(container.GetUniqueName(selector, pseudo), rule.GetRule(this, arbitraryValue, configuration, actualSelector), TransformHtml);
            }

            if (hasSelector)
            {
                var rule = TailwindDefaultRule.TailwindSelectors.Properties.FirstOrDefault(w => w.IsMatchArbitrary(selector, arbitraryValue));
                if (rule != null)
                    container.Add(container.GetUniqueName(selector), rule.GetRule(this, arbitraryValue, configuration, selector), TransformHtml);
            }
        }
    }
}