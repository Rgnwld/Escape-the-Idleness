using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class LifeBehavior : MonoBehaviour
{
    GameObject UICanvas;
    GameObject GameCanvas;

    PlayerBehavior pb;
    GameManager GM;
    DeathAds ads;
    GameTimer gameTimer;

    TextMeshProUGUI textPro;

    public bool isDead = false;
    [HideInInspector] public bool canTryAgain = false;

    [SerializeField] int startLives;
    [SerializeField] float timerCount = 11;
    bool timer = false;

    //Awake, Start and Update
    #region UnityBasics

    void Awake ()
    {
        //Atrela o Script do PlayerBehavior
        pb = GetComponent<PlayerBehavior>();


        //Atrela o UICanvas
        UICanvas = GameObject.Find("UICanvas");
        GameCanvas = GameObject.Find("GameCanvas");

        //Atrela o GameManager
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Procura o Painel de Tentar de novo (PainelTryAgain)
        if (UICanvas.transform.Find("PainelTryAgain") != null)
            textPro = UICanvas.transform.Find("PainelTryAgain").Find("Timer").GetComponent<TextMeshProUGUI>();

        if (GameObject.Find("Ads") != null)
            ads = GameObject.Find("Ads").GetComponent<DeathAds>();

        if (GameObject.Find("GameTimer") != null)
            gameTimer = GameObject.Find("GameTimer").GetComponent<GameTimer>();

    }

    void Start ()
    {
        //Inicializa a vida do player
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            ads.Load("rewardedVideo");
            GM.SetPlayerLives(startLives);
            GM.SetDeathTimer(timerCount);
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            gameTimer.StartTimer();
        }

        //Atualiza a quantidade de vida na UI;
        UILife();

        //Define o jogador como vivo
        isDead = false;
    }


    void Update ()
    {
        if (timer)
        {
            Timer();
        }
    }

    #endregion

    #region Comportamento do sistema de vida

    //Função de morte - Definição base;
    public void Die ()
    {
        if (GM.Die())
        {
            gameTimer.StopTimer();
            UICanvas.transform.Find("PainelTryAgain").gameObject.SetActive(true);
            isDead = true;
            timer = true;

        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    //Função de tentar novamente;
    void TryAgain ()
    {
        Time.timeScale = 0;
        UICanvas.transform.Find("PainelTryAgain").gameObject.SetActive(false);
        if (ads != null)
        {
            if (canTryAgain)
            {
                if (ads.IsReady("rewardedVideo"))
                {
                    ads.ShowAds("rewardedVideo");
                    isDead = false;
                    UICanvas.transform.Find("EndUI").gameObject.SetActive(isDead);
                }
                else
                {
                    Time.timeScale = 1;
                    UICanvas.transform.Find("PainelTryAgain").gameObject.SetActive(true);
                    UICanvas.transform.Find("PainelTryAgain").Find("CantAds").gameObject.SetActive(true);
                    return;
                }
            }
            else
            {
                //ads.OnUnityAdsReady("video");
                UICanvas.transform.Find("EndUI").gameObject.SetActive(isDead);
                UICanvas.transform.Find("PainelTryAgain").gameObject.SetActive(false);
            }
        }

        else
        {
            UICanvas.transform.Find("EndUI").gameObject.SetActive(true);
        }
    }

    #endregion

    #region Atualizacao de Interface
    void UILife ()
    {
        Transform PlayerLiveUI;

        if (GameCanvas != null)
        {
            PlayerLiveUI = GameCanvas.transform.Find("Lifes");

            for (int activeLives = 0; activeLives < 3; activeLives++)
            {
                if (GM.GetPlayerLives() >= activeLives)
                {
                    PlayerLiveUI.GetChild(activeLives).gameObject.SetActive(true);
                }
                else
                {
                    PlayerLiveUI.GetChild(activeLives).gameObject.SetActive(false);
                }
            }
        }
    }

    //Timer da função tentar novamente
    void Timer ()
    {
        timerCount -= Time.deltaTime;
        textPro.text = timerCount.ToString("0");

        if (canTryAgain == true)
        {
            timer = false;
            TryAgain();
            return;
        }

        if (timerCount < 0)
        {
            timer = false;
            TryAgain();
            return;
        }
    }

    #endregion

}
