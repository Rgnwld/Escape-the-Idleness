using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] LayerMask clickableLayer;
    [SerializeField] [Range(0, 10)]
    float speed, jumpForce, fallMultiplier = 2.5f, lowJumpMultiplier = 2.5f;

    public float HORIZONTAL_MOVE = 1;
    public Rigidbody2D rb2d;
    [HideInInspector] public bool canJump = false;

    //AudioSource
    [Header("Sons")]
    //Só pra pular
    [SerializeField] AudioSource aJump;
    //Outros Sons
    public AudioSource aSource;

    public AudioClip jump, getHit, spawn, hitWall;

    void Start ()
    {
        if (GameObject.Find("ClickableSurface") != null)
        {
            aSource.PlayOneShot(spawn);
            aJump.clip = jump;
        }
    }

    void FixedUpdate()
    {
        Movement();
        if(GameObject.Find("ClickableSurface") != null)
        {
            Jump();
        }
    }

    void Movement ()
    {
        rb2d.velocity = new Vector2(HORIZONTAL_MOVE * speed, rb2d.velocity.y);
    }

    void Jump ()
    {
        if (Input.touchCount > 0 && canJump)
        {
            Touch t1 = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(t1.position);

            if (Physics.Raycast(ray, 100, clickableLayer.value))
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);

            //Sound clips
            aJump.Play();
        }

        if ((Input.GetMouseButton(0)) && canJump)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, 100, clickableLayer.value))
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);

            //Sound clips;
            aJump.Play();
        }

        if (rb2d.velocity.y < 0)
        {
            rb2d.velocity += Vector2.up * fallMultiplier * Physics2D.gravity.y * Time.deltaTime;
            if (rb2d.velocity.y < -10)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, -10);
            }

            //Sound;
            if (aJump.clip == jump)
                aJump.Stop();
        }

        if (rb2d.velocity.y > 0 && (Input.touchCount <= 0 && !Input.GetMouseButton(0)))
        {
            rb2d.velocity += Vector2.up * lowJumpMultiplier * Physics2D.gravity.y * Time.deltaTime;

            //Sound;
            if (aJump.clip == jump && rb2d.velocity.y < 0)
                aJump.Stop();
        }
    }
}
