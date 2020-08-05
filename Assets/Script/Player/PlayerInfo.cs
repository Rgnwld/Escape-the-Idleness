using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int deaths, longestRun;
    public float fastRun;

    public PlayerInfo (Player player)
    {
        deaths = player.deathCounter;
        longestRun = player.longestRun;
        fastRun = player.fastRun;
    }
}
