// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Rules
{
    public interface ITransform
    {
        object TransformOrigin { get; }

        object Translate { get; }

        object Scale { get; }

        object Rotate { get; }
    }
}