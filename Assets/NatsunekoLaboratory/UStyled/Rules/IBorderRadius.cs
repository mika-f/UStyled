// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Rules
{
    public interface IBorderRadius : IRule
    {
        object BorderRadius { get; }

        object BorderTopLeftRadius { get; }

        object BorderTopRightRadius { get; }

        object BorderBottomLeftRadius { get; }

        object BorderBottomRightRadius { get; }
    }
}