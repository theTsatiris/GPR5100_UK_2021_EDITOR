using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StandaloneLevelEditor : EditorWindow
{
    public static GameObject Terrain;
    public static GameObject GoodCollectible;
    public static GameObject BadCollectible;

    public float scaleFactor = 1.0f;
    public float badColChance = 0.0f;
    public int numOfCol = 10;
    public int padding = 10;

    private Collider collider;
    private GameObject terrain;

    private List<GameObject> GoodCollectibles;
    private List<GameObject> BadCollectibles;

    [MenuItem("Super Level Editor/Level Editor GUI")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(StandaloneLevelEditor));
    }

    void OnGUI()
    {
        Terrain = EditorGUILayout.ObjectField("Terrain", Terrain, typeof(GameObject), true) as GameObject;
        GoodCollectible = EditorGUILayout.ObjectField("Collectible", GoodCollectible, typeof(GameObject), true) as GameObject;
        BadCollectible = EditorGUILayout.ObjectField("Bad Collectible", BadCollectible, typeof(GameObject), true) as GameObject;

        scaleFactor = EditorGUILayout.Slider("Terrain Size (%)", scaleFactor * 100, 10.0f, 200.0f) / 100.0f;

        GUIContent content = new GUIContent();
        content.text = "Bad Collectibles %";
        content.tooltip = "Determines the maximum percentage of bad collectibles in the set of all spawned collectibles";
        badColChance = EditorGUILayout.Slider(content, badColChance * 100, 0.0f, 100.0f) / 100.0f;

        content.text = "Collectibles #";
        content.tooltip = "Number of total spawned collectibles in the level";
        numOfCol = EditorGUILayout.IntSlider(content, numOfCol, 10, 500);

        padding = EditorGUILayout.IntSlider("Padding", padding, 1, 10);

        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();

        content.text = "Generate Terrain";
        content.tooltip = "Generates a new terrain from the prefab. If terrain exists, it destroys it.";
        if (GUILayout.Button(content))
        {
            CreateTerrain();
        }

        content.text = "Resize Terrain";
        content.tooltip = "Resizes an already generated terrain.";
        if (GUILayout.Button(content))
        {
            ResizeTerrain();
        }

        GUILayout.EndVertical();

        content.text = "Spawn Collectibles";
        content.tooltip = "Spawns collectibles in an already generated terrain. If there are previously spanwed collectibles, it clears the level first.";
        if (GUILayout.Button(content))
        {
            SpawnCollectibles();
        }

        GUILayout.EndHorizontal();
    }

    public void CreateTerrain()
    {
        if (terrain != null)
        {
            GameObject.DestroyImmediate(terrain);
            terrain = GameObject.Instantiate(Terrain, Vector3.zero, Quaternion.identity);
            terrain.transform.localScale = Vector3.one * scaleFactor;
            collider = terrain.GetComponent<Collider>();
        }
        else
        {
            terrain = GameObject.Instantiate(Terrain, Vector3.zero, Quaternion.identity);
            terrain.transform.localScale = Vector3.one * scaleFactor;
            collider = terrain.GetComponent<Collider>();
        }
    }

    public void ResizeTerrain()
    {
        if (terrain != null)
            terrain.transform.localScale = Vector3.one * scaleFactor;
    }

    public void SpawnCollectibles()
    {
        if (collider == null)
        {
            Debug.Log("You haven't initialized a terrain yet!!!");
        }
        else
        {
            if ((GoodCollectibles == null) || (BadCollectibles == null))
            {
                GoodCollectibles = new List<GameObject>();
                BadCollectibles = new List<GameObject>();
            }
            else
            {
                foreach (GameObject obj in GoodCollectibles)
                {
                    GameObject.DestroyImmediate(obj);
                }
                foreach (GameObject obj in BadCollectibles)
                {
                    GameObject.DestroyImmediate(obj);
                }
                GoodCollectibles.Clear();
                BadCollectibles.Clear();
            }

            float minX = collider.bounds.min.x + padding;
            float maxX = collider.bounds.max.x - padding;
            float minZ = collider.bounds.min.z + padding;
            float maxZ = collider.bounds.max.z - padding;

            for (int i = 0; i < numOfCol; i++)
            {
                Vector3 randomSpawnPos = new Vector3(Random.Range(minX, maxX), 1.35f, Random.Range(minZ, maxZ));
                float dice = Random.Range(0.0f, 1.0f);
                if (dice < badColChance)
                {
                    BadCollectibles.Add(GameObject.Instantiate(BadCollectible, randomSpawnPos, Quaternion.identity));
                }
                else
                {
                    GoodCollectibles.Add(GameObject.Instantiate(GoodCollectible, randomSpawnPos, Quaternion.identity));
                }
            }
        }
    }
}
