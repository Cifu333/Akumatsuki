using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject weapon;
    public float vRotation;
    public float offset;
    public bool lightAttack;

    private float lightAttackCoolCounter;
    private float lightAttackCounter;
    public float lightAttackDuration;
    public float lightAttackTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject temp = weapon;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (lightAttackCoolCounter <= 0 && lightAttackCounter <= 0)
            {
                lightAttack = true;
                if (GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
                {
                    temp = Instantiate(weapon, transform.position + new Vector3(-offset, 0, 0), transform.rotation);
                }
                else
                {
                    temp = Instantiate(weapon, transform.position + new Vector3(offset, 0, 0), transform.rotation);
                }
                temp.transform.parent = transform;
                lightAttackCounter = lightAttackDuration;
                Destroy(temp, lightAttackDuration);
            }
        }

        if (lightAttackCounter > 0)
        {
            lightAttackCounter -= Time.deltaTime;
            if (lightAttackCounter <= 0)
            {
                lightAttackCoolCounter = lightAttackTime;
                lightAttack = false;
            }
        }

        if (lightAttackCounter > 0)
        {
            lightAttackCounter -= Time.deltaTime;
        }

    }
}
