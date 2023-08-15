// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text.RegularExpressions;

using NatsunekoLaboratory.UStyled.Extensions;
using NatsunekoLaboratory.UStyled.Rules;

namespace NatsunekoLaboratory.UStyled.Presets
{
    public class PrimitivePreset : IPreset

    {
        public PrimitivePreset()
        {
            var presets = new List<IPreset>
            {
                new AlignPreset(),
                new BehaviourPreset(),
                new BorderPreset(),
                new FlexPreset(),
                new LayoutPreset(),
                new PositionPreset(),
                new SizePreset(),
                new SpacingPreset(),
                new StaticPreset(),
                new TransitionsPreset(),
                new TypographyPreset()
            };

            foreach (var preset in presets)
            {
                StaticRules.AddRange(preset.StaticRules);
                DynamicRules.AddRange(preset.DynamicRules);
            }
        }

        public Dictionary<string, IRule> StaticRules { get; } = new Dictionary<string, IRule>();
        public Dictionary<Regex, IRule> DynamicRules { get; } = new Dictionary<Regex, IRule>();
    }
}