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
    public class SizePreset : IPreset
    {
        public Dictionary<string, IRule> StaticRules => new Dictionary<string, IRule>
        {
            // width
            { "w-full", new DynamicRule { Width = "100%" } },

            // height
            { "h-full", new StaticRule { Height = "100%" } }
        };

        public Dictionary<Regex, IRule> DynamicRules => new Dictionary<Regex, IRule>
        {
            // min-width
            { new Regex(@"^min-w-([+-]?([0-9]*[.])?[0-9]+)p$"), new DynamicRule { MinWidth = "$1%" } },
            { new Regex(@"^min-w-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { MinWidth = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^min-w-(.+)$"), new DynamicRule { MinWidth = "$1" } },

            // width
            { new Regex(@"^w-([+-]?([0-9]*[.])?[0-9]+)p$"), new DynamicRule { Width = "$1%" } },
            { new Regex(@"^w-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { Width = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^w-(.+)$"), new DynamicRule { Width = "$1", Converter = new UnitConverter() } },

            // max-width
            { new Regex(@"^max-w-([+-]?([0-9]*[.])?[0-9]+)p$"), new DynamicRule { MaxWidth = "$1%" } },
            { new Regex(@"^max-w-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { MaxWidth = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^max-w-(.+)$"), new DynamicRule { MaxWidth = "$1", Converter = new UnitConverter() } },

            // min-height
            { new Regex(@"^min-h-([+-]?([0-9]*[.])?[0-9]+)p$"), new DynamicRule { MinHeight = "$1%" } },
            { new Regex(@"^min-h-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { MinHeight = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^min-h-(.+)$"), new DynamicRule { MinHeight = "$1", Converter = new UnitConverter() } },

            // height
            { new Regex(@"^h-([+-]?([0-9]*[.])?[0-9]+)p$"), new DynamicRule { Height = "$1%" } },
            { new Regex(@"^h-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { Height = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^h-(.+)$"), new DynamicRule { Height = "$1", Converter = new UnitConverter() } },

            // max-height
            { new Regex(@"^max-h-([+-]?([0-9]*[.])?[0-9]+)p$"), new DynamicRule { MaxHeight = "$1%" } },
            { new Regex(@"^max-h-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { MaxHeight = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^max-h-(.+)$"), new DynamicRule { MaxHeight = "$1", Converter = new UnitConverter() } }
        };
    }
}