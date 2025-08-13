// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;

using NatsunekoLaboratory.UStyled.Configurations;
using NatsunekoLaboratory.UStyled.Rules.Interfaces;

namespace NatsunekoLaboratory.UStyled.Converters
{
    public interface ISelectorValueConverter
    {
        List<KeyValuePair<string, object>> ConvertToValue(IDynamicRule rule, IConfigurationProvider configuration, string selector);

        List<KeyValuePair<string, object>> ApplyToValue(IDynamicRule rule, string arbitraryValue, IConfigurationProvider configuration, string selector);

        bool IsAcceptedSelector(string selector);

        bool IsAcceptedValue(string value);
    }
}