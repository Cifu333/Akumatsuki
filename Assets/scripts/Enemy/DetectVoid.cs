using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectVoid : MonoBehaviour
{
    public bool inVoid;
    public Vector3 ray;
    public LayerMask groundMask;
    private float groundDistance = 1.5f;
    RaycastHit2D hit;
    EnemyStatus enemyStatus;
    // Start is called before the first frame update
    void Start()
    {
        inVoid = false;
        enemyStatus = GetComponent<EnemyStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectFall();
    }

    private void DetectFall()
    {
        if (GetComponent<EnemyMovement>().dir == EnemyMovement.Direction.LEFT)
        {
            if (ray.x > 0 && enemyStatus.type != EnemyStatus.Type.RANGED)
                ray.x = -ray.x;
            else if (ray.x < 0 && enemyStatus.type == EnemyStatus.Type.RANGED)
                ray.x = -ray.x;
            Debug.DrawRay(transform.position + ray, transform.up * -1 * groundDistance, Color.red);
            hit = Physics2D.Raycast(transform.position + ray, transform.up * -1, groundDistance, groundMask);
        }
        else
        {
            if (ray.x < 0 && enemyStatus.type != EnemyStatus.Type.RANGED)
                ray.x = -ray.x;
            else if (ray.x > 0 && enemyStatus.type == EnemyStatus.Type.RANGED)
                ray.x = -ray.x;
            hit = Physics2D.Raycast(transform.position + ray, transform.up * -1, groundDistance, groundMask);
            Debug.DrawRay(transform.position + ray, transform.up * -1 * groundDistance, Color.red);
        }
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position + ray, transform.up * -1 * groundDistance, Color.green);
            inVoid = false;
        }
        else
        {
            Debug.DrawRay(transform.position + ray, transform.up * -1 * groundDistance, Color.red);
            inVoid = true;
        }
    }
}
