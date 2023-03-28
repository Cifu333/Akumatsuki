using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public float hp = 50;
    public Door d;
    public int money = 10;
    public int misery = 5;
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
<<<<<<< HEAD
            Destroy(gameObject, Time.deltaTime * 4);
=======
            Destroy(gameObject, 0);
>>>>>>> 3989b5519c7f34966f2ce41f10f06149c38a6a95
        }
    }
}
