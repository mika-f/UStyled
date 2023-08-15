// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Rules
{
    public interface ISlice
    {
        int? ExtUnitySliceLeft { get; }

        int? ExtUnitySliceTop { get; }

        int? ExtUnitySliceRight { get; }


        int? ExtUnitySliceBottom { get; }

        object ExtUnitySliceScale { get; }
    }
}