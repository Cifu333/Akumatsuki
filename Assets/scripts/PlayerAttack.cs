using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject weapon;
    public float vRotation = 10;
    public float offset = 1.1f;
    public bool lightAttack = false;
    public bool heavyAttack = false;

    private float lightAttackCoolCounter;
    private float lightAttackCounter;
    public float lightAttackDuration = 0.25f;
    public float lightAttackTime = 0.25f;

    private bool charge;
    private float heavyAttackCoolCounter;
    private float heavyAttackCounter;
    private float heavyAttackChargeCounter;
    public float heavyAttackDuration = 0.5f;
    public float heavyAttackTime = 0.5f;
    public float heavyAttackCharge = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        charge = true;
    }

    // Update is called once per frame
    void Update()
    {
        LightAttack();
        HeavyAttack();

    }

    private void LightAttack()
    {
        GameObject temp;
        if (Input.GetKeyDown(KeyCode.E) && heavyAttack == false)
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
                lightAttackCounter = lightAttackDuration;
                temp.transform.parent = transform;
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

        if (lightAttackCoolCounter > 0f)
        {
            lightAttackCoolCounter -= Time.deltaTime;
        }
    }

    private void HeavyAttack()
    {
        GameObject temp;
        if (Input.GetKeyDown(KeyCode.Q) && lightAttack == false)
        {
            if (heavyAttackCoolCounter <= 0 && heavyAttackCounter <= 0)
            {
                if (charge == true)
                {
                    heavyAttack = true;
                    heavyAttackChargeCounter = heavyAttackCharge;
                    charge = false;
                }
                
            }
        }
        if (heavyAttackChargeCounter > 0)
        {
            heavyAttackChargeCounter -= Time.deltaTime;
            if (heavyAttackChargeCounter <= 0)
            {
                if (GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
                {
                    temp = Instantiate(weapon, transform.position + new Vector3(-offset, 0, 0), transform.rotation);
                }
                else
                {
                    temp = Instantiate(weapon, transform.position + new Vector3(offset, 0, 0), transform.rotation);
                }
                heavyAttackCounter = heavyAttackDuration;
                temp.transform.parent = transform;
                Destroy(temp, heavyAttackDuration);
            }
        }

        if (heavyAttackCounter > 0)
        {
            heavyAttackCounter -= Time.deltaTime;
            if (heavyAttackCounter <= 0)
            {
                heavyAttackCoolCounter = heavyAttackTime;
                heavyAttack = false;
            }
        }

        if (heavyAttackCoolCounter > 0f)
        {
            heavyAttackCoolCounter -= Time.deltaTime;
            charge = true;
        }

    }
}
