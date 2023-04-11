using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public float damage;
    public bool  hazardus;
    public float force;
    public float playerStun;
    public float invulneravilityTime;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        invulneravilityTime = 1f;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && hazardus == false)
        {
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
                collision.GetComponent<EnemyMovement>().stunTimeCounter = 2f;
            }
            if (gameObject.tag == "Weapon")
                GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>().misery += collision.GetComponent<EnemyStatus>().misery;
            if (gameObject.tag != "Fire")
                collision.gameObject.GetComponent<EnemyStatus>().hp -= damage;
            
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
                collision.gameObject.GetComponent<PlayerStatus>().invulneravilityFrames = invulneravilityTime;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.tag == "Fire" && collision.gameObject.tag == "Enemy")
        {
            time += Time.deltaTime;
            if (time >= 1)
            {
                collision.gameObject.GetComponent<EnemyStatus>().hp -= damage;
                time = 0;
            }
        }
    }
}
