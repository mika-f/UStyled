﻿// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Rules
{
    public interface IBorder : IRule
    {
        object BorderWidth { get; }

        object BorderLeftWidth { get; }

        object BorderRightWidth { get; }

        object BorderTopWidth { get; }

        object BorderBottomWidth { get; }
    }
}