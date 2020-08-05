using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIPauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseWindow;
    [SerializeField] Image muteButton;
    [SerializeField] Sprite mute, unmute;

    GameManager GM;
    DeathAds ads;
    GameTimer gameTimer;
    LifeBehavior lb;
    Player player;

    TextMeshProUGUI TimerText;


    bool isPaused = false;

    #region UnityBasic
    void Start ()
    {
        lb = GameObject.Find("Player").GetComponent<LifeBehavior>();
        player = GameObject.Find("Player").GetComponent<Player>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        ads = GameObject.Find("Ads").GetComponent<DeathAds>();
        gameTimer = GameObject.Find("GameTimer").GetComponent<GameTimer>();

        GM.Mute();
        CheckMuteIcon();
    }

    void Update ()
    {
        UpdateUITimer();

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                PauseMenu();
                return;
            }
        }
    }
    #endregion

    #region Configuracao do Pause

    public void PauseMenu ()
    {
        if (!isPaused)
        {
            gameTimer.StopTimer();
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1;
            gameTimer.StartTimer();
            isPaused = false;
        }

        pauseWindow.SetActive(isPaused);
    }
    #endregion

    #region Botoes
    public void QuitButton ()
    {
        //Por enquanto -- A ideia é retornar ao menu principal;
        GM.DeathCounter++;
        if (GM.DeathCounter == 3)
        {
            ads.ShowAds("video");
            GM.DeathCounter = 0;
        }
        PauseMenu();

        player.SetNumberOfDeaths(player.deathCounter + 1);
        player.SetRunDistance(SceneManager.GetActiveScene().buildIndex);
        player.SetRunSpeed(gameTimer.GetTime());
        player.SavePlayer();

        gameTimer.SetTime(0);
        SceneManager.LoadScene(0);
    }

    public void TryAgainButton ()
    {
        //Botão de tentar novamente;
        lb.canTryAgain = true;
    }

    //Sair
    public void RestartLevel ()
    {
        //Reiniciar Nivel
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    #endregion

    #region Configuracoes de Som
    public void MuteAll ()
    {
        GM.isMuted = !GM.isMuted;
        GM.Mute();
        CheckMuteIcon();
    }

    void CheckMuteIcon ()
    {
        if (GM.isMuted) muteButton.sprite = mute;
        else muteButton.sprite = unmute;
    }
    #endregion

    #region Auxiliar
    void UpdateUITimer ()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (transform.Find("Time") != null || gameTimer != null)
            {
                transform.Find("Time").gameObject.SetActive(true);

                TimerText = transform.Find("Time").GetComponent<TextMeshProUGUI>();
                TimerText.text = Time.time.ToString("0");
                TimerText.text = gameTimer.GetTime().ToString("0.00");
            }
        }
    }
    #endregion
}
