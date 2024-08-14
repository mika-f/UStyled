// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;

using NatsunekoLaboratory.UStyled.Compiler;
using NatsunekoLaboratory.UStyled.Configurations;
using NatsunekoLaboratory.UStyled.Rules.Interfaces;

namespace NatsunekoLaboratory.UStyled.Rules
{
    public class TailwindArbitraryValueRule : IDynamicRule
    {
        private static readonly Regex TailwindArbitraryValueRegex = new(@"^((?<screen>[a-z0-9]+):)?((?<pseudo>[a-z0-9]+):)?((?<selector>[a-z0-9-]+)+)(\[(?<arbitray>.*)\])?$", RegexOptions.Compiled);

        public bool TransformHtml => true;


        public bool IsMatchToSelector(string selector)
        {
            return TailwindArbitraryValueRegex.IsMatch(selector);
        }

        public void Apply(IConfigurationProvider configuration, ClassContainer container, string selector)
        {
            //
        }
    }
}