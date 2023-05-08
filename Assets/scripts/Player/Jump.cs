using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public int numJumps;
    private int jumps = 0;
    Rigidbody2D rb;
    public float force;
    public Animator anim;

    private bool grounded;

    public Direction dir = Direction.NONE;
    public enum Direction { NONE, UP, DOWN };

    GroundDetector gd;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gd = GetComponent<GroundDetector>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y < -0.1f)
        {
            dir = Direction.DOWN;
        }
        else if (rb.velocity.y > 0.1f)
        {
            dir = Direction.UP;
        }
        else 
        {
            dir = Direction.NONE;
        }
        anim.SetFloat("VelocityY",rb.velocity.y);

        if (GetComponent<PlayerAttack>().attack == false && GameObject.FindGameObjectWithTag("Token").GetComponent<Token>().justShot == false && GetComponent<PlayerStatus>().free == true)
        {
            if (grounded == true)
            {
                jumps = 0;
                if (Input.GetButtonDown("Jump"))
                {
                    rb.velocity = Vector2.zero;
                    rb.AddForce(Vector2.up * force);
                    jumps++;
                }
            }
            else
            {
                if (Input.GetButtonDown("Jump") && jumps < (numJumps - 1))
                {
                    rb.velocity = Vector2.zero;
                    rb.AddForce(Vector2.up * force);
                    jumps++;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (gd.grounded == true)
            grounded = true;
        else
            grounded = false;
    }
}
