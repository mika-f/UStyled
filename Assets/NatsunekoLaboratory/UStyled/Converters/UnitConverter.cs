// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using NatsunekoLaboratory.UStyled.Configurations;
using NatsunekoLaboratory.UStyled.Rules;

namespace NatsunekoLaboratory.UStyled.Converters
{
    public class UnitConverter : IPropertyValueConverter
    {
        public object ConvertValue(IDynamicRule rule, IConfigurationProvider configuration, object value)
        {
            if (float.TryParse(value.ToString(), out var f))
                return $"{f * 0.25 * configuration.DefaultFontSize}px";

            if (configuration.SizeVariants.TryGetValue(value.ToString(), out var v))
                return $"{v * configuration.DefaultFontSize}px";

            return value;
        }
    }
}