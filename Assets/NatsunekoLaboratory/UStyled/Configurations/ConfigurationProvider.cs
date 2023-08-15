// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text.RegularExpressions;

using NatsunekoLaboratory.UStyled.Extensions;
using NatsunekoLaboratory.UStyled.Presets;
using NatsunekoLaboratory.UStyled.Rules;

namespace NatsunekoLaboratory.UStyled.Configurations
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public Dictionary<string, IRule> StaticRules { get; } = new Dictionary<string, IRule>();

        public Dictionary<Regex, IRule> DynamicRules { get; } = new Dictionary<Regex, IRule>();

        public List<IPreset> Presets { get; } = new List<IPreset>();


        public float DefaultFontSize { get; set; } = 16;

        public Dictionary<string, float> SizeVariants { get; set; } = new Dictionary<string, float>
        {
            { "xs", 0.75f },
            { "sm", 0.875f },
            { "base", 1.0f },
            { "lg", 1.125f },
            { "xl", 1.25f },
            { "2xl", 1.5f },
            { "3xl", 1.875f },
            { "4xl", 2.25f },
            { "5xl", 3f },
            { "6xl", 3.75f },
            { "7xl", 4.5f },
            { "8xl", 6f },
            { "9xl", 8f }
        };

        public Dictionary<string, IRule> GetStaticRules()
        {
            var rules = new Dictionary<string, IRule>();
            rules.AddRange(StaticRules);

            foreach (var preset in Presets)
                rules.AddRange(preset.StaticRules);

            return rules;
        }

        public Dictionary<Regex, IRule> GetDynamicRules()
        {
            var rules = new Dictionary<Regex, IRule>();
            rules.AddRange(DynamicRules);

            foreach (var preset in Presets)
                rules.AddRange(preset.DynamicRules);

            return rules;
        }
    }
}