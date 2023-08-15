// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text.RegularExpressions;

using NatsunekoLaboratory.UStyled.Rules;

namespace NatsunekoLaboratory.UStyled.Presets
{
    public class FlexPreset : IPreset
    {
        public Dictionary<string, IRule> StaticRules => new Dictionary<string, IRule>
        {
            { "flex", new StaticRule { Display = Display.Flex } },
            { "flex-1", new StaticRule { Flex = "1 1 0%" } },
            { "flex-auto", new StaticRule { Flex = "1 1 auto" } },
            { "flex-initial", new StaticRule { Flex = "0 1 auto" } },
            { "flex-none", new StaticRule { Flex = "none" } },

            // directions
            { "flex-row", new StaticRule { FlexDirection = FlexDirection.Row } },
            { "flex-row-reverse", new StaticRule { FlexDirection = FlexDirection.RowReverse } },
            { "flex-col", new StaticRule { FlexDirection = FlexDirection.Column } },
            { "flex-col-reverse", new StaticRule { FlexDirection = FlexDirection.ColumnReverse } },

            // wraps
            { "flex-wrap", new StaticRule { FlexWrap = FlexWrap.Wrap } },
            { "flex-wrap-reverse", new StaticRule { FlexWrap = FlexWrap.WrapReverse } },
            { "flex-nowrap", new StaticRule { FlexWrap = FlexWrap.Nowrap } }
        };

        public Dictionary<Regex, IRule> DynamicRules => new Dictionary<Regex, IRule>
        {
            // shrink/grow/basis
            { new Regex("^(flex-)?shrink-(.+)$"), new DynamicRule { FlexShrink = "$2" } },
            { new Regex("^(flex-)?grow-(.+)$"), new DynamicRule { FlexGrow = "$2" } },
            { new Regex("^(flex-)?basis-(.+)$"), new DynamicRule { FlexBasis = "$2" } }
        };
    }
}