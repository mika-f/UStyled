// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Rules
{
    public interface ITransition
    {
        object Transition { get; }

        object TransitionProperty { get; }

        object TransitionDuration { get; }

        object TransitionTimingFunction { get; }

        object TransitionDelay { get; }
    }

    public enum TransitionProperty
    {
        All,
        Initial,
        None
    }

    public enum TransitionTimingFunction
    {
        Ease,
        EaseIn,
        EaseOut,
        EaseInOut,
        Linear,
        EaseInSine,
        EaseOutSine,
        EaseInOutSine,
        EaseInCubic,
        EaseOutCubic,
        EaseInOutCubic,
        EaseInCirc,
        EaseOutCirc,
        EaseInOutCirc,
        EaseInElastic,
        EaseOutElastic,
        EaseInOutElastic,
        EaseInBack,
        EaseOutBack,
        EaseInOutBack,
        EaseInBounce,
        EaseOutBounce,
        EaseInOutBounce
    }
}