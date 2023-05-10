using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panels : MonoBehaviour
{

    public GameObject canva;
    public GameObject XIsPresd;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        XIsPresd.SetActive(true);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        XIsPresd.SetActive(false);
    }
}
