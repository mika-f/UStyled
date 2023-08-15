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
    public class StaticPreset : IPreset
    {
        public Dictionary<string, IRule> StaticRules => new Dictionary<string, IRule>
        {
            // visibility
            { "visible", new StaticRule { Visibility = Visibility.Visible } },
            { "invisible", new StaticRule { Visibility = Visibility.Hidden } },
            { "hidden", new StaticRule { Visibility = Visibility.Hidden } },

            // white-space
            { "whitespace-normal", new StaticRule { WhiteSpace = Whitespace.Normal } },
            { "whitespace-nowrap", new StaticRule { WhiteSpace = Whitespace.Nowrap } },

            // text-overflow
            { "truncate", new StaticRule { Overflow = Overflow.Hidden, TextOverflow = "ellipsis", WhiteSpace = Whitespace.Nowrap } },
            { "text-truncate", new StaticRule { Overflow = Overflow.Hidden, TextOverflow = "ellipsis", WhiteSpace = Whitespace.Nowrap } },
            { "text-ellipsis", new StaticRule { TextOverflow = "ellipsis" } },
            { "text-clip", new StaticRule { TextOverflow = "clip" } },

            // font-styles
            { "italic", new StaticRule { ExtUnityFontStyle = FontStyle.Italic } },
            { "non-italic", new StaticRule { ExtUnityFontStyle = FontStyle.Normal } },
            { "bold", new StaticRule { ExtUnityFontStyle = FontStyle.Bold } },
            { "non-bold", new StaticRule { ExtUnityFontStyle = FontStyle.Normal } },
            { "italic-bold", new StaticRule { ExtUnityFontStyle = FontStyle.BoldAndItalic } },
            { "bold-italic", new StaticRule { ExtUnityFontStyle = FontStyle.BoldAndItalic } }
        };

        public Dictionary<Regex, IRule> DynamicRules => new Dictionary<Regex, IRule>
        {
            { new Regex("^curcor-(.*)$"), new DynamicRule { Cursor = "$1" } }
        };
    }
}