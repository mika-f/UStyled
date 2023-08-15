// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text.RegularExpressions;

using NatsunekoLaboratory.UStyled.Rules;

namespace NatsunekoLaboratory.UStyled.Presets
{
    public class BorderPreset : IPreset
    {
        public Dictionary<string, IRule> StaticRules => new Dictionary<string, IRule>
        {
            // radius
            { "rounded-full", new StaticRule { BorderRadius = "9999px" } }
        };

        public Dictionary<Regex, IRule> DynamicRules => new Dictionary<Regex, IRule>
        {
            // size
            { new Regex(@"^border-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { BorderWidth = "$1px" } },
            { new Regex(@"^border-x-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { BorderLeftWidth = "$1px", BorderRightWidth = "$1" } },
            { new Regex(@"^border-y-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { BorderLeftColor = "$1px", BorderRightWidth = "$1" } },
            { new Regex(@"^border-t-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { BorderTopWidth = "$1px, " } },
            { new Regex(@"^border-b-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { BorderBottomWidth = "$1px" } },
            { new Regex(@"^border-l-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { BorderLeftWidth = "$1px" } },
            { new Regex(@"^border-r-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { BorderRightWidth = "$1px" } },

            // colors
            { new Regex(@"^border-(.+)$"), new DynamicRule { BorderColor = "#$1" } },
            { new Regex(@"^border-x-(.+)$"), new DynamicRule { BorderLeftColor = "#$1", BorderRightColor = "#$1" } },
            { new Regex(@"^border-y-(.+)$"), new DynamicRule { BorderLeftColor = "#$1", BorderRightColor = "#$1" } },
            { new Regex(@"^border-t-(.+)$"), new DynamicRule { BorderTopColor = "#$1" } },
            { new Regex(@"^border-b-(.+)$"), new DynamicRule { BorderBottomColor = "#$1" } },
            { new Regex(@"^border-l-(.+)$"), new DynamicRule { BorderLeftColor = "#$1" } },
            { new Regex(@"^border-r-(.+)$"), new DynamicRule { BorderRightColor = "#$1" } },

            // radius
            { new Regex(@"^rounded-(.+)$"), new DynamicRule { BorderRadius = "$1px" } },
            { new Regex(@"^rounded-t-(.+)$"), new DynamicRule { BorderTopLeftRadius = "$1px", BorderTopRightRadius = "$1px" } },
            { new Regex(@"^rounded-b-(.+)$"), new DynamicRule { BorderBottomLeftRadius = "$1px", BorderBottomRightRadius = "$1px" } },
            { new Regex(@"^rounded-l-(.+)$"), new DynamicRule { BorderTopLeftRadius = "$1px", BorderBottomLeftRadius = "$1px" } },
            { new Regex(@"^rounded-r-(.+)$"), new DynamicRule { BorderTopRightRadius = "$1px", BorderBottomRightRadius = "$1px" } },
            { new Regex(@"^rounded-tl-(.+)$"), new DynamicRule { BorderTopLeftRadius = "$1px" } },
            { new Regex(@"^rounded-bl-(.+)$"), new DynamicRule { BorderBottomLeftRadius = "$1px" } },
            { new Regex(@"^rounded-tr-(.+)$"), new DynamicRule { BorderTopRightRadius = "$1px" } },
            { new Regex(@"^rounded-br-(.+)$"), new DynamicRule { BorderBottomRightRadius = "$1px" } }
        };
    }
}