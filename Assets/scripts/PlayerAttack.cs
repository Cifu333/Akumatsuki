using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject weapon;
    public float vRotation;
    public BoxCollider2D bc;
    public float time;
    public float angle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject temp;
            if (GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
            {
                temp = Instantiate(weapon, transform.position + new Vector3(-0.4f,0.7f,0.2f), transform.rotation);
            }
            else
            {
                temp = Instantiate(weapon, transform.position + new Vector3(0.4f, 0.7f, 0.2f), transform.rotation);
            }
            temp.transform.parent = transform;

            temp.transform.eulerAngles = Vector3.forward * angle;

            Destroy(temp, time);
        }

    }
}
