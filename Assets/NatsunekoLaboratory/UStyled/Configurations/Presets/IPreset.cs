// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;

using NatsunekoLaboratory.UStyled.Rules.Interfaces;

namespace NatsunekoLaboratory.UStyled.Configurations.Presets
{
    /// <summary>
    ///     IPreset represents preset for UStyled, which is useful for using pre-defined styles.
    /// </summary>
    public interface IPreset
    {
        List<IRule> Rules { get; }
    }
}