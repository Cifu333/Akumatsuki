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

            }
            off = true;
            offTimeCounter -= Time.deltaTime;
            if (offTimeCounter < 0)
                off = false;
        }
    }
}
