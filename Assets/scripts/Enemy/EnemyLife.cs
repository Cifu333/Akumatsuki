using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public float hp = 50;
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
            Destroy(gameObject, 0);
        }
    }
}
