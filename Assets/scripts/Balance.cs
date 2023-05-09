using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    public float velocity;
    public float timeMax;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        time = timeMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                velocity = -velocity;
                time = timeMax;
            }
        }
    }
}
