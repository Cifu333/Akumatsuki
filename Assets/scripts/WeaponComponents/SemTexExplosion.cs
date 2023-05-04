using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemTexExplosion : MonoBehaviour
{
    public float fadeInTime;
    public float stunTime;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr =   GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.color -= new Color(0,0,0,fadeInTime) * Time.deltaTime;
        if (sr.color.a <= 0)
        {
            Destroy(gameObject, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.GetComponent<EnemyStatus>().type == EnemyStatus.Type.BOSS)
            { 

            }
            collision.gameObject.GetComponent<EnemyStatus>().stunTimeCounter = stunTime;
            collision.gameObject.GetComponent<EnemyStatus>().stunned = true;
        }
    }

}
