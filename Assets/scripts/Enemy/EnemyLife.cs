using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public float hp = 50;
    public Door d;
    public int money = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Muerte();
    }



    private void Muerte()
    {
        if (hp <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>().money += money;
            d.numEnemys--;
            Destroy(gameObject, Time.deltaTime * 4);
        }
    }
}
