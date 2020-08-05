using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerScript : MonoBehaviour
{

    public string gameId = "3699407";
    public string placementId = "banner01";
    public bool testMode = false;

    void Start ()
    {
        Advertisement.Load("banner01");
        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady ()
    {
        while (!Advertisement.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(placementId);
    }
}