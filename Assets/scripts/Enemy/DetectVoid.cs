using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectVoid : MonoBehaviour
{
    public bool inVoid;
    public List<Vector3> rays;
    public LayerMask groundMask;
    public float offSide;
    // Start is called before the first frame update
    void Start()
    {
        inVoid = false;   
    }

    // Update is called once per frame
    void Update()
    {
        DetectVoid();
    }

    private void DetectVoid()
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
}
