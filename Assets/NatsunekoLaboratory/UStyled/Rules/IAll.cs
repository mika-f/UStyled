// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Rules
{
    internal interface IAll : IRule
    {
        All? All { get; }
    }

    public enum All
    {
        Initial,
        Inherit,
        Unset,
        Revert,
        RevertLayer
    }
}