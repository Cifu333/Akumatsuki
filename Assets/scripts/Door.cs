using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int numEnemys;
    public bool win;
    // Start is called before the first frame update
    void Start()
    {
        win = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && numEnemys <= 0)
        {
            win = true;
            Destroy(collision.gameObject);
        }
        
    }
}