// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using NatsunekoLaboratory.UStyled.Compiler;
using NatsunekoLaboratory.UStyled.Configurations;
using NatsunekoLaboratory.UStyled.Converters;
using NatsunekoLaboratory.UStyled.Rules.Interfaces;

namespace NatsunekoLaboratory.UStyled.Rules
{
    public class TailwindDefaultRule : IDynamicRule
    {
        private static readonly Regex TailwindSelectorRegex = new(@"^((?<screen>[a-z0-9]+):)?((?<pseudo>[a-z0-9]+):)?((?<selector>[a-z0-9-.]+)+)$", RegexOptions.Compiled);

        public bool TransformHtml => true;

        public bool IsMatchToSelector(string selector)
        {
            return TailwindSelectorRegex.IsMatch(selector);
        }

        public void Apply(IConfigurationProvider configuration, ClassContainer container, string selector)
        {
            var match = TailwindSelectorRegex.Match(selector);

            var hasScreen = match.Groups["screen"].Success;
            var hasPseudo = match.Groups["pseudo"].Success;
            var hasSelector = match.Groups["selector"].Success;

            if (!hasScreen && !hasPseudo && hasSelector)
            {
                var rule = TailwindSelectors.Properties.FirstOrDefault(w => w.IsMatch(selector));
                if (rule != null) container.Add(container.GetUniqueName(selector), rule.GetRule(this, configuration, selector));
            }
        }

        public abstract class TailwindSelector
        {
            public string SelectorPrefix { get; init; }


            public bool IsMatch(string selector)
            {
                if (string.IsNullOrWhiteSpace(SelectorPrefix))
                    return IsMatchToSelector(selector, selector);

                if (selector.StartsWith(SelectorPrefix))
                {
                    var selectorWithoutPrefix = selector.Substring(SelectorPrefix.Length + 1);
                    return IsMatchToSelector(selector, selectorWithoutPrefix);
                }

                return false;
            }

            protected abstract bool IsMatchToSelector(string selector, string selectorWithoutPrefix);

            public List<KeyValuePair<string, object>> GetRule(IDynamicRule rule, IConfigurationProvider configuration, string selector)
            {
                if (string.IsNullOrWhiteSpace(SelectorPrefix))
                    return GetRuleImpl(rule, configuration, selector);

                var withoutPrefix = selector.Substring(SelectorPrefix.Length + 1);
                return GetRuleImpl(rule, configuration, withoutPrefix);
            }

            protected abstract List<KeyValuePair<string, object>> GetRuleImpl(IDynamicRule rule, IConfigurationProvider configuration, string selector);
        }

        public class TailwindStaticValueSelector : TailwindSelector
        {
            public string Key { get; set; }

            public List<string> Values { get; set; }

            protected override bool IsMatchToSelector(string selector, string selectorWithoutPrefix)
            {
                return Values.Contains(selectorWithoutPrefix);
            }

            protected override List<KeyValuePair<string, object>> GetRuleImpl(IDynamicRule rule, IConfigurationProvider configuration, string selector)
            {
                return new List<KeyValuePair<string, object>>
                {
                    new(Key, selector)
                };
            }
        }

        public class TailwindValueConvertSelector : TailwindSelector
        {
            public ISelectorValueConverter Converter { get; set; }

            protected override bool IsMatchToSelector(string selector, string selectorWithoutPrefix)
            {
                return selector.StartsWith(SelectorPrefix + "-") && Converter.IsAcceptedSelector(selectorWithoutPrefix);
            }

            protected override List<KeyValuePair<string, object>> GetRuleImpl(IDynamicRule rule, IConfigurationProvider configuration, string selector)
            {
                return Converter.ConvertToValue(rule, configuration, selector);
            }
        }

