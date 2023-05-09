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

    public bool stunned;
    public float stunTimeCounter;
    // Start is called before the first frame update
    void Start()
    {
        ea = GetComponent<EnemyAttack>();
        free = true;
        stunned = true;
        stunTimeCounter = 2;
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
    }
}
