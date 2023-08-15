// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using NatsunekoLaboratory.UStyled.Converters;
using NatsunekoLaboratory.UStyled.Validators;

namespace NatsunekoLaboratory.UStyled.Rules
{
    public interface IDynamicRule : IRule
    {
        IPropertyValueConverter Converter { get; }

        IPropertyValueValidator Validator { get; }
    }
}