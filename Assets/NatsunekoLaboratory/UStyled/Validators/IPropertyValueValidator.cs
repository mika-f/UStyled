// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using NatsunekoLaboratory.UStyled.Configurations;

namespace NatsunekoLaboratory.UStyled.Validators
{
    public interface IPropertyValueValidator
    {
        bool IsValid(IConfigurationProvider configuration, string value);
    }
}