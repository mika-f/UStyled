// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace NatsunekoLaboratory.UStyled.Configurations
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public ConfigurationProvider(UStyledCompilerBuilder builder)
        {
            DefaultFontSize = builder.DefaultFontSize;
            ColorVariants = builder.ColorVariants;
            SizeVariants = builder.SizeVariants;
            Variables = builder.Variables;
        }

        public uint DefaultFontSize { get; }
        public IReadOnlyDictionary<string, string> ColorVariants { get; }
        public IReadOnlyDictionary<string, float> SizeVariants { get; }
        public IReadOnlyDictionary<string, string> Variables { get; }
    }
}