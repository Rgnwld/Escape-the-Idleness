using UnityEngine;

public class FireBehavior : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.tag == "Player" && !col.GetComponent<LifeBehavior>().isDead)
        {
            col.GetComponent<LifeBehavior>().Die();
        }
    }
}
