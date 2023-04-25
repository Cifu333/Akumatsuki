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

    public bool stunned;
    public float stunTimeCounter;

    public GameObject target;

    Rigidbody2D rb;

    public EnemyStatus es;

    public SpriteRenderer sr;
    GroundDetector ground;
    public Direction dir = Direction.NONE;
    public float currentSpeed = 0.0f;
    public float speed = 5;
    public float distance;
    public float difX;
    public float difY;
    public float detect;

    public int numJumps;

    Animator anim;

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
        es = GetComponent<EnemyStatus>();
        target = GameObject.FindGameObjectWithTag("Player");
        stunned = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stunned)
        {
            switch (es.type)
            {
                case EnemyStatus.Type.MELEE:
                    MeleeMovement();
                    break;
                case EnemyStatus.Type.RANGED:
                    RangedMovement();
                    break;
                case EnemyStatus.Type.FLYING:
                    FlyingMovement();
                    break;
                case EnemyStatus.Type.TANK:
                    MeleeMovement();
                    break;

            }
            if (dir == Direction.RIGHT && transform.localScale.x < 0)
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
            if (dir == Direction.LEFT && transform.localScale.x > 0)
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
        }
        else
        {
            anim.SetBool("Movement", false);
            stunTimeCounter -= Time.deltaTime;
            if (stunTimeCounter <= 0)
            {
                stunned = false;
                rb.velocity = Vector2.zero;
            }
        }

        //anim.SetBool("Moving", speed != 0);
        //anim.SetBool("Grounded", ground.grounded);
    }

    private void FixedUpdate()
    {
        if (!stunned)
        {
            if (target != null)
            {
                if (es.type != EnemyStatus.Type.FLYING)
                    rb.velocity *= new Vector3(0, 1, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<CapsuleCollider2D>(), true);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<CapsuleCollider2D>(), true);
        }
    }

    private void MeleeMovement()
    {
        if (target != null)
        {
            if (es.free == true)
            {
                currentSpeed = speed;
                difX = transform.position.x - target.transform.position.x;
                if (difX < 0) { difX = -difX; }
                if (transform.position.x < target.transform.position.x && difX < detect)
                {
                    if (transform.localScale.x < 0) { transform.localScale = new Vector3(1 * transform.localScale.x, 1 * transform.localScale.y, 0); }
                    if (difX > distance)
                    {
                        transform.position += new Vector3(currentSpeed * Time.deltaTime, 0, 0);
                        anim.SetBool("Movement", true);
                    }
                    dir = Direction.RIGHT;
                }
                else if (transform.position.x > target.transform.position.x && difX < detect)
                {
                    if (transform.localScale.x > 0) { transform.localScale = new Vector3(-1 * transform.localScale.x, 1 * transform.localScale.y, 1); }
                    if (difX > distance)
                    {
                        transform.position += new Vector3(-currentSpeed * Time.deltaTime, 0, 0);
                        anim.SetBool("Movement", true);
                    }
                    dir = Direction.LEFT;
                }
                else
                    anim.SetBool("Movement", false);


            }
        }
    }
    private void FlyingMovement()
    {
        if (target != null)
        {
            if (es.free == true)
            {
                currentSpeed = speed;
                difX = transform.position.x - target.transform.position.x;
                difY = transform.position.y - target.transform.position.y;
                if (difX < 0) { difX = -difX; }
                if (difY < 0) { difY = -difY; }
                if (transform.position.x < target.transform.position.x && difX < detect && difY < detect)
                {
                    if (transform.localScale.x < 0) { transform.localScale = new Vector3(1 * transform.localScale.x, 1 * transform.localScale.y, 0); }
                    if (difX > distance)
                        transform.position += new Vector3(currentSpeed * Time.deltaTime, 0, 0);
                    dir = Direction.RIGHT;
                }
                if (transform.position.x > target.transform.position.x && difX < detect && difY < detect)
                {
                    if (transform.localScale.x > 0) { transform.localScale = new Vector3(-1 * transform.localScale.x, 1 * transform.localScale.y, 1); }
                    if (difX > distance)
                        transform.position += new Vector3(-currentSpeed * Time.deltaTime, 0, 0);
                    dir = Direction.LEFT;
                }

                if (transform.position.y > target.transform.position.y && difX < detect && difY < detect)
                {
                    if (difY > distance)
                        transform.position += new Vector3(0, -currentSpeed * Time.deltaTime, 0);
                }
                if (transform.position.y < target.transform.position.y && difX < detect && difY < detect)
                {
                    if (difY > distance)
                        transform.position += new Vector3(0, currentSpeed * Time.deltaTime, 0);
                }

            }
        }
    }

    private void RangedMovement()
    {
        if (target != null)
        {
            if (es.free == true)
            {
                currentSpeed = speed;
                difX = transform.position.x - target.transform.position.x;
                if (difX < 0) { difX = -difX; }
                if (transform.position.x < target.transform.position.x && difX < detect)
                {
                    if (transform.localScale.x < 0) { transform.localScale = new Vector3(1 * transform.localScale.x, 1 * transform.localScale.y, 0); }
                    if (difX < distance)
                        transform.position += new Vector3(-currentSpeed * Time.deltaTime, 0, 0);
                    dir = Direction.RIGHT;
                }
                if (transform.position.x > target.transform.position.x && difX < detect)
                {
                    if (transform.localScale.x > 0) { transform.localScale = new Vector3(-1 * transform.localScale.x, 1 * transform.localScale.y, 1); }
                    if (difX < distance)
                        transform.position += new Vector3(currentSpeed * Time.deltaTime, 0, 0);
                    dir = Direction.LEFT;
                }

            }
        }
    }
}

/*
 //Salto
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
 */
