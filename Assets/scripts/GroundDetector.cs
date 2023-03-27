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
    public LayerMask groundMask;
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
        if (count > 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
            transform.parent = null;
        }
        if (grounded == true && Input.GetAxis("Horizontal") == 0)
        {
            rb.gravityScale = 0;
        }
        else if (gameObject.tag == "Player")
        {
            if (GetComponent<HorizontalMovement>().dash == false && GetComponent<PlayerStatus>().free == true && GetComponent<DemonAbilities>().ability == false)
                rb.gravityScale = 8;

        }
        else
        {
            rb.gravityScale = 8;
        }
    }
}
