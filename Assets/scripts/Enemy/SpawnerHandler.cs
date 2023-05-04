using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHandler : MonoBehaviour
{
    public bool spawnAll;
    public List<GameObject> spawnList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        spawnAll = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnAll == true)
        {
            for (int i = 0; i < spawnList.Count; i++)
            {
                spawnList[i].GetComponent<Spawner>().spawn = true;
            }
            spawnAll = false;
        }
    }
}
