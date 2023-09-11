// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;

using NatsunekoLaboratory.UStyled.Compiler;
using NatsunekoLaboratory.UStyled.Configurations;
using NatsunekoLaboratory.UStyled.Rules.Interfaces;

namespace NatsunekoLaboratory.UStyled.Rules
{
    public class StaticSelectorAndValueRule : IStaticRule
    {
        private readonly string _selector;
        private readonly List<KeyValuePair<string, object>> _values;

        public StaticSelectorAndValueRule(string selector, List<KeyValuePair<string, object>> values)
        {
            _selector = selector;
            _values = values;
        }

        public bool TransformHtml => false;

        public bool IsMatchToSelector(string selector)
        {
            return _selector == $".{selector}";
        }

        public void Apply(IConfigurationProvider configuration, ClassContainer container, string selector)
        {
            container.Add(selector, _values, TransformHtml);
        }
    }
}