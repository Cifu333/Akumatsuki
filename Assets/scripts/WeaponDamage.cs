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
<<<<<<< HEAD

=======
>>>>>>> 7288fe169b7f1111e78a622c8aa4ad6ce2e35853
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && hazardus == false)
        {
            collision.gameObject.GetComponent<EnemyLife>().hp -= damage;
        }
        if (collision.gameObject.tag == "Player" && hazardus == true)
        {
            collision.gameObject.GetComponent<PlayerLife>().hp -= damage;
        }
    }
}
