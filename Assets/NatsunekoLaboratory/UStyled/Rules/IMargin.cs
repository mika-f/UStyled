// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Rules
{
    public interface IMargin : IRule
    {
        object Margin { get; }

        object MarginLeft { get; }

        object MarginRight { get; }

        object MarginTop { get; }

        object MarginBottom { get; }
    }
}