        public static class TailwindSelectors
        {
            public static readonly Dictionary<string, float> BaseDefinitionScales = new()
            {
                { "xs", 0.75f },
                { "sm", 0.875f },
                { "base", 1.0f },
                { "lg", 1.125f },
                { "xl", 1.25f },
                { "2xl", 1.5f },
                { "3xl", 1.875f },
                { "4xl", 2.25f },
                { "5xl", 3f },
                { "6xl", 3.75f },
                { "7xl", 4.5f },
                { "8xl", 6f },
                { "9xl", 8f }
            };

            public static readonly Dictionary<string, float> BaseDefinitionSizes = new()
            {
                { "0", 0 },
                { "px", 0.0625f },
                { "0.5", 0.125f },
                { "1", 0.25f },
                { "1.5", 0.375f },
                { "2", 0.5f },
                { "3", 0.75f },
                { "3.5", 0.875f },
                { "4", 1 },
                { "5", 1.25f },
                { "6", 1.5f },
                { "7", 1.75f },
                { "8", 2f },
                { "9", 2.25f },
                { "10", 2.5f },
                { "11", 2.75f },
                { "12", 3f },
                { "14", 3.5f },
                { "16", 4f },
                { "20", 5f },
                { "24", 6f },
                { "28", 7f },
                { "32", 8f },
                { "36", 9f },
                { "40", 10f },
                { "44", 11f },
                { "48", 12f },
                { "52", 13f },
                { "56", 14f },
                { "60", 15f },
                { "64", 16f },
                { "72", 18f },
                { "80", 20f },
                { "96", 24f }
            };

            public static readonly Dictionary<string, float> BaseDefinitionBorderScale = new()
            {
                { "none", 0f },
                { "sm", 0.125f },
                { "", 0.25f },
                { "md", 0.375f },
                { "lg", 0.5f },
                { "xl", 0.75f },
                { "2xl", 1.0f },
                { "3xl", 1.5f },
                { "full", 9999f }
            };


            public static readonly Dictionary<string, string> ExtraDefinitionSizes = new()
            {
                { "full", "100%" }
            };

            public static Dictionary<string, int> BreakpointSizes = new()
            {
                { "sm", 640 },
                { "md", 768 },
                { "lg", 1024 },
                { "xl", 1280 },
                { "2xl", 1536 }
            };

            // https://docs.unity3d.com/ja/2023.1/Manual/UIE-USS-SupportedProperties.html
            public static readonly Dictionary<string, string> AlignContentDefinitions = new()
            {
                { "start", "flex-start" },
                { "end", "flex-end" },
                { "center", "center" },
                { "stretch", "stretch" }
            };

            public static readonly Dictionary<string, string> AlignItemsAndSelfDefinitions = new()
            {
                { "auto", "auto" },
                { "start", "flex-start" },
                { "end", "flex-end" },
                { "center", "center" },
                { "stretch", "stretch" }
            };

            // https://docs.unity3d.com/Manual/UIE-USS-Selectors-Pseudo-Classes.html
            public static Dictionary<string, string> PseudoElements = new()
            {
                { "hover", "hover" },
                { "active", "active" },
                { "inactive", "inactive" },
                { "focus", "focus" },
                { "selected", "selected" },
                { "disabled", "disabled" },
                { "enabled", "enabled" },
                { "checked", "checked" },
                { "root", "root" }
            };

