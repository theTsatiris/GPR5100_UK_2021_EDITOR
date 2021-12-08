using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public string playerName;
    public Vector3 playerPositsion;
    public int playerScore;

    public List<Vector3> goodCollectibles;
    public List<Vector3> badCollectibles;
}
