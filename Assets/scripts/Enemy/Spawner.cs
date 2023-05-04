using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public bool spawn;
    GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        spawn = true;
        GameObject.FindGameObjectWithTag("SpawnerHandler").GetComponent<SpawnerHandler>().spawnList.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn == true)
        {
            if (temp != null)
                Destroy(temp);
            temp = Instantiate(enemy, transform.position, transform.rotation);
            spawn = false;
        }
    }
}
