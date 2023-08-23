// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using UnityEditor;

namespace NatsunekoLaboratory.UStyled
{
    internal static class UStyledEditorConfigurations
    {
        [InitializeOnLoadMethod]
        public static void InitializeEditor()
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, "USTYLED");
        }
    }
}