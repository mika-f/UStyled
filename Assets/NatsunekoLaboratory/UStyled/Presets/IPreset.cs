// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text.RegularExpressions;

using NatsunekoLaboratory.UStyled.Rules;

namespace NatsunekoLaboratory.UStyled.Presets
{
    public interface IPreset
    {
        Dictionary<string, IRule> StaticRules { get; }

        Dictionary<Regex, IRule> DynamicRules { get; }
    }
}