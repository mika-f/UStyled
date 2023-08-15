// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Rules
{
    public interface IBorderColor : IRule
    {
        object BorderColor { get; }

        object BorderBottomColor { get; }

        object BorderLeftColor { get; }

        object BorderRightColor { get; }

        object BorderTopColor { get; }
    }
}