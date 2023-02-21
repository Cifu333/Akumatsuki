using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public float damage;
    public bool  hazardus;
    // Start is called before the first frame update
    void Start()
    {
        hazardus = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && hazardus == false)
        {
            collision.GetComponent<EnemyLife>().hp -= damage;
        }
        if (collision.tag == "Player" && hazardus == true)
        {
            collision.GetComponent<PlayerLife>().hp -= damage;
        }
    }
}
