using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditorHelp : EditorWindow
{
    [MenuItem("Super Level Editor/Level Editor HELP")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LevelEditorHelp));
    }

    private void OnGUI()
    {
        EditorGUILayout.HelpBox("This tool creates levels\nPress this button to do that thing,\netc.", MessageType.Info);
    }
}
