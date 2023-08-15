// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Rules
{
    public interface IText
    {
        object Color { get; }

        object ExtUnityFont { get; }

        object ExtUnityFontDefinition { get; }

        object FontSize { get; }

        FontStyle? ExtUnityFontStyle { get; }

        TextAlign? ExtUnityTextAlign { get; }

        TextOverflowPosition? ExtUnityTextOverflowPosition { get; }

        Whitespace? WhiteSpace { get; }

        object ExtUnityTextOutlineWidth { get; }

        object ExtUnityTextOutlineColor { get; }

        object ExtUnityTextOutline { get; }

        object LetterSpacing { get; }

        object TextOverflow { get; }

        object TextShadow { get; }

        object WordSpacing { get; }
    }

    public enum FontStyle
    {
        Normal,
        Italic,
        Bold,
        BoldAndItalic
    }

    public enum TextAlign
    {
        UpperLeft,
        MiddleLeft,
        LowerLeft,
        UpperCenter,
        MiddleCenter,
        LowerCenter,
        UpperRight,
        MiddleRight,
        LowerRight
    }

    public enum TextOverflowPosition
    {
        Start,
        Middle,
        End
    }

    public enum Whitespace
    {
        Normal,
        Nowrap
    }

    public enum TextOverflow
    {
        Clip,
        Ellipsis,
        Fade
    }
}