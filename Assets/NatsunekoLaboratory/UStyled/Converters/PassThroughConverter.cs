// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using NatsunekoLaboratory.UStyled.Configurations;
using NatsunekoLaboratory.UStyled.Rules.Interfaces;

namespace NatsunekoLaboratory.UStyled.Converters
{
    public class PassThroughConverter : IPropertyValueConverter
    {
        public object ConvertValue(IDynamicRule rule, IConfigurationProvider configuration, object value)
        {
            return value;
        }
    }
}