using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricDoor : MonoBehaviour
{
    public bool off;
    public float offTimeCounter;
    private bool first;
    // Start is called before the first frame update
    void Start()
    {
        off = false;
        first = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (offTimeCounter > 0)
        {
            if (first)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
                first = false;
            }
            off = true;
            offTimeCounter -= Time.deltaTime;
            if (offTimeCounter < 0)
            {
                off = false;
                first = true;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
            }
        }
    }
}
