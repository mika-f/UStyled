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
    public class TailwindPreset : IPreset
    {
        public TailwindPreset()
        {
            var primitives = new PrimitivePreset();
        }

        public Dictionary<string, IRule> StaticRules { get; } = new Dictionary<string, IRule>();

        public Dictionary<Regex, IRule> DynamicRules { get; } = new Dictionary<Regex, IRule>();
    }
}