// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using NatsunekoLaboratory.UStyled.Compiler;
using NatsunekoLaboratory.UStyled.Configurations;

namespace NatsunekoLaboratory.UStyled.Rules.Interfaces
{
    public interface IRule
    {
        bool TransformHtml { get; }


        bool IsMatchToSelector(string selector);

        void Apply(IConfigurationProvider configuration, ClassContainer container, string selector);
    }
}