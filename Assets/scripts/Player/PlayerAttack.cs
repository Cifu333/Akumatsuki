using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject weapon;
    private GameObject temp;

    public float offset = 1.1f;

    PlayerStatus ps;

    Rigidbody2D rb;

    private float time;

    public GameObject heavyWeapon;
    public bool attack;
    public bool lightAttack;
    public bool heavyAttack;

    private float lightAttackCoolCounter;
    public float lightAttackTime = 0.5f;

    public bool charge;

    private float heavyAttackCoolCounter;
    private float heavyAttackChargeCounter;
    public float heavyAttackTime = 1f;
    public float heavyAttackCharge = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        charge = true;
        lightAttack = false;
        heavyAttack = false;
        ps = GetComponent<PlayerStatus>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LightAttack();
        HeavyAttack();
    }

    private void LightAttack()
    {
        if (Input.GetKeyDown(KeyCode.E) && ps.free == true)
        {
            if (lightAttackCoolCounter <= 0)
            {
                lightAttack = true;
                attack = true;
                if (GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
                {
                    temp = Instantiate(weapon, transform.position + new Vector3(-offset, 0, 0), transform.rotation);
                }
                else
                {
                    temp = Instantiate(weapon, transform.position + new Vector3(offset, 0, 0), transform.rotation);
                }
                lightAttackCoolCounter = lightAttackTime;
                temp.transform.parent = transform;
            }
        }

        if (GetComponent<GroundDetector>().grounded == false && attack == true)
        {
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0;
            time += Time.deltaTime;
            if (time >= 0.3)
                rb.gravityScale = 8;
        }

        if (lightAttack == true)
        {
            lightAttack = false;
            Destroy(temp, Time.fixedDeltaTime);
        }

        if (lightAttackCoolCounter > 0f)
        {
            lightAttackCoolCounter -= Time.deltaTime;
            if (lightAttackCoolCounter <= 0)
            {
                attack = false;
            }
        }
    }

    private void HeavyAttack()
    {
        if (Input.GetKeyDown(KeyCode.Q) && ps.free == true)
        {
            if (heavyAttackCoolCounter <= 0)
            {
                if (charge == true)
                {
                    heavyAttack = true;
                    attack = true;
                    heavyAttackChargeCounter = heavyAttackCharge;
                    charge = false;
                }

            }
        }

        if (GetComponent<GroundDetector>().grounded == false && attack == true)
        {
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0;
            time += Time.deltaTime;
            if (time >= 0.3)
                rb.gravityScale = 8;
        }

        if (heavyAttackChargeCounter > 0)
        {
            heavyAttackChargeCounter -= Time.deltaTime;
            if (heavyAttackChargeCounter <= 0)
            {
                if (GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
                {
                    temp = Instantiate(heavyWeapon, transform.position + new Vector3(-(offset + 0.65f), 0, 0), transform.rotation);
                }
                else
                {
                    temp = Instantiate(heavyWeapon, transform.position + new Vector3(offset + 0.65f, 0, 0), transform.rotation);
                }
                heavyAttackCoolCounter = heavyAttackTime;
                temp.transform.parent = transform;
            }
        }

        if (heavyAttack == true)
        {
            heavyAttack = false;
            Destroy(temp, Time.fixedDeltaTime);
        }

        if (heavyAttackCoolCounter > 0f)
        {
            heavyAttackCoolCounter -= Time.deltaTime;
            charge = true;
            attack = false;
        }

    }

    
}


