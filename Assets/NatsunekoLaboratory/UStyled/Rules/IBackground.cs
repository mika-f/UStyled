// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Rules
{
    public interface IBackground : IRule
    {
        object BackgroundColor { get; }

        object BackgroundImage { get; }

        object BackgroundPosition { get; }

        object BackgroundPositionX { get; }

        object BackgroundPositionY { get; }

        BackgroundRepeat[] BackgroundRepeat { get; }

        object BackgroundSize { get; }

        object ExtUnityBackgroundImageTintColor { get; }

        BackgroundScaleMode? ExtUnityBackgroundScaleMode { get; }
    }

    public enum BackgroundRepeat
    {
        RepeatX,
        RepeatY,
        Repeat,
        Space,
        Round,
        NoRepeat
    }

    public enum BackgroundScaleMode
    {
        StretchToFill,
        ScaleAndCrop,
        ScaleToFit
    }
}