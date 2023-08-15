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
    public class PositionPreset : IPreset
    {
        public Dictionary<string, IRule> StaticRules => new Dictionary<string, IRule>
        {
            // justify-contents
            { "justify-start", new StaticRule { JustifyContent = JustifyContent.FlexStart } },
            { "justify-end", new StaticRule { JustifyContent = JustifyContent.FlexEnd } },
            { "justify-center", new StaticRule { JustifyContent = JustifyContent.Center } },
            { "justify-between", new StaticRule { JustifyContent = JustifyContent.SpaceBetween } },
            { "justify-around", new StaticRule { JustifyContent = JustifyContent.SpaceAround } },

            // align-content
            { "content-center", new StaticRule { AlignContent = AlignContent.Center } },
            { "content-start", new StaticRule { AlignContent = AlignContent.FlexStart } },
            { "content-end", new StaticRule { AlignContent = AlignContent.FlexEnd } },
            { "content-stretch", new StaticRule { AlignContent = AlignContent.Stretch } },

            // align-items
            { "items-start", new StaticRule { AlignItems = AlignItems.FlexStart } },
            { "items-end", new StaticRule { AlignItems = AlignItems.FlexEnd } },
            { "items-center", new StaticRule { AlignItems = AlignItems.Center } },
            { "items-stretch", new StaticRule { AlignItems = AlignItems.Stretch } },
            { "items-auto", new StaticRule { AlignItems = AlignItems.Auto } },

            // align-self
            { "self-auto", new StaticRule { AlignSelf = AlignSelf.Auto } },
            { "self-start", new StaticRule { AlignSelf = AlignSelf.FlexStart } },
            { "self-end", new StaticRule { AlignSelf = AlignSelf.FlexEnd } },
            { "self-center", new StaticRule { AlignSelf = AlignSelf.Center } },
            { "self-stretch", new StaticRule { AlignSelf = AlignSelf.Stretch } },

            // place-content
            { "place-content-center", new StaticRule { JustifyContent = JustifyContent.Center, AlignContent = AlignContent.Center } },
            { "place-content-start", new StaticRule { JustifyContent = JustifyContent.FlexStart, AlignContent = AlignContent.FlexStart } },
            { "place-content-end", new StaticRule { JustifyContent = JustifyContent.FlexEnd, AlignContent = AlignContent.FlexEnd } }
        };

        public Dictionary<Regex, IRule> DynamicRules => new Dictionary<Regex, IRule>
        {
            { new Regex(@"^inset-(.+)$"), new DynamicRule { Top = "$1", Left = "$1", Bottom = "$1", Right = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^inset-x-(.+)$"), new DynamicRule { Left = "$1", Right = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^inset-y-(.+)$"), new DynamicRule { Top = "$1", Bottom = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^t-(.+)$"), new DynamicRule { Top = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^r-(.+)$"), new DynamicRule { Right = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^b-(.+)$"), new DynamicRule { Bottom = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^l-(.+)$"), new DynamicRule { Left = "$1", Converter = new UnitConverter() } }
        };
    }
}