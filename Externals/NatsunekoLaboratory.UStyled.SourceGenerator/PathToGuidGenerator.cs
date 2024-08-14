// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.DotnetRuntime.Extensions;

namespace NatsunekoLaboratory.UStyled.SourceGenerator;

[Generator(LanguageNames.CSharp)]
public class PathToGuidGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(CreateForInitializedCode);

        var source = context.SyntaxProvider.ForAttributeWithMetadataName(context, "UStyledExternalAssetReferenceAttribute", static (node, token) => true, static (context, token) => context);
        context.RegisterSourceOutput(source, static (context, token) =>
        {
            var ct = context.CancellationToken;
            ct.ThrowIfCancellationRequested();

            var name = token.TargetSymbol.Name;
            var ns = token.TargetSymbol.ContainingNamespace.ToDisplayString();
            var type = token.TargetSymbol.ContainingType.Name;
            if (token.TargetNode is not MethodDeclarationSyntax mt)
                return;

            if (!mt.Modifiers.Any(SyntaxKind.PartialKeyword))
                return;

            var dir = Path.GetDirectoryName(token.TargetNode.SyntaxTree.FilePath);
            var path = Path.GetFullPath(Path.Combine(dir, token.Attributes[0].ConstructorArguments[0].Value as string));
            var guid = "";
            if (File.Exists(path) && File.Exists($"{path}.meta"))
            {
                using var sr = new StreamReader($"{path}.meta");
                var text = sr.ReadToEnd();
                var match = new Regex(@"^guid: (?<guid>[a-f0-9]{32})", RegexOptions.Compiled | RegexOptions.Multiline).Match(text);
                var hasGuid = match.Groups["guid"].Success;

                if (hasGuid) guid = match.Groups["guid"].Value;
            }


            var b = SyntaxFactory.Block().AddStatements(SyntaxFactory.ReturnStatement(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(guid))));
            var m = SyntaxFactory.MethodDeclaration(SyntaxFactory.IdentifierName("string"), name).WithModifiers(mt.Modifiers).WithBody(b);
            var c = SyntaxFactory.ClassDeclaration(SyntaxFactory.Identifier(type)).WithModifiers(((ClassDeclarationSyntax)mt.Parent).Modifiers).AddMembers(m);
            var s = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(ns)).AddMembers(c);
            var source = s.NormalizeWhitespace().ToFullString();

            context.AddSource($"{ns}.{type}.{name}.g.cs", source);
        });
    }

    private static void CreateForInitializedCode(IncrementalGeneratorPostInitializationContext context)
    {
        var ct = context.CancellationToken;
        ct.ThrowIfCancellationRequested();

        context.AddSource("UStyledExternalAssetReferenceAttribute.g.cs", @"
[global::System.AttributeUsage(global::System.AttributeTargets.Method)]
class UStyledExternalAssetReferenceAttribute : global::System.Attribute {
    public string Path { get; }

    public UStyledExternalAssetReferenceAttribute(string path)
    {
        Path = path;
    }
}
");
    }
}