using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject collectible;
    public GameObject badCollectible;

    public float BadCollectibleChance;

    public int NumberOfCollectibles;
    public float minX, minZ, maxX, maxZ;

    public static CollectibleSpawner csInstance { get; private set; }

    void Awake()
    {
        if(csInstance == null)
        {
            csInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCollectibles()
    {
        if ((UiHandler.PLAYER_DATA.badCollectibles.Count == 0) && (UiHandler.PLAYER_DATA.goodCollectibles.Count == 0))
        {
            for (int i = 0; i < NumberOfCollectibles; i++)
            {
                Vector3 randomSpawnPos = new Vector3(Random.Range(minX, maxX), 1.35f, Random.Range(minZ, maxZ));
                float dice = Random.Range(0.0f, 1.0f);
                if (dice < BadCollectibleChance)
                {
                    Instantiate(badCollectible, randomSpawnPos, Quaternion.identity);
                }
                else
                {
                    Instantiate(collectible, randomSpawnPos, Quaternion.identity);
                }
            }
        }
        else
        {
            foreach(Vector3 position in UiHandler.PLAYER_DATA.badCollectibles)
            {
                Instantiate(badCollectible, position, Quaternion.identity);
            }
            foreach(Vector3 position in UiHandler.PLAYER_DATA.goodCollectibles)
            {
                Instantiate(collectible, position, Quaternion.identity);
            }
        }
    }
}
