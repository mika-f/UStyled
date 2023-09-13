# UStyled

UStyled: A Utility-First USS Framework for UI Toolkit, inspired by [Tailwind CSS](https://tailwindcss.com/).

## Installation

### OpenUPM

```
$ openupm add cat.natsuneko.ustyled
```

## Description

### What is UStyled?

UStyled is a utility-first USS framework for UI Toolkit, inspired by [Tailwind CSS](https://tailwindcss.com/).  
You can styling UI Elements classes like `flex`, `pt-4`, `text-center` and `rotate-90`, and it can be composed to build any design, directly in your markup.

### Supported Styles

UStyled supports the following styles:

- TailwindCSS - `flex`, `pt-4`, `text-center`, `rotate-90` for static values, `top-[124px]`, `before:content-['']` for arbitrary values
- UnoCSS - `grid-cols-[auto,1fr,30px]`, `p-5px`, `mt-[0.3px]`, `bg-hex-b2a8bb` and `bg-[#b2a8bb]` for static and arbitrary values
- StylifyCSS - `height:120px`, `width:auto`, `hover:scale:1.1` and `font-size:12px` for static and arbitrary values

## How to Use

### JIT Mode

JIT Mode enables UStyled to compile USS on the fly.  
You can **also** use dynamic styles like `top-[124px]` and `before:content-['']` in this mode.

First, you need to add the `UStyledCompiler` into your C# script.

```csharp
// NOTE: USTYLED preprocessor directive are defined by UStyled, it is useful to avoid compile error when you distribute your project to others.
#if USTYLED
    using UStyled;
    using UStyled.Configurations;
#endif

public class SomeEditorWindow : EditorWindow
{
#if USTYLED
    private static readonly UStyledCompiler _compiler;
#endif

    // ...

#if USTYLED
    static SomeEditorWindow()
    {
        _compiler = new UStyledCompilerBuilder()
            .UsePreprocessor(UStyledPreprocessor.SerializedValue)
            .UsePresets(new StylifyPreset())
            .Build();
    }
#endif

    // ...

    private void CreateGUI()
    {
        // ...
#if USTYLED
        var (uxml, uss) = _compiler.CompileAsAsset(asset); // asset is VisualTreeAsset
        rootVisualElement.styleSheets.Add(uss);

        var tree = uxml.CloneTree();
        rootVisualElement.Add(tree);
#endif
    }
}
```

Then, you can use UStyled in your UXML.

```xml
<?xml version="1.0" encoding="utf-8"?>
<engine:UXML
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:engine="UnityEngine.UIElements"
  xmlns:editor="UnityEditor.UIElements"
  xsi:noNamespaceSchemaLocation="../../../../UIElementsSchema/UIElements.xsd"
  xsi:schemaLocation="UnityEngine.UIElements ../../../../UIElementsSchema/UnityEngine.UIElements.xsd
                      UnityEditor.UIElements ../../../../UIElementsSchema/UnityEditor.UIElements.xsd"
>
  <engine:VisualElement class="margin-left:auto margin-right:auto padding-left:16px padding-right:16px padding-top:8px padding-bottom:8px width:100%">
    <!-- ... -->
  </engine:VisualElement>
</engine:UXML>
```

### Static Mode

Static Mode enabled UStyled to compile USS at compile time.  
You can **only** use static styles like `flex`, `pt-4`, `text-center` and `rotate-90` in this mode.  
This mode is useful when you want to development performance.

```diff
+        var (uxml, uss) = _compiler.CompileAsAsset(asset, CompilerMode.Static);
-        var (uxml, uss) = _compiler.CompileAsAsset(asset);
```

### Production Mode

If you want to use UStyled in production, you can write the _compiled_ UXML and USS into files.

```csharp
var (uxml, uss) = _compiler.CompileAsString(asset);                      // JIT Mode
var (uxml, uss) = _compiler.CompileAsString(asset, CompilerMode.Static); // Static Mode

File.WriteAllText("path/to/uxml", uxml);
File.WriteAllText("path/to/uss", uss);
```

By exporting the compiled UXML and USS, you can use any styles without UStyled.

### Compile Configuration

By default, UStyled uses the random string for the class selector after compilation.  
If you want to use the other selector, you can use `UStyledCompilerBuilder` to configure the compiler.

```csharp
_compiler = new UStyledCompilerBuilder()
    .UsePreprocessor(UStyledPreprocessor.SerializedValue)
    .UsePresets(new StylifyPreset())
    .UseSelectorConvention(new YourCustomSelectorConvention()) // must implement IUStyledSelectorConvention interface
    .Build();
```

Default selector convention is `RandomAlphabeticalSelectorConvention` and it generates the selector like `u-styled-xxxxx`.
UStyled provides the following naming conventions as built-in:

- `RandomAlphabeticalSelectorConvention`
- `Html4CompatSelectorConvention`

## License

MIT by [@6jz](https://twitter.com/6jz)
