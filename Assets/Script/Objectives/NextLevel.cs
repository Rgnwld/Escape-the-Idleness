using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel: MonoBehaviour
{
    [SerializeField] KeyCollect key;
    [SerializeField] GameObject EndUI;

    [SerializeField] Sprite lockedDoor, UnlockedDoor;
    SpriteRenderer sRenderer;

    void Start ()
    {
        //    Debug.Log("A" + SceneManager.GetActiveScene().buildIndex);
        //    Debug.Log("B" + SceneManager.sceneCountInBuildSettings);
        sRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (key.isKeyCollected) sRenderer.sprite = UnlockedDoor;
        else sRenderer.sprite = lockedDoor;
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "Player" && key.isKeyCollected)
        {
            //NextLevel
            if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
            {
                EndUI.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
