using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelCreator))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelCreator creator = (LevelCreator)target;

        creator.scaleFactor = EditorGUILayout.Slider("Terrain Size (%)", creator.scaleFactor * 100, 10.0f, 200.0f) / 100.0f;
        
        GUIContent content = new GUIContent();
        content.text = "Bad Collectibles %";
        content.tooltip = "Determines the maximum percentage of bad collectibles in the set of all spawned collectibles";
        creator.badColChance = EditorGUILayout.Slider(content, creator.badColChance * 100, 0.0f, 100.0f) / 100.0f;

        content.text = "Collectibles #";
        content.tooltip = "Number of total spawned collectibles in the level";
        creator.numOfCol = EditorGUILayout.IntSlider(content, creator.numOfCol, 10, 500);

        creator.padding = EditorGUILayout.IntSlider("Padding", creator.padding, 1, 10);

        GUILayout.BeginHorizontal();

            GUILayout.BeginVertical();

                content.text = "Generate Terrain";
                content.tooltip = "Generates a new terrain from the prefab. If terrain exists, it destroys it.";
                if (GUILayout.Button(content))
                {
                    creator.CreateTerrain();
                }

                content.text = "Resize Terrain";
                content.tooltip = "Resizes an already generated terrain.";
                if (GUILayout.Button(content))
                {
                    creator.ResizeTerrain();
                }

            GUILayout.EndVertical();

            content.text = "Spawn Collectibles";
            content.tooltip = "Spawns collectibles in an already generated terrain. If there are previously spanwed collectibles, it clears the level first.";
            if (GUILayout.Button(content))
            {
                creator.SpawnCollectibles();
            }

        GUILayout.EndHorizontal();
    }
}
