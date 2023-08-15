// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text.RegularExpressions;

using NatsunekoLaboratory.UStyled.Rules;

namespace NatsunekoLaboratory.UStyled.Configurations
{
    public interface IConfigurationProvider
    {
        float DefaultFontSize { get; }

        Dictionary<string, float> SizeVariants { get; }

        Dictionary<string, IRule> GetStaticRules();

        Dictionary<Regex, IRule> GetDynamicRules();
    }
}