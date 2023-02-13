using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    public enum Direction { NONE, LEFT, RIGHT };

    public CapsuleCollider2D cd;

    public SpriteRenderer sr;
    public Animator anim;
    public GroundDetector ground;
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

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        ground = GetComponent<GroundDetector>();
        dir = Direction.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown("left shift"))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                dash = true;
                if (horizontal > 0)
                {
                    currentSpeed = dashSpeed;
                }
                if (horizontal < 0)
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
                dash = false;
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

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (dash == false) { currentSpeed = horizontal * speed; }

        transform.position += new Vector3(currentSpeed * Time.fixedDeltaTime, 0, 0);

        if(horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            dir = Direction.RIGHT;
        }
        if(horizontal < 0)
        {
            transform.localScale = new Vector3(1,-1,1);
            dir = Direction.LEFT;
        }


        anim.SetBool("Moving", horizontal != 0);
        anim.SetBool("Grounded", ground.grounded);
    }
}
