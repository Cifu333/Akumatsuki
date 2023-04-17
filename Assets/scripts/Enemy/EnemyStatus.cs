using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public enum Type { MELEE, RANGED, TANK, FLYING };
    public Type type;
    public float hp = 50;
    public int money = 10;
    public int misery = 5;
    public EnemyAttack ea;
    public bool free;
    // Start is called before the first frame update
    void Start()
    {
        ea = GetComponent<EnemyAttack>();
        free = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ea.attack == true)
            free = false;
        else
            free = true;

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
}
