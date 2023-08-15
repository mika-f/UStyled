// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text.RegularExpressions;

using NatsunekoLaboratory.UStyled.Configurations;
using NatsunekoLaboratory.UStyled.Converters;
using NatsunekoLaboratory.UStyled.Rules;

namespace NatsunekoLaboratory.UStyled.Presets
{
    public class TransitionsPreset : IPreset
    {
        public Dictionary<string, IRule> StaticRules => new Dictionary<string, IRule>
        {
            { "transition-none", new StaticRule { Transition = "none" } }
        };

        public Dictionary<Regex, IRule> DynamicRules => new Dictionary<Regex, IRule>
        {
            // timings
            { new Regex(@"^(transition-)?duration-(\d+)$"), new DynamicRule { TransitionDuration = "$2ms" } },
            { new Regex(@"^(transition-)?duration-(.+)$"), new DynamicRule { TransitionDuration = "$2" } },
            { new Regex(@"^(transition-)?delay-(\d+)$"), new DynamicRule { TransitionDelay = "$2ms" } },
            { new Regex(@"^(transition-)?delay-(.+)$"), new DynamicRule { TransitionDelay = "$2" } },
            { new Regex(@"^(transition-)?ease-(.+)$"), new DynamicRule { TransitionTimingFunction = "$2" } },

            // props
            { new Regex(@"^(transition-)?property-(.+)$"), new DynamicRule { TransitionProperty = "$2", Converter = new TransitionPropertyConverter() } }
        };

        private class TransitionPropertyConverter : IPropertyValueConverter
        {
            public object ConvertValue(IDynamicRule rule, IConfigurationProvider configuration, object value)
            {
                switch (value)
                {
                    case "all":
                        return TransitionProperty.All;

                    case "colors":
                        return string.Join(" ,", "color", "background-color", "border-color", "outline-color");

                    case "none":
                        return TransitionProperty.None;

                    case "opacity":
                        return "opacity";

                    case "shadow":
                        return "box-shadow";

                    case "transform":
                        return "transform";

                    default:
                        return value;
                }
            }
        }
    }
}