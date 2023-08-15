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
    public class AlignPreset : IPreset
    {
        public Dictionary<string, IRule> StaticRules => new Dictionary<string, IRule>
        {
            // base
            { "text-left", new StaticRule { ExtUnityTextAlign = TextAlign.MiddleLeft } },
            { "text-center", new StaticRule { ExtUnityTextAlign = TextAlign.MiddleCenter } },
            { "text-right", new StaticRule { ExtUnityTextAlign = TextAlign.MiddleRight } },

            // extensions
            { "text-upper-left", new StaticRule { ExtUnityTextAlign = TextAlign.UpperLeft } },
            { "text-middle-left", new StaticRule { ExtUnityTextAlign = TextAlign.MiddleLeft } },
            { "text-lower-left", new StaticRule { ExtUnityTextAlign = TextAlign.LowerLeft } },
            { "text-upper-center", new StaticRule { ExtUnityTextAlign = TextAlign.UpperCenter } },
            { "text-middle-center", new StaticRule { ExtUnityTextAlign = TextAlign.MiddleCenter } },
            { "text-lower-center", new StaticRule { ExtUnityTextAlign = TextAlign.LowerCenter } },
            { "text-upper-right", new StaticRule { ExtUnityTextAlign = TextAlign.UpperRight } },
            { "text-middle-right", new StaticRule { ExtUnityTextAlign = TextAlign.MiddleRight } },
            { "text-lower-right", new StaticRule { ExtUnityTextAlign = TextAlign.LowerRight } }
        };

        public Dictionary<Regex, IRule> DynamicRules => new Dictionary<Regex, IRule>();
    }
}