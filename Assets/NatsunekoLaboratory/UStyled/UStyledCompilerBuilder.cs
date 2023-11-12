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
#if CSHARP_9_OR_LATER
    public record UStyledCompilerBuilder
#else
    public class UStyledCompilerBuilder
#endif
    {
#if CSHARP_9_OR_LATER
        public uint DefaultFontSize { get; private init; }

        public IReadOnlyDictionary<string, string> ColorVariants { get; private init; }

        public IReadOnlyDictionary<string, float> SizeVariants { get; private init; }

        public IReadOnlyDictionary<string, string> Variables { get; private init; }

        public UStyledPreprocessor Preprocessor { get; private init; }

        public IReadOnlyCollection<IRule> Rules { get; private init; }

#endif
        public uint DefaultFontSize { get; private set; }

        public IReadOnlyDictionary<string, string> ColorVariants { get; private set; }

        public IReadOnlyDictionary<string, float> SizeVariants { get; private set; }

        public IReadOnlyDictionary<string, string> Variables { get; private set; }

        public UStyledPreprocessor Preprocessor { get; private set; }

        public IReadOnlyCollection<IRule> Rules { get; private set; }


        public UStyledCompilerBuilder UsePreprocessor(UStyledPreprocessor preprocessor)
        {
#if CSHARP_9_OR_LATER
            return this with { Preprocessor = preprocessor };
#else
            return new UStyledCompilerBuilder
            {
                DefaultFontSize = DefaultFontSize,
                ColorVariants = ColorVariants,
                SizeVariants = SizeVariants,
                Variables = Variables,
                Preprocessor = preprocessor,
                Rules = Rules
            };
#endif
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

#if CSHARP_9_OR_LATER
            return this with { Rules = r.AsReadOnly() };
#else
            return new UStyledCompilerBuilder
            {
                DefaultFontSize = DefaultFontSize,
                ColorVariants = ColorVariants,
                SizeVariants = SizeVariants,
                Variables = Variables,
                Preprocessor = Preprocessor,
                Rules = r.AsReadOnly()
            };
#endif
        }

        public UStyledCompilerBuilder SetDefaultFontSize(uint size)
        {
#if CSHARP_9_OR_LATER
            return this with { DefaultFontSize = size };
#else
            return new UStyledCompilerBuilder
            {
                DefaultFontSize = size,
                ColorVariants = ColorVariants,
                SizeVariants = SizeVariants,
                Variables = Variables,
                Preprocessor = Preprocessor,
                Rules = Rules
            };

#endif
        }

        public UStyledCompilerBuilder AddColorVariant(string name, string value)
        {
            return AddColorVariants(new Dictionary<string, string> { { name, value } });
        }

        public UStyledCompilerBuilder AddColorVariants(Dictionary<string, string> variants)
        {
            var r = new Dictionary<string, string>((IDictionary<string, string>)ColorVariants ?? new Dictionary<string, string>());
            r.AddRange(variants);

#if CSHARP_9_OR_LATER
            return this with { ColorVariants = r };
#else
            return new UStyledCompilerBuilder
            {
                DefaultFontSize = DefaultFontSize,
                ColorVariants = variants,
                SizeVariants = SizeVariants,
                Variables = Variables,
                Preprocessor = Preprocessor,
                Rules = Rules
            };
#endif
        }

        public UStyledCompilerBuilder AddSizeVariant(string name, float value)
        {
            return AddSizeVariants(new Dictionary<string, float> { { name, value } });
        }

        public UStyledCompilerBuilder AddSizeVariants(Dictionary<string, float> variants)
        {
            var r = new Dictionary<string, float>((IDictionary<string, float>)SizeVariants ?? new Dictionary<string, float>());
            r.AddRange(variants);

#if CSHARP_9_OR_LATER
            return this with { SizeVariants = r };
#else
            return new UStyledCompilerBuilder
            {
                DefaultFontSize = DefaultFontSize,
                ColorVariants = ColorVariants,
                SizeVariants = variants,
                Variables = Variables,
                Preprocessor = Preprocessor,
                Rules = Rules
            };
#endif
        }

        public UStyledCompilerBuilder AddVariable(string name, string value)
        {
            return AddVariables(new Dictionary<string, string> { { name, value } });
        }

        public UStyledCompilerBuilder AddVariables(Dictionary<string, string> variables)
        {
            var r = new Dictionary<string, string>((IDictionary<string, string>)Variables ?? new Dictionary<string, string>());
            r.AddRange(variables);

#if CSHARP_9_OR_LATER
            return this with { Variables = r };
#else
            return new UStyledCompilerBuilder
            {
                DefaultFontSize = DefaultFontSize,
                ColorVariants = ColorVariants,
                SizeVariants = SizeVariants,
                Variables = variables,
                Preprocessor = Preprocessor,
                Rules = Rules
            };
#endif
        }

        public UStyledCompiler Build()
        {
            return new UStyledCompiler(new ConfigurationProvider(this), Preprocessor, Rules);
        }
    }
}