using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StandaloneLevelEditor : EditorWindow
{
    public GameObject Terrain;
    public GameObject GoodCollectible;
    public GameObject BadCollectible;

    public float scaleFactor = 1.0f;
    public float badColChance = 0.0f;
    public int numOfCol = 10;
    public int padding = 10;

    private Collider collider;
    private GameObject terrain;
    private GameObject Level;

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

        content.text = "Spawn Collectibles";
        content.tooltip = "Spawns collectibles in an already generated terrain. If there are previously spanwed collectibles, it clears the level first.";
        if (GUILayout.Button(content))
        {
            SpawnCollectibles();
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        content.text = "Save Configuration";
        content.tooltip = "Save level configuration in a .json file";
        if (GUILayout.Button(content))
        {
            SaveConfig();
        }

        content.text = "Load Configuration";
        content.tooltip = "Load level configuration from a .json file";
        if (GUILayout.Button(content))
        {
            LoadConfig();
        }

        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();

        content.text = "Save Level";
        content.tooltip = "Save entire level as a prefab";
        if (GUILayout.Button(content))
        {
            SaveLevel();
        }

        content.text = "Load Level";
        content.tooltip = "Load entire level from prefab";
        if (GUILayout.Button(content))
        {
            LoadLevel();
        }

        GUILayout.EndHorizontal();
    }

    public void CreateTerrain()
    {
        if (terrain != null)
        {
            if(Level == null)
                Level = new GameObject("Level");
            GameObject.DestroyImmediate(terrain);
            terrain = GameObject.Instantiate(Terrain, Vector3.zero, Quaternion.identity);
            terrain.transform.parent = Level.transform;
            terrain.transform.localScale = Vector3.one * scaleFactor;
            collider = terrain.GetComponent<Collider>();
        }
        else
        {
            if(Level == null)
                Level = new GameObject("Level");
            terrain = GameObject.Instantiate(Terrain, Vector3.zero, Quaternion.identity);
            terrain.transform.parent = Level.transform;
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
                    GameObject temp = GameObject.Instantiate(BadCollectible, randomSpawnPos, Quaternion.identity);
                    temp.transform.parent = Level.transform;
                    BadCollectibles.Add(temp);
                }
                else
                {
                    GameObject temp = GameObject.Instantiate(GoodCollectible, randomSpawnPos, Quaternion.identity);
                    temp.transform.parent = Level.transform;
                    GoodCollectibles.Add(temp);
                }
            }
        }
    }

    public void SaveConfig()
    {
        LevelGenData data = new LevelGenData();
        data.ScaleFactor = scaleFactor;
        data.BadColllectibleChance = badColChance;
        data.NumberOfCollectibles = numOfCol;
        data.Padding = padding;

        string jsonString = JsonUtility.ToJson(data);

        string path = EditorUtility.SaveFilePanelInProject("Save configuration data", "", "json", "Pick a suitable file name for your config data");

        if(!string.IsNullOrEmpty(path))
        {
            System.IO.File.WriteAllText(path, jsonString);
        }
    }

    public void LoadConfig()
    {
        string path = EditorUtility.OpenFilePanel("Load level configuration", Application.dataPath, "json");

        if (!string.IsNullOrEmpty(path))
        {
            string jsonString = System.IO.File.ReadAllText(path);
            LevelGenData data = JsonUtility.FromJson<LevelGenData>(jsonString);

            scaleFactor = data.ScaleFactor;
            badColChance = data.BadColllectibleChance;
            numOfCol = data.NumberOfCollectibles;
            padding = data.Padding;
        }
    }

    public void SaveLevel()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save level as a prefab", "", "prefab", "Select a valid file name for your prefab");
        
        if (!string.IsNullOrEmpty(path))
        {
            //If we dont want to connect the new prefab with the level gameobject
            //PrefabUtility.SaveAsPrefabAsset(Level, path);

            //If we want to connect the new prefab with the level gameobject
            PrefabUtility.SaveAsPrefabAssetAndConnect(Level, path, InteractionMode.UserAction);
        }
    }

    public void LoadLevel()
    {
        string path = EditorUtility.OpenFilePanel("Load level from prefab", Application.dataPath, "prefab");
        Debug.Log(path);
        if (!string.IsNullOrEmpty(path))
        {
            GameObject levelPrefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
            Level = GameObject.Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
