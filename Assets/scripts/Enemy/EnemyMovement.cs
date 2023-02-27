using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyMovement : MonoBehaviour
{
    public enum Direction { NONE, LEFT, RIGHT };

    public float force;

    public GameObject target;

    Rigidbody2D rb;

    EnemyAttack ea;

    public SpriteRenderer sr;
    GroundDetector ground;
    public Direction dir = Direction.NONE;
    public float currentSpeed = 0.0f;
    public float speed = 5;
    public float distance;
    public float dif;
    public float detect;

    public int numJumps;



    [SerializeField]
    public float wallDistance = 2.5f;
    public List<Vector3> rays;
    public LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ground = GetComponent<GroundDetector>();
        dir = Direction.NONE;
        rb = GetComponent<Rigidbody2D>();
        ea = GetComponent<EnemyAttack>();   
    }

    // Update is called once per frame
    void Update()
    {
        int jumps = 0;

        int count = 0;
        if (dir == Direction.LEFT)
        {
            for (int i = 0; i < rays.Count; i++)
            {
                Debug.DrawRay(transform.position + rays[i], transform.right * -1 * wallDistance, Color.red);
                RaycastHit2D hit = Physics2D.Raycast(transform.position + rays[i], transform.right * -1, wallDistance, groundMask);

                if (hit.collider != null)
                {
                    count++;
                    Debug.DrawRay(transform.position + rays[i], transform.right * -1 * hit.distance, Color.green);
                }
            }
        }
        if (dir == Direction.RIGHT)
        {
            for (int i = 0; i < rays.Count; i++)
            {
                Debug.DrawRay(transform.position + rays[i], transform.right * 1 * wallDistance, Color.red);
                RaycastHit2D hit = Physics2D.Raycast(transform.position + rays[i], transform.right * 1, wallDistance, groundMask);

                if (hit.collider != null)
                {
                    count++;
                    Debug.DrawRay(transform.position + rays[i], transform.right * 1 * hit.distance, Color.green);
                }
            }
        }
        if (count > 0 && jumps > 0)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * force);
            jumps--;
        }

        ground = GetComponent<GroundDetector>();

        if (ground.grounded == true)
        {
            jumps = numJumps;
            if (count > 0)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * force);
            }
        }


        //anim.SetBool("Moving", speed != 0);
        //anim.SetBool("Grounded", ground.grounded);
    }

    private void FixedUpdate()
    {
        if (ea.attack == false) {
            currentSpeed = speed;
            dif = transform.position.x - target.transform.position.x;
            if (dif < 0) { dif = -dif; }
            if (transform.position.x < target.transform.position.x && dif > distance && dif < detect) {
                if (transform.localScale.x < 0) { transform.localScale = new Vector3(1 * transform.localScale.x, 1 * transform.localScale.y, 0); }
                transform.position += new Vector3(currentSpeed * Time.fixedDeltaTime, 0, 0);
                dir = Direction.RIGHT;
            }
            if (transform.position.x > target.transform.position.x && dif > distance && dif < detect) {
                if (transform.localScale.x > 0) { transform.localScale = new Vector3(-1 * transform.localScale.x, 1 * transform.localScale.y, 1); }

                transform.position += new Vector3(-currentSpeed * Time.fixedDeltaTime, 0, 0);

                dir = Direction.LEFT;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<CapsuleCollider2D>(), true);
        }

        if (collision.gameObject.tag == "Enemys")
        {
            Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<CapsuleCollider2D>(), true);
        }
    }
}

