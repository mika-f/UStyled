// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Rules
{
    public interface IPosition : IRule
    {
        Position? Position { get; }

        object Left { get; }

        object Top { get; }

        object Right { get; }

        object Bottom { get; }
    }

    public enum Position
    {
        Absolute,
        Relative
    }
}