using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public bool attach;
    // Start is called before the first frame update
    void Start()
    {
        attach = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 7 || collision.gameObject.tag == "Enemy")
                    attach = true;
    }
}
