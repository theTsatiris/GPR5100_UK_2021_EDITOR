using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FloorHandler))]
public class FloorHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Label("Resize the gameobject [1.0f means no resizing]");

        //base.OnInspectorGUI();
        DrawDefaultInspector();

        FloorHandler handler = (FloorHandler)target;

        handler.sizeModifierY = EditorGUILayout.Slider("% Resizing", handler.sizeModifierY * 100, 10, 200) / 100.0f;
        //handler.sizeModifier = EditorGUILayout.FloatField("Size Factor", handler.sizeModifier);

        string message = "This is a help box!";

        GUILayout.BeginHorizontal();

            if (GUILayout.Button("Resize"))
            {
                handler.ResizeObject();
                //message = "GameObject resized by " + handler.sizeModifier * 100 + "%";
            }
            if (GUILayout.Button("Save"))
            {
                string jsonString = JsonUtility.ToJson(handler);
                System.IO.File.WriteAllText(Application.dataPath + "/Saves/leveldata.json", jsonString);
                //message = "Configuration saved to " + Application.dataPath + "/Saves/leveldata.json";
            }

        GUILayout.EndHorizontal();

        EditorGUILayout.HelpBox(message, MessageType.Info);

    }
}
