using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricDoor : MonoBehaviour
{
    public bool off;
    public float offTimeCounter;
    // Start is called before the first frame update
    void Start()
    {
        off = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (offTimeCounter > 0)
        {
            off = true;
            offTimeCounter -= Time.deltaTime;
            if (offTimeCounter < 0)
                off = false;
        }
    }
}
