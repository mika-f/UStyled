// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using NatsunekoLaboratory.UStyled.Configurations;
using NatsunekoLaboratory.UStyled.Configurations.Presets;
using NatsunekoLaboratory.UStyled.Extensions;
using NatsunekoLaboratory.UStyled.Rules.Interfaces;

namespace NatsunekoLaboratory.UStyled
{
    public record UStyledCompilerBuilder
    {
        public uint DefaultFontSize { get; private init; }

        public IReadOnlyDictionary<string, string> ColorVariants { get; private init; }

        public IReadOnlyDictionary<string, float> SizeVariants { get; private init; }

        public IReadOnlyDictionary<string, string> Variables { get; private init; }

        public UStyledPreprocessor Preprocessor { get; private init; }

        public IReadOnlyCollection<IRule> Rules { get; private init; }

        public IReadOnlyCollection<string> AlwaysGeneratedClasses { get; private init; }

        public UStyledCompilerBuilder UsePreprocessor(UStyledPreprocessor preprocessor)
        {
            return this with { Preprocessor = preprocessor };
        }

        public UStyledCompilerBuilder UsePreset(IPreset preset)
        {
            return UsePresets(preset);
        }

        public UStyledCompilerBuilder UsePresets(params IPreset[] presets)
        {
            return AddRules(presets.SelectMany(w => w.Rules).ToArray());
        }

        public UStyledCompilerBuilder AddRule(IRule rule)
        {
            return AddRules(rule);
        }

        public UStyledCompilerBuilder AddRules(params IRule[] rules)
        {
            var r = new List<IRule>(Rules ?? new List<IRule>());
            r.AddRange(rules);

            return this with { Rules = r.AsReadOnly() };
        }

        public UStyledCompilerBuilder UseAlwaysIncluded(params string[] classes)
        {
            var c = new List<string>(AlwaysGeneratedClasses ?? new List<string>());
            c.AddRange(classes);

            return this with { AlwaysGeneratedClasses = c.AsReadOnly() };
        }

        public UStyledCompilerBuilder SetDefaultFontSize(uint size)
        {
            return this with { DefaultFontSize = size };
        }

        public UStyledCompilerBuilder AddColorVariant(string name, string value)
        {
            return AddColorVariants(new Dictionary<string, string> { { name, value } });
        }

        public UStyledCompilerBuilder AddColorVariants(Dictionary<string, string> variants)
        {
            var r = new Dictionary<string, string>(ColorVariants ?? new Dictionary<string, string>());
            r.AddRange(variants);

            return this with { ColorVariants = r };
        }

        public UStyledCompilerBuilder AddSizeVariant(string name, float value)
        {
            return AddSizeVariants(new Dictionary<string, float> { { name, value } });
        }

        public UStyledCompilerBuilder AddSizeVariants(Dictionary<string, float> variants)
        {
            var r = new Dictionary<string, float>(SizeVariants ?? new Dictionary<string, float>());
            r.AddRange(variants);

            return this with { SizeVariants = r };
        }

        public UStyledCompilerBuilder AddVariable(string name, string value)
        {
            return AddVariables(new Dictionary<string, string> { { name, value } });
        }

        public UStyledCompilerBuilder AddVariables(Dictionary<string, string> variables)
        {
            var r = new Dictionary<string, string>(Variables ?? new Dictionary<string, string>());
            r.AddRange(variables);

            return this with { Variables = r };
        }

        public UStyledCompiler Build()
        {
            return new UStyledCompiler(new ConfigurationProvider(this), Preprocessor, Rules);
        }
    }
}