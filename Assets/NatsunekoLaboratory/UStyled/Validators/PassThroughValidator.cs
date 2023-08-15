// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Validators
{
    public class PassThroughValidator : IPropertyValueValidator
    {
        public bool IsValid(string value)
        {
            return true;
        }
    }
}