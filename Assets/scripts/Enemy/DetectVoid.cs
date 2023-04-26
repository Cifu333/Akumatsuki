using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectVoid : MonoBehaviour
{
    public bool inVoid;
    public Vector3 ray;
    public LayerMask groundMask;
    public float offSet;
    private float groundDistance = 1.5f;
    RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        inVoid = false;   
    }

    // Update is called once per frame
    void Update()
    {
        DetectFall();
    }

    private void DetectFall()
    {
        Debug.DrawRay(transform.position + ray, transform.up * -1 * groundDistance, Color.red);
        if (GetComponent<EnemyMovement>().dir == EnemyMovement.Direction.LEFT)
        {
            if (ray.x > 0)
                ray.x = -ray.x;
            hit = Physics2D.Raycast(transform.position + ray + new Vector3(-offSet, 0), transform.up * -1, groundDistance, groundMask);
        }
        else
        {
            if (ray.x < 0)
                ray.x = -ray.x;
            hit = Physics2D.Raycast(transform.position + ray + new Vector3(-offSet, 0), transform.up * -1, groundDistance, groundMask);
        }
        if (hit.transform.tag != "Ground" && hit.transform.tag != "Enemy")
            inVoid = true;
        else
            inVoid = false;
    }
}
