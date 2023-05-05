using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWall : MonoBehaviour
{
    public bool wall;
    Rigidbody2D rb;
    [SerializeField]
    private float wallDistance = 0.5f;
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
        if (gameObject.tag == "Player")
        {
            if (gameObject.GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
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
            else
            {
                for (int i = 0; i < rays.Count; i++)
                {
                    Debug.DrawRay(transform.position + new Vector3(-rays[i].x, rays[i].y), transform.right * wallDistance, Color.red);
                    RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(-rays[i].x, rays[i].y), transform.right, wallDistance, groundMask);
                    if (hit.collider != null)
                    {
                        count++;
                        Debug.DrawRay(transform.position + new Vector3(-rays[i].x, rays[i].y), transform.right * hit.distance, Color.green);
                    }
                }
            }
        }
        else
        {
            if (gameObject.GetComponent<EnemyMovement>().dir == EnemyMovement.Direction.LEFT)
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
            else
            {
                for (int i = 0; i < rays.Count; i++)
                {
                    Debug.DrawRay(transform.position + new Vector3(-rays[i].x, rays[i].y), transform.right * wallDistance, Color.red);
                    RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(-rays[i].x, rays[i].y), transform.right, wallDistance, groundMask);
                    if (hit.collider != null)
                    {
                        count++;
                        Debug.DrawRay(transform.position + new Vector3(-rays[i].x, rays[i].y), transform.right * hit.distance, Color.green);
                    }
                }
            }
        }
        if (count > 0)
        {
            wall = true;
        }
        else
        {
            wall = false;
        }
    }
}
