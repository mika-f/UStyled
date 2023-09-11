// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Configurations
{
    public enum UStyledPreprocessor
    {
        /// <summary>
        ///     use serialized (string) value for processing class names, it is useful for templates
        /// </summary>
        SerializedValue,

        /// <summary>
        ///     use deserialized (VisualTreeAsset) value for processing class names, this is default value
        /// </summary>
        DeserializedValue
    }
}