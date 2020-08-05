using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int playerExtraLives;
    [SerializeField] float timer;
    AudioSource[] aSList;
    public bool isMuted = false;
    public int DeathCounter = 0;

    #region Inicializador

    void Awake ()
    {

        if (!GameObject.Find("GameManager"))
        {
            GameObject gm = new GameObject("GameManager");
            gm.AddComponent<GameManager>();
            DontDestroyOnLoad(gm);

            if (!GameObject.Find("Ads"))
            {
                GameObject ads = new GameObject("Ads");
                ads.AddComponent<DeathAds>();
                ads.AddComponent<BannerScript>();
                DontDestroyOnLoad(ads);
            }

            if (!GameObject.Find("GameTimer"))
            {
                GameObject timer = new GameObject("GameTimer");
                timer.AddComponent<GameTimer>();
                DontDestroyOnLoad(timer);
            }
        }
    }

    #endregion

    #region Configuracao de Som
    public void Mute ()
    {
        aSList = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in aSList)
        {
            audioSource.mute = isMuted;
        }
    }
    #endregion

    #region Configuracao de Vida
    public void SetPlayerLives (int lives)
    {
        playerExtraLives = lives;
    }

    public int GetPlayerLives ()
    {
        return playerExtraLives;
    }

    public void SetDeathTimer (float _timer)
    {
        timer = _timer;
    }

    public float GetDeathTimer ()
    {
        return timer;
    }

    public bool Die ()
    {
        if (playerExtraLives >= 1)
        {
            playerExtraLives--;
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion
}
