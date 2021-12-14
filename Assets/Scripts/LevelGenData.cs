using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenData
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

    private List<GameObject> GoodCollectibles;
    private List<GameObject> BadCollectibles;

    /*public LevelGenData()
    {

    }

    public LevelGenData(GameObject terrain, GameObject goodCollectible, GameObject badCollectible)
    {
        Terrain = terrain;
        GoodCollectible = goodCollectible;
        BadCollectible = badCollectible;
    }*/

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
