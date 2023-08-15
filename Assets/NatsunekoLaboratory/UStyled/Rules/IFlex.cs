// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Rules
{
    public interface IFlex : IRule
    {
        object FlexGrow { get; }

        object FlexShrink { get; }

        object FlexBasis { get; }

        object Flex { get; }

        AlignSelf? AlignSelf { get; }

        FlexDirection? FlexDirection { get; }

        FlexWrap? FlexWrap { get; }

        AlignContent? AlignContent { get; }

        AlignItems? AlignItems { get; }

        JustifyContent? JustifyContent { get; }
    }

    public enum AlignSelf
    {
        Auto,
        FlexStart,
        FlexEnd,
        Center,
        Stretch
    }

    public enum FlexDirection
    {
        Row,
        RowReverse,
        Column,
        ColumnReverse
    }

    public enum FlexWrap
    {
        Nowrap,
        Wrap,
        WrapReverse
    }

    public enum AlignContent
    {
        FlexStart,
        FlexEnd,
        Center,
        Stretch
    }

    public enum AlignItems
    {
        Auto,
        FlexStart,
        FlexEnd,
        Center,
        Stretch
    }

    public enum JustifyContent
    {
        FlexStart,
        FlexEnd,
        Center,
        SpaceBetween,
        SpaceAround
    }
}