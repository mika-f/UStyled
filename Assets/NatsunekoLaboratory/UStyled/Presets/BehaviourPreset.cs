// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text.RegularExpressions;

using NatsunekoLaboratory.UStyled.Rules;

namespace NatsunekoLaboratory.UStyled.Presets
{
    public class BehaviourPreset : IPreset
    {
        public Dictionary<string, IRule> StaticRules => new Dictionary<string, IRule>
        {
            { "outline-none", new StaticRule { ExtUnityTextOutline = "2px transparent" } }
        };

        public Dictionary<Regex, IRule> DynamicRules => new Dictionary<Regex, IRule>
        {
            { new Regex(@"^outline-(width-|size-)?([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { ExtUnityTextOutlineWidth = "$1px" } },
            { new Regex(@"^outline-(.+)$"), new DynamicRule { ExtUnityTextOutlineColor = "#$1" } }
        };
    }
}