using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemTex : MonoBehaviour
{
    public GameObject Explosion;
    public float rotate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.eulerAngles += new Vector3(0, 0, rotate) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 7)
        {
            if (collision.gameObject.tag == "ElectricDoor")
            {
                collision.GetComponent<ElectricDoor>().offTimeCounter = 3;
            }
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
