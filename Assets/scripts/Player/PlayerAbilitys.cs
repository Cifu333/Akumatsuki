using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitys : MonoBehaviour
{
    public List<bool> human = new List<bool>(4);
    public List<bool> demon = new List<bool>(4);
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < human.Count; i++)
        {
            human[i] = false;
        }
        for (int i = 0; i < demon.Count; i++)
        {
            demon[i] = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
