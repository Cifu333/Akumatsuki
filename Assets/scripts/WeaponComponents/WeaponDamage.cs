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
    public AudioSource sound1;
    public AudioSource sound2;
    public AudioSource sound3;
    // Start is called before the first frame update
    void Start()
    {
        invulneravilityTime = 1f;
        if (gameObject.tag != "fire")
            Instantiate(sound1);
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

                if (transform.parent.GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.RIGHT)
                {
                    collision.attachedRigidbody.AddForce(new Vector2(force / collision.attachedRigidbody.mass, force / collision.attachedRigidbody.mass));
                }
                else
                {
                    collision.attachedRigidbody.AddForce(new Vector2(-force / collision.attachedRigidbody.mass, force / collision.attachedRigidbody.mass));
                }
                if (collision.GetComponent<EnemyStatus>().type != EnemyStatus.Type.TANK)
                {
                    collision.GetComponent<EnemyStatus>().stunned = true;
                    collision.GetComponent<EnemyStatus>().stunTimeCounter = 1f;
                }
            }
            if (gameObject.tag == "Weapon")
            {
                GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>().misery += collision.GetComponent<EnemyStatus>().misery;
                sound1.gameObject.GetComponent<AudioSource>().Pause();
            }
            
            if (gameObject.tag != "fire") {
                if (collision.GetComponent<EnemyStatus>().type == EnemyStatus.Type.RANGED && gameObject.tag == "Weapon")
                    Instantiate(sound3);
                else
                    Instantiate(sound2);
            }
            collision.gameObject.GetComponent<EnemyStatus>().hp -= damage;
            
        }
        if (collision.gameObject.tag == "Player" && hazardus == true)
        {
            if (collision.gameObject.GetComponent<PlayerStatus>().invulneravility == false)
            {
                collision.gameObject.GetComponent<PlayerStatus>().hp -= damage; 
                GameObject.FindGameObjectWithTag("MainCamera").transform.parent.GetComponent<camShake>().pressToShake2 = true;
                if (gameObject.tag != "Bullet" && collision.GetComponent<PlayerStatus>().hp > 0)
                {
                    if (gameObject.transform.parent.GetComponent<EnemyMovement>().dir == EnemyMovement.Direction.RIGHT)
                    {
                        collision.attachedRigidbody.AddForce(new Vector2(force / collision.attachedRigidbody.mass, force / collision.attachedRigidbody.mass));
                    }
                    else
                    {
                        collision.attachedRigidbody.AddForce(new Vector2(-force / collision.attachedRigidbody.mass, force / collision.attachedRigidbody.mass));
                    }
                    collision.gameObject.GetComponent<PlayerStatus>().stun = true;
                    collision.gameObject.GetComponent<PlayerStatus>().stunCounter = playerStun;
                }
                sound1.gameObject.GetComponent<AudioSource>().Pause();
                Instantiate(sound2);

                collision.gameObject.GetComponent<PlayerStatus>().invulneravility = true;
                collision.gameObject.GetComponent<PlayerStatus>().invulneravilityCounter = invulneravilityTime;
            }
        }
    }
}
