// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Rules
{
    public interface ICursor : IRule
    {
        object Cursor { get; }
    }

    public enum Cursor
    {
        Arrow,
        Text,
        ResizeVertical,
        ResizeHorizontal,
        Link,
        SlideArrow,
        ResizeUpRight,
        ResizeUpLeft,
        MoveArrow,
        RotateArrow,
        ScaleArrow,
        ArrowPlus,
        ArrowMinus,
        Pan,
        Orbit,
        Zoom,
        Fps,
        SplitResizeUpDown,
        SplitResizeLeftRight
    }
}