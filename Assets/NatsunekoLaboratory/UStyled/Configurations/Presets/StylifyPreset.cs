// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;

using NatsunekoLaboratory.UStyled.Rules;
using NatsunekoLaboratory.UStyled.Rules.Interfaces;

namespace NatsunekoLaboratory.UStyled.Configurations.Presets
{
    public class StylifyPreset : IPreset
    {
        public List<IRule> Rules => new()
        {
            new RawSelectorPropertyRule()
        };
    }
}