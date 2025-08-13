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
            var settings = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            if (settings.Contains("USTYLED"))
                return;

            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, "USTYLED");
        }
    }
}