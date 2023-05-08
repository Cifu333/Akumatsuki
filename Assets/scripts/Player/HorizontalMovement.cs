using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    public enum Direction { NONE, LEFT, RIGHT };

    public CapsuleCollider2D cd;

    DetectWall dw;

    Rigidbody2D rb;

    PlayerStatus ps;

    SpriteRenderer sr;
    Animator anim;
    GroundDetector ground;
    public Direction dir = Direction.NONE;
    public float currentSpeed = 0.0f;
    public float speed = 5;

    public float dashSpeed = 15;

    private float dashCoolCounter;
    public float dashTime = 2f;

    public float dashDuration = 0.5f;
    private float dashCounter;

    public bool dash = false;

    [SerializeField]
    public float wallDistance = 2.5f;
    public List<Vector3> rays;
    public LayerMask groundMask;

    public List<AudioSource> audios;

    private bool playSound;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        ground = GetComponent<GroundDetector>();
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponent<PlayerStatus>();
        dw = GetComponent<DetectWall>();
        dir = Direction.RIGHT;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ps.stun)
        {
            if (GetComponent<PlayerStatus>().free == true)
            {
                float horizontal = Input.GetAxis("Horizontal");
                if (Input.GetKeyDown("left shift"))
                {
                    if (dashCoolCounter <= 0 && dashCounter <= 0)
                    {
                        dash = true;
                        gameObject.GetComponent<PlayerStatus>().invulneravility = true;
                        rb.gravityScale = 0;
                        rb.velocity = Vector3.zero;
                        rb.angularVelocity = 0;
                        if (dir == Direction.RIGHT)
                        {
                            currentSpeed = dashSpeed;
                        }
                        if (dir == Direction.LEFT)
                        {
                            currentSpeed = -dashSpeed;
                        }
                        dashCounter = dashDuration;
                    }
                }

                if (dashCounter > 0)
                {
                    dashCounter -= Time.deltaTime;
                    if (dashCounter <= 0)
                    {
                        currentSpeed = horizontal * speed;
                        dashCoolCounter = dashTime;
                        gameObject.GetComponent<PlayerStatus>().invulneravility = false;
                        dash = false;
                        rb.gravityScale = ground.gravity;
                    }
                }

                if (dashCoolCounter > 0)
                {
                    dashCoolCounter -= Time.deltaTime;
                }

                if (dash == true)
                {
                    int countt = 0;
                    for (int i = 0; i < rays.Count; i++)
                    {
                        Debug.DrawRay(transform.position + rays[i], transform.right * -1 * wallDistance, Color.red);
                        RaycastHit2D hit = Physics2D.Raycast(transform.position + rays[i], transform.right * -1, wallDistance, groundMask);
                        if (hit.collider != null)
                        {
                            countt++;
                            Debug.DrawRay(transform.position + rays[i], transform.right * -1 * hit.distance, Color.green);
                        }
                    }
                    if (countt > 0)
                    {
                        horizontal = Input.GetAxis("Horizontal");
                        currentSpeed = horizontal * speed;
                    }
                }
            }
        }

        if (!ps.stun)
        {
            if (GetComponent<PlayerStatus>().free == true)
            {
                float horizontal = Input.GetAxis("Horizontal");

                if (dash == false) 
                { 
                    currentSpeed = horizontal * speed;
                }

                if (dw.wall == false)
                {
                    transform.position += new Vector3(currentSpeed * Time.deltaTime, 0, 0);
                }

                if (horizontal > 0)
                {
                    if (transform.localScale.x < 0) { transform.localScale = new Vector3(-1 * transform.localScale.x, 1 * transform.localScale.y, 0); }
                    dir = Direction.RIGHT;
                }
                if (horizontal < 0)
                {
                    if (transform.localScale.x > 0) { transform.localScale = new Vector3(-1 * transform.localScale.x, 1 * transform.localScale.y, 1); }
                    dir = Direction.LEFT;
                }


                anim.SetBool("Moving", horizontal != 0);
                anim.SetBool("Grounded", ground.grounded);
                anim.SetBool("Dashed", dash);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!ps.stun)
        {
            if (GetComponent<PlayerStatus>().free == true)
            {
                rb.velocity *= new Vector3(0, 1, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            if (dash == true)
                Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<CapsuleCollider2D>(), true);
            else
                Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<CapsuleCollider2D>(), false);
        }
    }

    private void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}
