using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] PlayerMovement pm;
    [SerializeField] LayerMask LayerToHide;
    SpriteRenderer playerSprite;

    void Start ()
    {
        //Inicialização de jogar a cada cena nova;
        Time.timeScale = 1;
        playerSprite = transform.Find("body").GetComponent<SpriteRenderer>();
     
    }

    void FixedUpdate ()
    {
        BetterCollisions();
        SpriteUpdate();
    }

    //Atualiza o sprite do jogador de acordo com a direção que ele anda;
    void SpriteUpdate ()
    {
        if (pm.HORIZONTAL_MOVE > 0)
        {
            playerSprite.flipX = false;
        }
        else
        {
            playerSprite.flipX = true;
        }
    }

    //Colisão melhora por Raycast;
    void BetterCollisions ()
    {
        //  Floor
        RaycastHit2D hitFloor = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), Mathf.Infinity, LayerToHide.value);
        if (hitFloor.collider != null)
        {
            Debug.DrawRay(transform.position, Vector2.down * hitFloor.distance, Color.yellow);
            if (hitFloor.distance < 0.21f)
            {
                pm.canJump = true;
            }
            else
            {
                pm.canJump = false;
            }
        }

        //  Wall
        RaycastHit2D hitWallLeft = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), Mathf.Infinity, LayerToHide.value);
        RaycastHit2D hitWallRight = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), Mathf.Infinity, LayerToHide.value);

        if (hitWallLeft.collider != null)
        {
            Debug.DrawRay(transform.position, Vector2.left * hitWallLeft.distance, Color.yellow);
            if (hitWallLeft.distance < 0.21f)
            {
                pm.aSource.PlayOneShot(pm.hitWall);
                pm.HORIZONTAL_MOVE = 1;
            }
        }

        if (hitWallRight.collider != null)
        {
            Debug.DrawRay(transform.position, Vector2.right * hitWallRight.distance, Color.yellow);
            if (hitWallRight.distance < 0.21f)
            {
                pm.aSource.PlayOneShot(pm.hitWall);
                pm.HORIZONTAL_MOVE = -1;
            }
        }

    }    
}

