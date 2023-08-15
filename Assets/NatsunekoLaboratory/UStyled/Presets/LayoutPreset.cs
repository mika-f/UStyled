// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text.RegularExpressions;

using NatsunekoLaboratory.UStyled.Configurations;
using NatsunekoLaboratory.UStyled.Rules;

namespace NatsunekoLaboratory.UStyled.Presets
{
    public class LayoutPreset : IPreset
    {
        public Dictionary<string, IRule> StaticRules => new Dictionary<string, IRule>
        {
            { "overflow-hidden", new StaticRule { Overflow = Overflow.Hidden } },
            { "overflow-visible", new StaticRule { Overflow = Overflow.Visible } }
        };

        public Dictionary<Regex, IRule> DynamicRules => new Dictionary<Regex, IRule>();
    }
}