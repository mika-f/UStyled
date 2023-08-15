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
    public class SpacingPreset : IPreset
    {
        public Dictionary<string, IRule> StaticRules => new Dictionary<string, IRule>(); // EMPTY

        public Dictionary<Regex, IRule> DynamicRules => new Dictionary<Regex, IRule>
        {
            // margin
            { new Regex(@"^m-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { Margin = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^mx-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { MarginLeft = "$1", MarginRight = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^my-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { MarginTop = "$1", MarginBottom = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^mt-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { MarginTop = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^mb-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { MarginBottom = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^ml-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { MarginLeft = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^mr-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { MarginRight = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^m-(.+)$"), new DynamicRule { Margin = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^mx-(.+)$"), new DynamicRule { MarginLeft = "$1", MarginRight = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^my-(.+)$"), new DynamicRule { MarginTop = "$1", MarginBottom = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^mt-(.+)$"), new DynamicRule { MarginTop = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^mb-(.+)$"), new DynamicRule { MarginBottom = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^ml-(.+)$"), new DynamicRule { MarginLeft = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^mr-(.+)$"), new DynamicRule { MarginRight = "$1", Converter = new UnitConverter() } },

            // padding
            { new Regex(@"^p-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { Padding = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^px-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { PaddingLeft = "$1", PaddingRight = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^py-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { PaddingTop = "$1", PaddingBottom = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^pt-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { PaddingTop = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^pb-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { PaddingBottom = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^pl-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { PaddingLeft = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^pr-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { PaddingRight = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^p-(.+)$"), new DynamicRule { Padding = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^px-(.+)$"), new DynamicRule { PaddingLeft = "$1", PaddingRight = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^py-(.+)$"), new DynamicRule { PaddingTop = "$1", PaddingBottom = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^pt-(.+)$"), new DynamicRule { PaddingTop = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^pb-(.+)$"), new DynamicRule { PaddingBottom = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^pl-(.+)$"), new DynamicRule { PaddingLeft = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^pr-(.+)$"), new DynamicRule { PaddingRight = "$1", Converter = new UnitConverter() } }
        };
    }
}