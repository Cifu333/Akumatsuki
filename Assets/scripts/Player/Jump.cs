using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public int numJumps;
    private int jumps = 0;
    public GameObject target;
    Rigidbody2D rb;
    public float force;
    public bool fall = false;

    private GroundDetector targetGroundDetectorComponent;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y < -0.1f)
        {
            fall = true;
        }
        else
        {
            fall = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        targetGroundDetectorComponent = target.GetComponent<GroundDetector>();
        if (targetGroundDetectorComponent.grounded == true)
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
