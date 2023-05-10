using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerStatus>().inThorns = true;
            collision.GetComponent<PlayerStatus>().inThornsCount++;
        }
        if (collision.tag == "fire")
            Destroy(gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerStatus>().inThornsCount--;
            if (collision.GetComponent<PlayerStatus>().inThornsCount <= 0)
                collision.GetComponent<PlayerStatus>().inThorns = false;
        }
    }
}
