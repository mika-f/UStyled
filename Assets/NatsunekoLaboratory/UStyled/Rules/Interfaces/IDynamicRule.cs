// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Rules.Interfaces
{
    /// <summary>
    ///     IDynamicRule represents a rule which can be changed at runtime.
    ///     If you want to use arbitrary rule, you should use this interface. otherwise, you should use IStaticRule.
    /// </summary>
    public interface IDynamicRule : IRule { }
}