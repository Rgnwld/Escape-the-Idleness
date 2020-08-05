using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject exitGO;
    [SerializeField] bool changeRotation;
    Vector3 exitPos;

    void Start ()
    {
        exitPos = exitGO.transform.position;
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.tag == "Player")
        {
            col.gameObject.transform.position = exitPos;
            if (changeRotation)
            {
                col.GetComponent<PlayerMovement>().HORIZONTAL_MOVE *= -1;
            }
        }
    }
}
