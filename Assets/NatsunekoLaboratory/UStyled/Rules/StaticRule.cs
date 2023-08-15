// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Rules
{
    public class StaticRule : IAll, IAppearance, IBackground, IBorder, IBorderColor, IBorderRadius, ICursor, IFlex, IHeight, IMargin, IPadding, IPosition, ISlice, IText, ITransform, ITransition, IWidth
    {
        public All? All { get; set; }
        public Overflow? Overflow { get; set; }
        public OverflowClipBox? ExtUnityOverflowClipBox { get; set; }
        public object ExtUnityParagraphSpacing { get; set; }
        public float? Opacity { get; set; }
        public Visibility? Visibility { get; set; }
        public Display? Display { get; set; }
        public object BackgroundColor { get; set; }
        public object BackgroundImage { get; set; }
        public object BackgroundPosition { get; set; }
        public object BackgroundPositionX { get; set; }
        public object BackgroundPositionY { get; set; }
        public BackgroundRepeat[] BackgroundRepeat { get; set; }
        public object BackgroundSize { get; set; }
        public object ExtUnityBackgroundImageTintColor { get; set; }
        public BackgroundScaleMode? ExtUnityBackgroundScaleMode { get; set; }
        public object BorderWidth { get; set; }
        public object BorderLeftWidth { get; set; }
        public object BorderRightWidth { get; set; }
        public object BorderTopWidth { get; set; }
        public object BorderBottomWidth { get; set; }
        public object BorderColor { get; set; }
        public object BorderBottomColor { get; set; }
        public object BorderLeftColor { get; set; }
        public object BorderRightColor { get; set; }
        public object BorderTopColor { get; set; }
        public object BorderRadius { get; set; }
        public object BorderTopLeftRadius { get; set; }
        public object BorderTopRightRadius { get; set; }
        public object BorderBottomLeftRadius { get; set; }
        public object BorderBottomRightRadius { get; set; }
        public object Cursor { get; set; }
        public object FlexGrow { get; set; }
        public object FlexShrink { get; set; }
        public object FlexBasis { get; set; }
        public object Flex { get; set; }
        public AlignSelf? AlignSelf { get; set; }
        public FlexDirection? FlexDirection { get; set; }
        public FlexWrap? FlexWrap { get; set; }
        public AlignContent? AlignContent { get; set; }
        public AlignItems? AlignItems { get; set; }
        public JustifyContent? JustifyContent { get; set; }
        public object MinHeight { get; set; }
        public object MaxHeight { get; set; }
        public object Height { get; set; }
        public object Margin { get; set; }
        public object MarginLeft { get; set; }
        public object MarginRight { get; set; }
        public object MarginTop { get; set; }
        public object MarginBottom { get; set; }
        public object Padding { get; set; }
        public object PaddingLeft { get; set; }
        public object PaddingRight { get; set; }
        public object PaddingTop { get; set; }
        public object PaddingBottom { get; set; }
        public Position? Position { get; set; }
        public object Left { get; set; }
        public object Top { get; set; }
        public object Right { get; set; }
        public object Bottom { get; set; }
        public int? ExtUnitySliceLeft { get; set; }
        public int? ExtUnitySliceTop { get; set; }
        public int? ExtUnitySliceRight { get; set; }
        public int? ExtUnitySliceBottom { get; set; }
        public object ExtUnitySliceScale { get; set; }
        public object Color { get; set; }
        public object ExtUnityFont { get; set; }
        public object ExtUnityFontDefinition { get; set; }
        public object FontSize { get; set; }
        public FontStyle? ExtUnityFontStyle { get; set; }
        public TextAlign? ExtUnityTextAlign { get; set; }
        public TextOverflowPosition? ExtUnityTextOverflowPosition { get; set; }
        public Whitespace? WhiteSpace { get; set; }
        public object ExtUnityTextOutlineWidth { get; set; }
        public object ExtUnityTextOutlineColor { get; set; }
        public object ExtUnityTextOutline { get; set; }
        public object LetterSpacing { get; set; }
        public object TextOverflow { get; set; }
        public object TextShadow { get; set; }
        public object WordSpacing { get; set; }
        public object TransformOrigin { get; set; }
        public object Translate { get; set; }
        public object Scale { get; set; }
        public object Rotate { get; set; }
        public object Transition { get; set; }
        public object TransitionProperty { get; set; }
        public object TransitionDuration { get; set; }
        public object TransitionTimingFunction { get; set; }
        public object TransitionDelay { get; set; }
        public object MinWidth { get; set; }
        public object MaxWidth { get; set; }
        public object Width { get; set; }
    }
}