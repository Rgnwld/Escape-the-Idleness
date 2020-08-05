using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class DeathAds : MonoBehaviour, IUnityAdsListener
{
    string gameId = "3699407";
    bool testMode = false;

    void Start ()
    {
        // Inicializar os anuncios
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
    }
    public void RestartLevel ()
    {
        //Reinicia o nivel atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #region Unity Ads
    public void Load (string placementId)
    {
        Advertisement.Load(placementId);
    }

    public bool IsReady (string placementId)
    {
        return Advertisement.IsReady(placementId);
    }

    // Quando o objeto que faz o anuncio é destruido;
    public void OnDestroy ()
    {
        Advertisement.RemoveListener(this);
    }

    public void ShowAds (string placementId)
    {
        Advertisement.Show(placementId);
    }

    public void OnUnityAdsReady (string placementId)
    {
        Debug.Log("Ads Ready");
    }

    public void OnUnityAdsDidError (string message)
    {

    }

    public void OnUnityAdsDidStart (string placementId)
    {

    }

    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            if (placementId == "rewardedVideo")
                RestartLevel();
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }
    #endregion

}