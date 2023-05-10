using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panel_x : MonoBehaviour
{

    public GameObject canvas;
    public GameObject press;
    private bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        press.SetActive(false);
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange && Input.GetButtonDown("interaction"))
        {
            canvas.SetActive(true);
        }

        if(!inRange)
        {
            canvas.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        press.SetActive(true);
        inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        press.SetActive(false);
        inRange = false;
    }
}
