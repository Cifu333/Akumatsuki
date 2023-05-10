using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool grounded;
    Rigidbody2D rb;
    [SerializeField]
    private float groundDistance = 1.5f;
    public List<Vector3> rays;
    public Vector3 rayS;
    public LayerMask groundMask;
    public float gravity;
    private bool lastG;
    public Vector3 positionS;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        int count = 0;
        int countS = 0;
        if (gameObject.tag == "Player")
        {
            if (gameObject.GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
            {
                for (int i = 0; i < rays.Count; i++)
                {
                    Debug.DrawRay(transform.position + rays[i], transform.up * -1 * groundDistance, Color.red);
                    RaycastHit2D hit = Physics2D.Raycast(transform.position + rays[i], transform.up * -1, groundDistance, groundMask);
                    if (hit.collider != null)
                    {
                        count++;
                        Debug.DrawRay(transform.position + rays[i], transform.up * -1 * hit.distance, Color.green);
                        if (hit.transform.tag == "PlataformaMovil")
                        {
                            transform.parent = hit.transform;
                        }
                        else
                        {
                            transform.parent = null;
                        }
                    }

                }

                Debug.DrawRay(transform.position + rayS, transform.up * -1 * groundDistance, Color.red);
                RaycastHit2D hitS = Physics2D.Raycast(transform.position + rayS, transform.up * -1, groundDistance, groundMask);
                if (hitS.collider != null)
                {
                    countS++;
                    Debug.DrawRay(transform.position + rayS, transform.up * -1 * hitS.distance, Color.green);
                    if (hitS.transform.tag == "PlataformaMovil")
                    {
                        transform.parent = hitS.transform;
                    }
                    else
                    {
                        transform.parent = null;
                    }
                }
            }
            else
            {
                for (int i = 0; i < rays.Count; i++)
                {
                    Debug.DrawRay(transform.position + new Vector3(-rays[i].x, rays[i].y), transform.up * -1 * groundDistance, Color.red);
                    RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(-rays[i].x, rays[i].y), transform.up * -1, groundDistance, groundMask);
                    if (hit.collider != null)
                    {
                        count++;
                        Debug.DrawRay(transform.position + new Vector3(-rays[i].x, rays[i].y), transform.up * -1 * hit.distance, Color.green);
                        if (hit.transform.tag == "PlataformaMovil")
                        {
                            transform.parent = hit.transform;
                        }
                        else
                        {
                            transform.parent = null;
                        }
                    }
                }

                Debug.DrawRay(transform.position + new Vector3(-rayS.x, rayS.y), transform.up * -1 * groundDistance, Color.red);
                RaycastHit2D hitS = Physics2D.Raycast(transform.position + new Vector3(-rayS.x, rayS.y), transform.up * -1, groundDistance, groundMask);
                if (hitS.collider != null)
                {
                    countS++;
                    Debug.DrawRay(transform.position + new Vector3(-rayS.x, rayS.y), transform.up * -1 * hitS.distance, Color.green);
                    if (hitS.transform.tag == "PlataformaMovil")
                    {
                        transform.parent = hitS.transform;
                    }
                    else
                    {
                        transform.parent = null;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < rays.Count; i++)
            {
                Debug.DrawRay(transform.position + rays[i], transform.up * -1 * groundDistance, Color.red);
                RaycastHit2D hit = Physics2D.Raycast(transform.position + rays[i], transform.up * -1, groundDistance, groundMask);
                if (hit.collider != null)
                {
                    count++;
                    Debug.DrawRay(transform.position + rays[i], transform.up * -1 * hit.distance, Color.green);
                    if (hit.transform.tag == "PlataformaMovil")
                    {
                        transform.parent = hit.transform;
                    }
                    else
                    {
                        transform.parent = null;
                    }
                }
            }
        }
        if (count > 0)
        {
            grounded = true;
            lastG = true;
        }
        if (lastG && count == 3 && countS == 1)
        {
            positionS = transform.position;
        }
        if (count <= 0)
        {
            lastG = false;
            grounded = false;
            transform.parent = null;
        }
        if (grounded == true && Input.GetAxis("Horizontal") == 0)
        {
            rb.gravityScale = 0;
        }
        else if (gameObject.tag == "Player")
        {
            if ((GetComponent<HorizontalMovement>().dash == false && GetComponent<DemonAbilities>().ability == false))
                rb.gravityScale = gravity;

        }
        else
        {
            rb.gravityScale = gravity;
        }
    }
}
