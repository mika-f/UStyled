# UStyled

UStyled: UnoCSS inspired instant on-demand atomic USS engine.

## Usage

### JIT Compile

UStyled provides JIT compile feature. You can compile USS on-demand.
Example:

```csharp
using NatsunekoLaboratory.UStyled;
using NatsunekoLaboratory.UStyled.Configurations;
using NatsunekoLaboratory.UStyled.Presets;

using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;

namespace NatsunekoLaboratory.Examples.UStyled
{
    public abstract class SomeEditorWindow : EditorWindow
    {
        private static readonly UStyledCompiler UStyled;

        private SerializedObject _so;

        static SomeEditorWindow()
        {
            // create a instance of UStyledCompiler with configurations
            UStyled = new UStyledCompiler().DefineConfig(new ConfigurationProvider { Presets = { new PrimitivePreset() } });
        }

        private static T LoadAssetByGuid<T>(string guid) where T : Object
        {
            return AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid));
        }

        // ReSharper disable once InconsistentNaming
        public void CreateGUI()
        {
            _so = new SerializedObject(this);
            _so.Update();

            var root = rootVisualElement;

            // load target uxml
            var xaml = LoadAssetByGuid<VisualTreeAsset>("...");
            var tree = xaml.CloneTree();

            // compile stylesheet and assign it
            root.styleSheets.Add(UStyled.JitCompile(xaml));

            tree.Bind(_so);
            root.Add(tree);
        }
    }
}
```

### Pre Compile

If you want to distribute your editor extension with UStyled, you can pre-compile USS.  
Example:

```csharp
using System.IO;

using NatsunekoLaboratory.UStyled;
using NatsunekoLaboratory.UStyled.Configurations;
using NatsunekoLaboratory.UStyled.Presets;

using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;

namespace NatsunekoLaboratory.Examples.UStyled
{
    public abstract class UStyledStaticGeneration
    {
        private static readonly UStyledCompiler UStyled;

        private SerializedObject _so;

        static UStyledStaticGeneration()
        {
            // create a instance of UStyledCompiler with configurations
            UStyled = new UStyledCompiler().DefineConfig(new ConfigurationProvider { Presets = { new PrimitivePreset() } });
        }

        private static T LoadAssetByGuid<T>(string guid) where T : Object
        {
            return AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid));
        }

        [MenuItem("Natsuneko Laboratory/UStyled/Generate")]
        public static void Generate()
        {
            var xaml = LoadAssetByGuid<VisualTreeAsset>("...");
            var uss = UStyled.Compile(xaml);
            File.WriteAllText("/path/to/output.uss", uss);
        }
    }
}
```

## USS Classes

UStyled provides some classes to [Windi CSS](https://windicss.org/) and [Tailwind CSS](https://tailwindcss.com/).  
But UStyled does not provide all classes by limitation of USS.

## Presets

You can define custom-rules with presets.

### Static Rules

Static Rules is a rule that is defined in source code.  
Example:

```csharp
using System.Collections.Generic;
using System.Text.RegularExpressions;

using NatsunekoLaboratory.UStyled.Rules;

namespace NatsunekoLaboratory.UStyled.Presets
{
    public class AlignPreset : IPreset
    {
        public Dictionary<string, IRule> StaticRules => new Dictionary<string, IRule>
        {
            // base
            { "text-left", new StaticRule { ExtUnityTextAlign = TextAlign.MiddleLeft } },
            { "text-center", new StaticRule { ExtUnityTextAlign = TextAlign.MiddleCenter } },
            { "text-right", new StaticRule { ExtUnityTextAlign = TextAlign.MiddleRight } },

            // extensions
            { "text-upper-left", new StaticRule { ExtUnityTextAlign = TextAlign.UpperLeft } },
            { "text-middle-left", new StaticRule { ExtUnityTextAlign = TextAlign.MiddleLeft } },
            { "text-lower-left", new StaticRule { ExtUnityTextAlign = TextAlign.LowerLeft } },
            { "text-upper-center", new StaticRule { ExtUnityTextAlign = TextAlign.UpperCenter } },
            { "text-middle-center", new StaticRule { ExtUnityTextAlign = TextAlign.MiddleCenter } },
            { "text-lower-center", new StaticRule { ExtUnityTextAlign = TextAlign.LowerCenter } },
            { "text-upper-right", new StaticRule { ExtUnityTextAlign = TextAlign.UpperRight } },
            { "text-middle-right", new StaticRule { ExtUnityTextAlign = TextAlign.MiddleRight } },
            { "text-lower-right", new StaticRule { ExtUnityTextAlign = TextAlign.LowerRight } }
        };

        public Dictionary<Regex, IRule> DynamicRules => new Dictionary<Regex, IRule>();
    }
}
```

The above code defines some classes for text alignment.  
`.text-left` generates `text-align: left;` and `.text-upper-left` generates `text-align: upper-left;`.

### Dynamic Rules

Dynamic Rules is a rule that is defined in runtime.

```csharp
using System.Collections.Generic;
using System.Text.RegularExpressions;

using NatsunekoLaboratory.UStyled.Converters;
using NatsunekoLaboratory.UStyled.Rules;

namespace NatsunekoLaboratory.UStyled.Presets
{
    public class SpacingPreset : IPreset
    {
        public Dictionary<string, IRule> StaticRules => new Dictionary<string, IRule>(); // EMPTY

        public Dictionary<Regex, IRule> DynamicRules => new Dictionary<Regex, IRule>
        {
            { new Regex(@"^p-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { Padding = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^px-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { PaddingLeft = "$1", PaddingRight = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^py-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { PaddingTop = "$1", PaddingBottom = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^pt-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { PaddingTop = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^pb-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { PaddingBottom = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^pl-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { PaddingLeft = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^pr-([+-]?([0-9]*[.])?[0-9]+)$"), new DynamicRule { PaddingRight = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^p-(.+)$"), new DynamicRule { Padding = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^px-(.+)$"), new DynamicRule { PaddingLeft = "$1", PaddingRight = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^py-(.+)$"), new DynamicRule { PaddingTop = "$1", PaddingBottom = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^pt-(.+)$"), new DynamicRule { PaddingTop = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^pb-(.+)$"), new DynamicRule { PaddingBottom = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^pl-(.+)$"), new DynamicRule { PaddingLeft = "$1", Converter = new UnitConverter() } },
            { new Regex(@"^pr-(.+)$"), new DynamicRule { PaddingRight = "$1", Converter = new UnitConverter() } }
        };
    }
}
```

The above code defines some classes for padding.  
`.p-1` generates `padding: 4px;` and `.p-1.5` generates `padding: 6px;`.  
All values are converted by `Converter` (default: pass-through).

## License

MIT by [@6jz](https://twitter.com/6jz)
