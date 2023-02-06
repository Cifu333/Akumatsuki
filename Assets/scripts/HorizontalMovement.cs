using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    public enum Direction { NONE, LEFT, RIGHT };

    public SpriteRenderer sr;
    public Animator anim;
    public GroundDetector ground;
    public Direction dir = Direction.NONE;
    public float currentSpeed = 0.0f;
    public float speed = 5;
    public float dashSpeed = 3;
    private bool dash;
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
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        currentSpeed = horizontal * speed;
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
        if (Input.GetButtonDown("LeftShift"))
        {
            if (horizontal > 0)
            {
                transform.position += new Vector3(currentSpeed * Time.fixedDeltaTime * dashSpeed, 0, 0);
            }
            if (horizontal < 0)
            {
                transform.position += new Vector3(currentSpeed * Time.fixedDeltaTime * dashSpeed, 0, 0);
            }
            else
            {
                transform.position += new Vector3(currentSpeed * Time.fixedDeltaTime * dashSpeed, 0, 0);
            }
        }
        anim.SetBool("Moving", horizontal != 0);
        anim.SetBool("Grounded", ground.grounded);
    }

    IEnumerator Dash_corrutine()
    {
        dash = false;
        yield return new WaitForSeconds(2);
        dash = true;
    }

}
