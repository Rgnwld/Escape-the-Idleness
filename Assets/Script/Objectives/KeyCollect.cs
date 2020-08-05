using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollect : MonoBehaviour
{
    GameObject GameCanvas;
    SpriteRenderer KeyUI;
    [SerializeField] AudioClip pickUp;
    AudioSource aSource;

    public bool isKeyCollected = false;

    void Start ()
    {
        aSource = GetComponent<AudioSource>();
        KeyUI = GameObject.Find("GameCanvas").transform.Find("Objectives").Find("Key").GetComponent<SpriteRenderer>();
    }

    void CollectKey ()
    {
        isKeyCollected = true;
        KeyUI.color = new Color(255, 255, 255, 255);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 0.5f);
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "Player")
        {
            aSource.PlayOneShot(pickUp);
            CollectKey();
        }
    }
}
