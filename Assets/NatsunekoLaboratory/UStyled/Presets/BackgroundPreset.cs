// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text.RegularExpressions;

using NatsunekoLaboratory.UStyled.Rules;

namespace NatsunekoLaboratory.UStyled.Presets
{
    public class BackgroundPreset : IPreset
    {
        public Dictionary<string, IRule> StaticRules => new Dictionary<string, IRule>
        {
            // images
            { "bg-none", new StaticRule { BackgroundImage = "none" } },

            // sizes
            { "bg-auto", new StaticRule { BackgroundSize = "auto" } },
            { "bg-cover", new StaticRule { BackgroundSize = "cover" } },
            { "bg-contain", new StaticRule { BackgroundSize = "contain" } },

            // repeats
            { "bg-repeat", new StaticRule { BackgroundRepeat = new[] { BackgroundRepeat.Repeat } } },
            { "bg-no-repeat", new StaticRule { BackgroundRepeat = new[] { BackgroundRepeat.NoRepeat } } },
            { "bg-repeat-x", new StaticRule { BackgroundRepeat = new[] { BackgroundRepeat.RepeatX } } },
            { "bg-repeat-y", new StaticRule { BackgroundRepeat = new[] { BackgroundRepeat.RepeatY } } },
            { "bg-repeat-round", new StaticRule { BackgroundRepeat = new[] { BackgroundRepeat.Round } } },
            { "bg-repeat-space", new StaticRule { BackgroundRepeat = new[] { BackgroundRepeat.Space } } }
        };

        public Dictionary<Regex, IRule> DynamicRules => new Dictionary<Regex, IRule>
        {
            // images
            { new Regex(@"^bg-(.*)$"), new DynamicRule { BackgroundColor = "$1" } },

            // positions
            { new Regex(@"^bg-([-\w]{3,})$"), new DynamicRule { BackgroundPosition = "$1 $2 $3" } }
        };
    }
}