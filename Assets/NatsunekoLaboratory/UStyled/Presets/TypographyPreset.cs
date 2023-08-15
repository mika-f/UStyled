// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text.RegularExpressions;

using NatsunekoLaboratory.UStyled.Converters;
using NatsunekoLaboratory.UStyled.Rules;

namespace NatsunekoLaboratory.UStyled.Presets
{
    internal class TypographyPreset : IPreset
    {
        public Dictionary<string, IRule> StaticRules => new Dictionary<string, IRule>();

        public Dictionary<Regex, IRule> DynamicRules => new Dictionary<Regex, IRule>
        {
            // font-size
            { new Regex("^text-(.*)$"), new DynamicRule { FontSize = "$1", Converter = new UnitConverter() } },

            // letter-spacing
            { new Regex("^(font-)?tracking-(.+)$"), new DynamicRule { LetterSpacing = "$2", Converter = new UnitConverter() } },

            // word-spacing
            { new Regex("^(font-)?word-spacing-(.+)$"), new DynamicRule { WordSpacing = "$2", Converter = new UnitConverter() } },

            // font
            { new Regex("^font-(.+)$"), new DynamicRule { ExtUnityFont = "$1" } }
        };
    }
}