using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public int BabyPartsNecessarys;
    private bool done;
    private bool inContact;
    public GameObject soil;
    // Start is called before the first frame update
    void Start()
    {
        done = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inContact && done && Input.GetKey(KeyCode.X))
        {
            soil.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inContact = true;
            if (collision.GetComponent<PlayerStatus>().babyParts == BabyPartsNecessarys)
                done = true;

        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inContact = false;
        }
    }
}