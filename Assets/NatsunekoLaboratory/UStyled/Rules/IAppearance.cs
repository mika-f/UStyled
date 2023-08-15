// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Rules
{
    public interface IAppearance : IRule
    {
        Overflow? Overflow { get; }

        OverflowClipBox? ExtUnityOverflowClipBox { get; }

        object ExtUnityParagraphSpacing { get; }

        float? Opacity { get; }

        Visibility? Visibility { get; }

        Display? Display { get; }
    }

    public enum Overflow
    {
        Hidden,
        Visible
    }

    public enum OverflowClipBox
    {
        PaddingBox,
        ContentBox
    }

    public enum Visibility
    {
        Visible,
        Hidden
    }

    public enum Display
    {
        Flex,
        None
    }
}