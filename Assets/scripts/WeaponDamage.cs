using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public float damage;
    public bool  hazardus;
    public float force;
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
        if (collision.gameObject.tag == "Enemy" && hazardus == false)
        {
            collision.gameObject.GetComponent<EnemyLife>().hp -= damage;
            collision.GetComponent<EnemyMovement>().stunned = true;
            if (collision.transform.position.x > collision.GetComponent<EnemyMovement>().target.transform.position.x)
            {
                collision.attachedRigidbody.AddForce(new Vector2(force / collision.attachedRigidbody.mass, force / collision.attachedRigidbody.mass));
            }
            else
            {
                collision.attachedRigidbody.AddForce(new Vector2(-force / collision.attachedRigidbody.mass, force / collision.attachedRigidbody.mass));
            }
        }
        if (collision.gameObject.tag == "Player" && hazardus == true)
        {
            collision.gameObject.GetComponent<PlayerLife>().hp -= damage;
        }
    }
}
