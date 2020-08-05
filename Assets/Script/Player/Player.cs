using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int deathCounter = 0, longestRun = 0;
    public float fastRun = 0;

    public void SavePlayer ()
    {
        PlayerInfo data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            if (data.deaths > deathCounter)
            {
                deathCounter = data.deaths;
            }

            if (data.longestRun > longestRun)
            {
                longestRun = data.longestRun;
            }

            if (data.fastRun > fastRun)
            {
                fastRun = data.fastRun;
            }
        }

        SaveSystem.SavePlayer(this);
    }

    public PlayerInfo LoadPlayer ()
    {
        PlayerInfo data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            deathCounter = data.deaths;
            longestRun = data.longestRun;
            fastRun = data.fastRun;
        }

        return data;
    }

    public void SetNumberOfDeaths (int _value)
    {
        deathCounter = _value;
    }

    public void SetRunDistance (int _distance)
    {
        longestRun = _distance;
    }

    public void SetRunSpeed (float _speed)
    {
        fastRun = _speed;
    }
}