            public static Dictionary<string, string> DefaultColors = new()
            {
                // 
                { "slate-50", "#f8fafc" },
                { "slate-100", "#f1f5f9" },
                { "slate-200", "#e2e8f0" },
                { "slate-300", "#cbd5e1" },
                { "slate-400", "#94a3b8" },
                { "slate-500", "#64748b" },
                { "slate-600", "#475569" },
                { "slate-700", "#334155" },
                { "slate-800", "#1e293b" },
                { "slate-900", "#0f172a" },

                //
                { "gray-50", "#f9fafb" },
                { "gray-100", "#f3f4f6" },
                { "gray-200", "#e5e7eb" },
                { "gray-300", "#d1d5db" },
                { "gray-400", "#9ca3af" },
                { "gray-500", "#6b7280" },
                { "gray-600", "#4b5563" },
                { "gray-700", "#374151" },
                { "gray-800", "#1f2937" },
                { "gray-900", "#111827" },

                //
                { "zinc-50", "#fafafa" },
                { "zinc-100", "#f4f4f5" },
                { "zinc-200", "#e4e4e7" },
                { "zinc-300", "#d4d4d8" },
                { "zinc-400", "#a1a1aa" },
                { "zinc-500", "#71717a" },
                { "zinc-600", "#52525b" },
                { "zinc-700", "#3f3f46" },
                { "zinc-800", "#27272a" },
                { "zinc-900", "#18181b" },

                //
                { "neutral-50", "#fafafa" },
                { "neutral-100", "#f5f5f5" },
                { "neutral-200", "#e5e5e5" },
                { "neutral-300", "#d4d4d4" },
                { "neutral-400", "#a3a3a3" },
                { "neutral-500", "#737373" },
                { "neutral-600", "#525252" },
                { "neutral-700", "#404040" },
                { "neutral-800", "#262626" },
                { "neutral-900", "#171717" },

                // 
                { "stone-50", "#fafaf9" },
                { "stone-100", "#f5f5f4" },
                { "stone-200", "#e7e5e4" },
                { "stone-300", "#d6d3d1" },
                { "stone-400", "#a8a29e" },
                { "stone-500", "#78716c" },
                { "stone-600", "#57534e" },
                { "stone-700", "#44403c" },
                { "stone-800", "#292524" },
                { "stone-900", "#1c1917" },

                //
                { "red-50", "#fef2f2" },
                { "red-100", "#fee2e2" },
                { "red-200", "#fecaca" },
                { "red-300", "#fca5a5" },
                { "red-400", "#f87171" },
                { "red-500", "#ef4444" },
                { "red-600", "#dc2626" },
                { "red-700", "#b91c1c" },
                { "red-800", "#991b1b" },
                { "red-900", "#7f1d1d" },

                //
                { "orange-50", "#fff7ed" },
                { "orange-100", "#ffedd5" },
                { "orange-200", "#fed7aa" },
                { "orange-300", "#fdba74" },
                { "orange-400", "#fb923c" },
                { "orange-500", "#f97316" },
                { "orange-600", "#ea580c" },
                { "orange-700", "#c2410c" },
                { "orange-800", "#9a3412" },
                { "orange-900", "#7c2d12" },

                //
                { "amber-50", "#fffbeb" },
                { "amber-100", "#fef3c7" },
                { "amber-200", "#fde68a" },
                { "amber-300", "#fcd34d" },
                { "amber-400", "#fbbf24" },
                { "amber-500", "#f59e0b" },
                { "amber-600", "#d97706" },
                { "amber-700", "#b45309" },
                { "amber-800", "#92400e" },
                { "amber-900", "#78350f" },

                // 
                { "yellow-50", "#fefce8" },
                { "yellow-100", "#fef9c3" },
                { "yellow-200", "#fef08a" },
                { "yellow-300", "#fde047" },
                { "yellow-400", "#facc15" },
                { "yellow-500", "#eab308" },
                { "yellow-600", "#ca8a04" },
                { "yellow-700", "#a16207" },
                { "yellow-800", "#854d0e" },
                { "yellow-900", "#713f12" },

                //
                { "lime-50", "#f7fee7" },
                { "lime-100", "#ecfccb" },
                { "lime-200", "#d9f99d" },
                { "lime-300", "#bef264" },
                { "lime-400", "#a3e635" },
                { "lime-500", "#84cc16" },
                { "lime-600", "#65a30d" },
                { "lime-700", "#4d7c0f" },
                { "lime-800", "#3f6212" },
                { "lime-900", "#365314" },

                // 
                { "green-50", "#f0fdf4" },
                { "green-100", "#dcfce7" },
                { "green-200", "#bbf7d0" },
                { "green-300", "#86efac" },
                { "green-400", "#4ade80" },
                { "green-500", "#22c55e" },
                { "green-600", "#16a34a" },
                { "green-700", "#15803d" },
                { "green-800", "#166534" },
                { "green-900", "#14532d" },

                // 
                { "emerald-50", "#ecfdf5" },
                { "emerald-100", "#d1fae5" },
                { "emerald-200", "#a7f3d0" },
                { "emerald-300", "#6ee7b7" },
                { "emerald-400", "#34d399" },
                { "emerald-500", "#10b981" },
                { "emerald-600", "#059669" },
                { "emerald-700", "#047857" },
                { "emerald-800", "#065f46" },
                { "emerald-900", "#064e3b" },

                //
                { "teal-50", "#f0fdfa" },
                { "teal-100", "#ccfbf1" },
                { "teal-200", "#99f6e4" },
                { "teal-300", "#5eead4" },
                { "teal-400", "#2dd4bf" },
                { "teal-500", "#14b8a6" },
                { "teal-600", "#0d9488" },
                { "teal-700", "#0f766e" },
                { "teal-800", "#115e59" },
                { "teal-900", "#134e4a" },

                //
                { "cyan-50", "#ecfeff" },
                { "cyan-100", "#cffafe" },
                { "cyan-200", "#a5f3fc" },
                { "cyan-300", "#67e8f9" },
                { "cyan-400", "#22d3ee" },
                { "cyan-500", "#06b6d4" },
                { "cyan-600", "#0891b2" },
                { "cyan-700", "#0e7490" },
                { "cyan-800", "#155e75" },
                { "cyan-900", "#164e63" },

                //
                { "sky-50", "#f0f9ff" },
                { "sky-100", "#e0f2fe" },
                { "sky-200", "#bae6fd" },
                { "sky-300", "#7dd3fc" },
                { "sky-400", "#38bdf8" },
                { "sky-500", "#0ea5e9" },
                { "sky-600", "#0284c7" },
                { "sky-700", "#0369a1" },
                { "sky-800", "#075985" },
                { "sky-900", "#0c4a6e" },

                //
                { "blue-50", "#eff6ff" },
                { "blue-100", "#dbeafe" },
                { "blue-200", "#bfdbfe" },
                { "blue-300", "#93c5fd" },
                { "blue-400", "#60a5fa" },
                { "blue-500", "#3b82f6" },
                { "blue-600", "#2563eb" },
                { "blue-700", "#1d4ed8" },
                { "blue-800", "#1e40af" },
                { "blue-900", "#1e3a8a" },

                //
                { "indigo-50", "#eef2ff" },
                { "indigo-100", "#e0e7ff" },
                { "indigo-200", "#c7d2fe" },
                { "indigo-300", "#a5b4fc" },
                { "indigo-400", "#818cf8" },
                { "indigo-500", "#6366f1" },
                { "indigo-600", "#4f46e5" },
                { "indigo-700", "#4338ca" },
                { "indigo-800", "#3730a3" },
                { "indigo-900", "#312e81" },

                //
                { "violet-50", "#f5f3ff" },
                { "violet-100", "#ede9fe" },
                { "violet-200", "#ddd6fe" },
                { "violet-300", "#c4b5fd" },
                { "violet-400", "#a78bfa" },
                { "violet-500", "#8b5cf6" },
                { "violet-600", "#7c3aed" },
                { "violet-700", "#6d28d9" },
                { "violet-800", "#5b21b6" },
                { "violet-900", "#4c1d95" },

                //
                { "purple-50", "#faf5ff" },
                { "purple-100", "#f3e8ff" },
                { "purple-200", "#e9d5ff" },
                { "purple-300", "#d8b4fe" },
                { "purple-400", "#c084fc" },
                { "purple-500", "#a855f7" },
                { "purple-600", "#9333ea" },
                { "purple-700", "#7e22ce" },
                { "purple-800", "#6b21a8" },
                { "purple-900", "#581c87" },

                // 
                { "fuchsia-50", "#fdf4ff" },
                { "fuchsia-100", "#fae8ff" },
                { "fuchsia-200", "#f5d0fe" },
                { "fuchsia-300", "#f0abfc" },
                { "fuchsia-400", "#e879f9" },
                { "fuchsia-500", "#d946ef" },
                { "fuchsia-600", "#c026d3" },
                { "fuchsia-700", "#a21caf" },
                { "fuchsia-800", "#86198f" },
                { "fuchsia-900", "#701a75" },

                // 
                { "pink-50", "#fdf2f8" },
                { "pink-100", "#fce7f3" },
                { "pink-200", "#fbcfe8" },
                { "pink-300", "#f9a8d4" },
                { "pink-400", "#f472b6" },
                { "pink-500", "#ec4899" },
                { "pink-600", "#db2777" },
                { "pink-700", "#be185d" },
                { "pink-800", "#9d174d" },
                { "pink-900", "#831843" },

                //
                { "rose-50", "#fff1f2" },
                { "rose-100", "#ffe4e6" },
                { "rose-200", "#fecdd3" },
                { "rose-300", "#fda4af" },
                { "rose-400", "#fb7185" },
                { "rose-500", "#f43f5e" },
                { "rose-600", "#e11d48" },
                { "rose-700", "#be123c" },
                { "rose-800", "#9f1239" },
                { "rose-900", "#881337" }
            };

