// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Rules
{
    public interface IPadding : IRule
    {
        object Padding { get; }

        object PaddingLeft { get; }

        object PaddingRight { get; }

        object PaddingTop { get; }

        object PaddingBottom { get; }
    }
}