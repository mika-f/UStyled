// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using UnityEditor;

namespace Assets.NatsunekoLaboratory.UStyled
{
    internal static class UStyledEditorConfigurations
    {
        [InitializeOnLoadMethod]
        public static void InitializeEditor()
        {
            var groups = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            if (groups.Contains("USTYLED"))
                return;

            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, "USTYLED");
        }
    }
}