            // public static TailwindSelector Container => new() { SelectorPrefix = "container" };

            // rules
            // ref: https://docs.unity3d.com/ja/2023.1/Manual/UIE-USS-Properties-Reference.html

            // display
            public static TailwindSelector Display => new TailwindStaticValueSelector { SelectorPrefix = "", Key = "display", Values = new List<string> { "flex", "none" } };
            public static TailwindSelector Overflow => new TailwindStaticValueSelector { SelectorPrefix = "overflow", Key = "overflow", Values = new List<string> { "hidden", "visible" } };
            public static TailwindSelector Position => new TailwindStaticValueSelector { SelectorPrefix = "", Key = "position", Values = new List<string> { "absolute", "relative" } };
            public static TailwindSelector InsetByPixel => new TailwindValueConvertSelector { SelectorPrefix = "inset", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "top", "left", "bottom", "right") };

            public static TailwindSelector InsetXByPixel => new TailwindValueConvertSelector { SelectorPrefix = "inset-x", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "left", "right") };
            public static TailwindSelector InsetYByPixel => new TailwindValueConvertSelector { SelectorPrefix = "inset-y", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "top", "bottom") };
            public static TailwindSelector TopByPixel => new TailwindValueConvertSelector { SelectorPrefix = "top", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "top") };
            public static TailwindSelector LeftByPixel => new TailwindValueConvertSelector { SelectorPrefix = "left", Converter = new TailwindKeyValueConverter(ExtraDefinitionSizes, "left") };
            public static TailwindSelector BottomByPixel => new TailwindValueConvertSelector { SelectorPrefix = "bottom", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "bottom") };
            public static TailwindSelector RightByPixel => new TailwindValueConvertSelector { SelectorPrefix = "right", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "right") };

            // flex
            public static TailwindSelector AlignContent => new TailwindValueConvertSelector { SelectorPrefix = "content", Converter = new TailwindKeyValueConverter(AlignContentDefinitions, "align-content") };
            public static TailwindSelector AlignItems => new TailwindValueConvertSelector { SelectorPrefix = "items", Converter = new TailwindKeyValueConverter(AlignItemsAndSelfDefinitions, "align-items") };
            public static TailwindSelector AlignSelf => new TailwindValueConvertSelector { SelectorPrefix = "self", Converter = new TailwindKeyValueConverter(AlignItemsAndSelfDefinitions, "align-self") };


            // text
            public static TailwindSelector FontSize => new TailwindValueConvertSelector { SelectorPrefix = "text", Converter = new TailwindBaseScaleConverter("font-size", 16, "px") };
            public static TailwindSelector FontStyle => new TailwindStaticValueSelector { SelectorPrefix = "font", Key = "-unity-font-style", Values = new List<string> { "italic", "bold", "bold-and-italic" } };
            public static TailwindSelector TextColor => new TailwindValueConvertSelector { SelectorPrefix = "text", Converter = new TailwindKeyValueConverter(DefaultColors, "color") };
            public static TailwindSelector BackgroundColor => new TailwindValueConvertSelector { SelectorPrefix = "bg", Converter = new TailwindKeyValueConverter(DefaultColors, "background-color") };

            // background

            // width
            public static TailwindSelector WidthByPixel => new TailwindValueConvertSelector { SelectorPrefix = "w", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "width") };
            public static TailwindSelector WidthByStatic => new TailwindValueConvertSelector { SelectorPrefix = "w", Converter = new TailwindKeyValueConverter(ExtraDefinitionSizes, "width") };
            public static TailwindSelector MinWidthByPixel => new TailwindValueConvertSelector { SelectorPrefix = "min-w", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "width") };
            public static TailwindSelector MaxWidthByPixel => new TailwindValueConvertSelector { SelectorPrefix = "max-w", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "width") };

            // height
            public static TailwindSelector HeightByPixel => new TailwindValueConvertSelector { SelectorPrefix = "h", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "height") };
            public static TailwindSelector HeightByStatic => new TailwindValueConvertSelector { SelectorPrefix = "h", Converter = new TailwindKeyValueConverter(ExtraDefinitionSizes, "height") };
            public static TailwindSelector MinHeightByPixel => new TailwindValueConvertSelector { SelectorPrefix = "min-h", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "height") };
            public static TailwindSelector MaxHeightByPixel => new TailwindValueConvertSelector { SelectorPrefix = "max-h", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "height") };

            // spacing
            public static TailwindSelector PaddingTopByPixel => new TailwindValueConvertSelector { SelectorPrefix = "pt", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "padding-top") };
            public static TailwindSelector PaddingBottomByPixel => new TailwindValueConvertSelector { SelectorPrefix = "pb", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "padding-bottom") };
            public static TailwindSelector PaddingLeftByPixel => new TailwindValueConvertSelector { SelectorPrefix = "pl", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "padding-left") };
            public static TailwindSelector PaddingRightByPixel => new TailwindValueConvertSelector { SelectorPrefix = "pr", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "padding-right") };
            public static TailwindSelector PaddingXByPixel => new TailwindValueConvertSelector { SelectorPrefix = "px", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "padding-left", "padding-right") };
            public static TailwindSelector PaddingYByPixel => new TailwindValueConvertSelector { SelectorPrefix = "py", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "padding-top", "padding-bottom") };
            public static TailwindSelector PaddingByPixel => new TailwindValueConvertSelector { SelectorPrefix = "p", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "padding") };


            // spacing
            public static TailwindSelector MarginTopByPixel => new TailwindValueConvertSelector { SelectorPrefix = "mt", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "margin-top") };
            public static TailwindSelector MarginBottomByPixel => new TailwindValueConvertSelector { SelectorPrefix = "mb", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "margin-bottom") };
            public static TailwindSelector MarginLeftByPixel => new TailwindValueConvertSelector { SelectorPrefix = "ml", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "margin-left") };
            public static TailwindSelector MarginRightByPixel => new TailwindValueConvertSelector { SelectorPrefix = "mr", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "margin-right") };
            public static TailwindSelector MarginXByPixel => new TailwindValueConvertSelector { SelectorPrefix = "mx", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "margin-left", "margin-right") };
            public static TailwindSelector MarginYByPixel => new TailwindValueConvertSelector { SelectorPrefix = "my", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "margin-top", "margin-bottom") };
            public static TailwindSelector MarginByPixel => new TailwindValueConvertSelector { SelectorPrefix = "m", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "margin") };

            // border
            public static TailwindSelector BorderTopByPixel => new TailwindValueConvertSelector { SelectorPrefix = "border-t", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "border-top-width") };
            public static TailwindSelector BorderBottomByPixel => new TailwindValueConvertSelector { SelectorPrefix = "border-b", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "border-bottom-width") };
            public static TailwindSelector BorderLeftByPixel => new TailwindValueConvertSelector { SelectorPrefix = "border-l", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "border-left-width") };
            public static TailwindSelector BorderRightByPixel => new TailwindValueConvertSelector { SelectorPrefix = "border-r", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "border-right-width") };
            public static TailwindSelector BorderByPixel => new TailwindValueConvertSelector { SelectorPrefix = "border", Converter = new TailwindBaseSizeConverter(BaseDefinitionSizes, 16, "px", "border-width") };
            public static TailwindSelector BorderColor => new TailwindValueConvertSelector { SelectorPrefix = "border", Converter = new TailwindKeyValueConverter(DefaultColors, "border-color") };
            public static TailwindSelector BorderRadiusByPixel => new TailwindValueConvertSelector { SelectorPrefix = "rounded", Converter = new TailwindBaseSizeConverter(BaseDefinitionBorderScale, 16, "px", "border-radius") };
            public static TailwindSelector BorderTopRadiusByPixel => new TailwindValueConvertSelector { SelectorPrefix = "rounded-t", Converter = new TailwindBaseSizeConverter(BaseDefinitionBorderScale, 16, "px", "border-top-left-radius", "border-top-right-radius") };
            public static TailwindSelector BorderRightRadiusByPixel => new TailwindValueConvertSelector { SelectorPrefix = "rounded-r", Converter = new TailwindBaseSizeConverter(BaseDefinitionBorderScale, 16, "px", "border-top-right-radius", "border-bottom-right-radius") };
            public static TailwindSelector BorderBottomRadiusByPixel => new TailwindValueConvertSelector { SelectorPrefix = "rounded-b", Converter = new TailwindBaseSizeConverter(BaseDefinitionBorderScale, 16, "px", "border-bottom-left-radius", "border-bottom-right-radius") };
            public static TailwindSelector BorderLeftRadiusByPixel => new TailwindValueConvertSelector { SelectorPrefix = "rounded-l", Converter = new TailwindBaseSizeConverter(BaseDefinitionBorderScale, 16, "px", "border-top-left-radius", "border-bottom-left-radius") };
            public static TailwindSelector BorderTopLeftRadiusByPixel => new TailwindValueConvertSelector { SelectorPrefix = "rounded-tl", Converter = new TailwindBaseSizeConverter(BaseDefinitionBorderScale, 16, "px", "border-top-left-radius") };
            public static TailwindSelector BorderTopRightRadiusByPixel => new TailwindValueConvertSelector { SelectorPrefix = "rounded-tr", Converter = new TailwindBaseSizeConverter(BaseDefinitionBorderScale, 16, "px", "border-top-right-radius") };
            public static TailwindSelector BorderBottomLeftRadiusByPixel => new TailwindValueConvertSelector { SelectorPrefix = "rounded-bl", Converter = new TailwindBaseSizeConverter(BaseDefinitionBorderScale, 16, "px", "border-bottom-left-radius") };
            public static TailwindSelector BorderBottomRightRadiusByPixel => new TailwindValueConvertSelector { SelectorPrefix = "rounded-br", Converter = new TailwindBaseSizeConverter(BaseDefinitionBorderScale, 16, "px", "border-bottom-right-radius") };

            public static List<TailwindSelector> Properties => new()
            {
                // Container,
                Display,
                Overflow,
                Position,
                InsetByPixel,
                InsetXByPixel,
                InsetYByPixel,
                TopByPixel,
                LeftByPixel,
                BottomByPixel,
                RightByPixel,

                //
                AlignContent,
                AlignItems,
                AlignSelf,

                //
                FontSize,
                FontStyle,
                TextColor,
                BackgroundColor,

                //
                WidthByPixel,
                WidthByStatic,
                MinWidthByPixel,
                MaxWidthByPixel,

                //
                HeightByPixel,
                HeightByStatic,
                MinHeightByPixel,
                MaxHeightByPixel,

                //
                PaddingTopByPixel,
                PaddingBottomByPixel,
                PaddingLeftByPixel,
                PaddingRightByPixel,
                PaddingXByPixel,
                PaddingYByPixel,
                PaddingByPixel,

                //
                MarginTopByPixel,
                MarginBottomByPixel,
                MarginLeftByPixel,
                MarginRightByPixel,
                MarginXByPixel,
                MarginYByPixel,
                MarginByPixel,

                //
                BorderTopByPixel,
                BorderBottomByPixel,
                BorderLeftByPixel,
                BorderRightByPixel,
                BorderByPixel,
                BorderColor,
                BorderRadiusByPixel,
                BorderTopRadiusByPixel,
                BorderBottomRadiusByPixel,
                BorderLeftRadiusByPixel,
                BorderRightRadiusByPixel,
                BorderTopLeftRadiusByPixel,
                BorderTopRightRadiusByPixel,
                BorderBottomLeftRadiusByPixel,
                BorderBottomRightRadiusByPixel
            };
        }

        private class TailwindBaseScaleConverter : ISelectorValueConverter
        {
            private readonly float _baseValue;
            private readonly string _key;
            private readonly string _unit;

            public TailwindBaseScaleConverter(string key, float baseValue, string unit = "")
            {
                _key = key;
                _baseValue = baseValue;
                _unit = unit;
            }

            public List<KeyValuePair<string, object>> ConvertToValue(IDynamicRule rule, IConfigurationProvider configuration, string selector)
            {
                if (TailwindSelectors.BaseDefinitionScales.TryGetValue(selector, out var scale))
                    return new List<KeyValuePair<string, object>>
                    {
                        new(_key, $"{scale * _baseValue}{_unit}")
                    };

                return new List<KeyValuePair<string, object>>();
            }

            public bool IsAcceptedSelector(string selector)
            {
                return TailwindSelectors.BaseDefinitionScales.ContainsKey(selector);
            }
        }

        private class TailwindBaseSizeConverter : ISelectorValueConverter
        {
            private readonly float _baseValue;
            private readonly string[] _properties;
            private readonly string _unit;
            private readonly Dictionary<string, float> _definitions;

            public TailwindBaseSizeConverter(Dictionary<string, float> definitions, float baseValue, string unit = "", params string[] properties)
            {
                _definitions = definitions;
                ;
                _properties = properties;
                _baseValue = baseValue;
                _unit = unit;
            }

            public List<KeyValuePair<string, object>> ConvertToValue(IDynamicRule rule, IConfigurationProvider configuration, string selector)
            {
                var items = new List<KeyValuePair<string, object>>();

                if (_definitions.TryGetValue(selector, out var scale))
                    foreach (var s in _properties)
                        items.Add(new KeyValuePair<string, object>(s, $"{scale * _baseValue}{_unit}"));

                return items;
            }

            public bool IsAcceptedSelector(string selector)
            {
                return _definitions.ContainsKey(selector);
            }
        }

        private class TailwindKeyValueConverter : ISelectorValueConverter
        {
            private readonly string[] _key;
            private readonly Dictionary<string, string> _definitions;

            public TailwindKeyValueConverter(Dictionary<string, string> definitions, params string[] key)
            {
                _key = key;
                _definitions = definitions;
            }

            public List<KeyValuePair<string, object>> ConvertToValue(IDynamicRule rule, IConfigurationProvider configuration, string selector)
            {
                var items = new List<KeyValuePair<string, object>>();

                if (_definitions.TryGetValue(selector, out var color))
                    foreach (var s in _key)
                        items.Add(new KeyValuePair<string, object>(s, color));

                return items;
            }

            public bool IsAcceptedSelector(string selector)
            {
                return _definitions.ContainsKey(selector);
            }
        }
    }
}