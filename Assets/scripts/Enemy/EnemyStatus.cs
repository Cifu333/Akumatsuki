using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public enum Type { MELEE, RANGED, TANK, FLYING, BOSS };
    public Type type;
    public float hp = 50;
    public int money = 10;
    public int misery = 5;
    public EnemyAttack ea;
    public bool free;
    private bool burn;

    public bool stunned;
    public float stunTimeCounter;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        ea = GetComponent<EnemyAttack>();
        free = true;
        stunned = true;
        stunTimeCounter = 2;
        time = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (ea.attack || stunned)
            free = false;
        else
            free = true;
        if (stunned == true)
        {
            stunTimeCounter -= Time.deltaTime;
            if (stunTimeCounter <= 0)
            {
                stunned = false;
                if (gameObject.layer != 7)
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
        Muerte();

        if (burn)
        {
            time += Time.deltaTime;
            if (time >= 1)
            {
                time = 0;
                hp -= 15;
            }
        }
    }



    private void Muerte()
    {
        if (hp <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>().money += money;
            Destroy(gameObject, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Destroy(gameObject, 0);
        }
        if (collision.gameObject.tag == "fire")
        {
            burn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "fire")
        {
            burn = false;
        }

    }
}
