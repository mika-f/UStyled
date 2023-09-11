// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled
{
    public enum CompileMode
    {
        /// <summary>
        ///     Enabling Static compilation. This mode is recommended for rapid development or not familiar with Utility-First CSS.
        ///     When mode=Static, UStyled will generate everything at initial compile time.
        /// </summary>
        Static,

        /// <summary>
        ///     Enabling Just-In-Time compilation. This mode is recommended for production or familiar with Utility-First CSS (such
        ///     as TailwindCSS V2.1+).
        ///     When mode=Jit, UStyled will generate your styles on-demand as you author your templates.
        /// </summary>
        /// <remarks>
        ///     Just-In-Time compilation depends on the order in which the elements appear. Therefore, if you use
        ///     order-dependent styling, you may end up with unintended styling when switching from Static to Jit.
        /// </remarks>
        Jit
    }
}