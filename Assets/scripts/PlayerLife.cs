using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public float hp = 100;
    public Vector3 respawn;
    // Start is called before the first frame update
    void Start()
    {
        respawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
