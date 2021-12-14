using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public GameObject Terrain;
    public GameObject GoodCollectible;
    public GameObject BadCollectible;

    [HideInInspector]
    public float scaleFactor = 1.0f;
    [HideInInspector]
    public float badColChance = 0.0f;
    [HideInInspector]
    public int numOfCol = 10;
    [HideInInspector]
    public int padding = 10;

    private Collider collider;
    private GameObject terrain;

    private List<GameObject> GoodCollectibles;
    private List<GameObject> BadCollectibles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateTerrain()
    {
        if (terrain != null)
        {
            DestroyImmediate(terrain);
            terrain = Instantiate(Terrain, Vector3.zero, Quaternion.identity);
            terrain.transform.localScale = Vector3.one * scaleFactor;
            collider = terrain.GetComponent<Collider>();
        }
        else
        {
            terrain = Instantiate(Terrain, Vector3.zero, Quaternion.identity);
            terrain.transform.localScale = Vector3.one * scaleFactor;
            collider = terrain.GetComponent<Collider>();
        }
    }

    public void ResizeTerrain()
    {
        if(terrain != null)
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
            if((GoodCollectibles == null) || (BadCollectibles == null))
            {
                GoodCollectibles = new List<GameObject>();
                BadCollectibles = new List<GameObject>();
            }
            else
            {
                foreach(GameObject obj in GoodCollectibles)
                {
                    DestroyImmediate(obj);
                }
                foreach(GameObject obj in BadCollectibles)
                {
                    DestroyImmediate(obj);
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
                    BadCollectibles.Add(Instantiate(BadCollectible, randomSpawnPos, Quaternion.identity));
                }
                else
                {
                    GoodCollectibles.Add(Instantiate(GoodCollectible, randomSpawnPos, Quaternion.identity));
                }
            }
        }
    }
}
