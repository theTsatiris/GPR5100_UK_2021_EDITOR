using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiHandler : MonoBehaviour
{
    public GameObject SaveModal;

    public GameObject Player;

    public TMP_Text playerName;
    public TMP_Text scoreText;
    public TMP_Text nameText;

    public static GameData PLAYER_DATA;

    // Start is called before the first frame update
    void Start()
    {
        if (System.IO.File.Exists(Application.dataPath + "/Saves/playerdata.json"))
        {
            string jsonData = System.IO.File.ReadAllText(Application.dataPath + "/Saves/playerdata.json");
            PLAYER_DATA = JsonUtility.FromJson<GameData>(jsonData);
        }
        else
        {
            PLAYER_DATA = new GameData();
            PLAYER_DATA.playerScore = 0;
            PLAYER_DATA.playerName = "New Player";
            PLAYER_DATA.playerPosition = new Vector3(0.0f, 1.35f, 0.0f);

            PLAYER_DATA.goodCollectibles = new List<Vector3>();
            PLAYER_DATA.badCollectibles = new List<Vector3>();
        }

        nameText.text = PLAYER_DATA.playerName;

        Instantiate(Player, PLAYER_DATA.playerPosition, Quaternion.identity);

        CollectibleSpawner.csInstance.SpawnCollectibles();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = UiHandler.PLAYER_DATA.playerScore.ToString();
    }

    public void OnSaveButtonClick()
    {
        SaveModal.SetActive(true);
    }

    public void OnExitModalButtonClick()
    {
        SaveModal.SetActive(false);
    }

    public void OnFinalSaveButtonClick()
    {
        if(!string.IsNullOrEmpty(playerName.text))
        {
            PLAYER_DATA.playerName = playerName.text;

            PLAYER_DATA.goodCollectibles.Clear();
            PLAYER_DATA.badCollectibles.Clear();

            GameObject[] goodCollectibles = GameObject.FindGameObjectsWithTag("good");
            GameObject[] badCollectibles = GameObject.FindGameObjectsWithTag("bad");

            foreach(GameObject obj in goodCollectibles)
            {
                PLAYER_DATA.goodCollectibles.Add(obj.transform.position);
            }
            foreach(GameObject obj in badCollectibles)
            {
                PLAYER_DATA.badCollectibles.Add(obj.transform.position);
            }

            string jsonString = JsonUtility.ToJson(PLAYER_DATA);

            Debug.Log(jsonString);

            System.IO.File.WriteAllText(Application.dataPath + "/Saves/playerdata.json", jsonString);

            SaveModal.SetActive(false);
        }
    }
}
