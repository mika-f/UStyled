// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace NatsunekoLaboratory.UStyled.Rules.Interfaces
{
    /// <summary>
    ///     IStaticRule represents a rule which cannot be changed at runtime.
    ///     If you want to use pre-compiled rules, you should use this interface. otherwise, you should use IDynamicRule.
    /// </summary>
    public interface IStaticRule : IRule { }
}