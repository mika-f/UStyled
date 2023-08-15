// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using NatsunekoLaboratory.UStyled.Converters;
using NatsunekoLaboratory.UStyled.Validators;

namespace NatsunekoLaboratory.UStyled.Rules
{
    public class DynamicRule : StaticRule, IDynamicRule
    {
        public IPropertyValueConverter Converter { get; set; } = new PassThroughConverter();
        public IPropertyValueValidator Validator { get; set; } = new PassThroughValidator();
    }
}