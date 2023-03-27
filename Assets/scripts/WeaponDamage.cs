using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public float damage;
    public bool  hazardus;
    public float force;
    public float playerStun;
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
            if (gameObject.tag != "Bullet")
            {
                if (collision.transform.position.x > collision.GetComponent<EnemyMovement>().target.transform.position.x)
                {
                    collision.attachedRigidbody.AddForce(new Vector2(force / collision.attachedRigidbody.mass, force / collision.attachedRigidbody.mass));
                }
                else
                {
                    collision.attachedRigidbody.AddForce(new Vector2(-force / collision.attachedRigidbody.mass, force / collision.attachedRigidbody.mass));
                }
                collision.GetComponent<EnemyMovement>().stunned = true;
                collision.GetComponent<EnemyMovement>().stunTimeCounter = collision.GetComponent<EnemyMovement>().stunTime;
            }
            if (collision.gameObject.GetComponent<EnemyLife>().hp <= 0)
            {
                gameObject.transform.parent.gameObject.GetComponent<PlayerStatus>().money += collision.gameObject.GetComponent<EnemyLife>().money;
            }
        }
        if (collision.gameObject.tag == "Player" && hazardus == true)
        {
            if (collision.gameObject.GetComponent<PlayerStatus>().invulneravility == false)
            {
                collision.gameObject.GetComponent<PlayerStatus>().hp -= damage;

                if (collision.transform.position.x > gameObject.transform.parent.gameObject.transform.position.x)
                {
                    collision.attachedRigidbody.AddForce(new Vector2(force / collision.attachedRigidbody.mass, force / collision.attachedRigidbody.mass));
                }
                else
                {
                    collision.attachedRigidbody.AddForce(new Vector2(-force / collision.attachedRigidbody.mass, force / collision.attachedRigidbody.mass));
                }
                collision.gameObject.GetComponent<HorizontalMovement>().stun = true;
                collision.gameObject.GetComponent<HorizontalMovement>().stunCounter = playerStun;

                collision.gameObject.GetComponent<PlayerStatus>().invulneravility = true;
                collision.gameObject.GetComponent<PlayerStatus>().invulneravilityCounter = collision.gameObject.GetComponent<PlayerStatus>().invulneravilityFrames;
            }
        }
    }
}
