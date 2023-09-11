// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Linq;

using NatsunekoLaboratory.UStyled.Configurations;

namespace NatsunekoLaboratory.UStyled.Validators
{
    public class UnitValidator : IPropertyValueValidator
    {
        private static readonly string[] Units =
        {
            // absolute units
            "cm",
            "mm",
            "Q",
            "in",
            "pc",
            "pt",
            "px",

            // relative units
            "em",
            "ex",
            "ch",
            "rem",
            "lh",
            "rlh",
            "vw",
            "vh",
            "vmin",
            "vmax",
            "vb",
            "vi",
            "svw",
            "svh",
            "lvw",
            "lvh",
            "dvh",
            "dvw"
        };

        public bool IsValid(IConfigurationProvider configuration, string value)
        {
            if (float.TryParse(value, out _))
                return true;

            if (Units.Any(value.EndsWith))
                return true;

            if (configuration.SizeVariants.ContainsKey(value))
                return true;

            return false;
        }
    }
}