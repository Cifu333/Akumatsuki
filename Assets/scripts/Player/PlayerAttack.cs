using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject weapon;

    public float offset = 1.1f;

    private Disparo bullet;

    PlayerAbilitys pa;

    public GameObject heavyWeapon;
    public bool attack;
    public bool lightAttack;
    public bool heavyAttack;
    public bool tentacleAttack;

    private float lightAttackCoolCounter;
    private float lightAttackCounter;
    public float lightAttackDuration = 0.25f;
    public float lightAttackTime = 0.25f;

    public bool charge;

    private float heavyAttackCoolCounter;
    private float heavyAttackCounter;
    private float heavyAttackChargeCounter;
    public float heavyAttackDuration = 0.5f;
    public float heavyAttackTime = 0.5f;
    public float heavyAttackCharge = 0.3f;

    private float tentacleAttackCoolCounter;
    private float tentacleAttackCounter;
    private float tentacleAttackChargeCounter;
    public float tentacleAttackDuration = 0.75f;
    public float tentacleAttackTime = 0.5f;
    public float tentacleAttackCharge = 0.2f;
    public float tentacleTranslation = 0.3f;
    private GameObject temp;


    // Start is called before the first frame update
    void Start()
    {
        charge = true;
        lightAttack = false;
        heavyAttack = false;
        tentacleAttack = false;
        bullet = GetComponent<Disparo>();
        pa = GetComponent<PlayerAbilitys>();
    }

    // Update is called once per frame
    void Update()
    {
        LightAttack();
        HeavyAttack();
        TentacleAttack();
    }

    private void LightAttack()
    {
        if (Input.GetKeyDown(KeyCode.E) && attack == false && bullet.bullet == false)
        {
            if (lightAttackCoolCounter <= 0 && lightAttackCounter <= 0)
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
                lightAttackCounter = lightAttackDuration;
                if (GetComponent<GroundDetector>().grounded == false)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * (GetComponent<Jump>().force / 4));
                }
                temp.transform.parent = transform;
            }
        }

        if (lightAttackCounter > 0)
        {
            lightAttackCounter -= Time.deltaTime;
            if (lightAttackCounter <= 0)
            {
                lightAttackCoolCounter = lightAttackTime;
                lightAttack = false;
                Destroy(temp, 0);
            }
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
        if (Input.GetKeyDown(KeyCode.Q) && attack == false && bullet.bullet == false)
        {
            if (heavyAttackCoolCounter <= 0 && heavyAttackCounter <= 0)
            {
                if (charge == true)
                {
                    heavyAttack = true;
                    attack = true;
                    heavyAttackChargeCounter = heavyAttackCharge;
                    charge = false;
                }
                if (GetComponent<GroundDetector>().grounded == false)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * (GetComponent<Jump>().force / 4));
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
                    temp = Instantiate(heavyWeapon, transform.position + new Vector3(-(offset + 0.65f), 0, 0), transform.rotation);
                }
                else
                {
                    temp = Instantiate(heavyWeapon, transform.position + new Vector3(offset + 0.65f, 0, 0), transform.rotation);
                }
                heavyAttackCounter = heavyAttackDuration;
                temp.transform.parent = transform;
            }
        }

        if (heavyAttackCounter > 0)
        {
            heavyAttackCounter -= Time.deltaTime;
            if (heavyAttackCounter <= 0)
            {
                heavyAttackCoolCounter = heavyAttackTime;
                heavyAttack = false;
                Destroy(temp, 0);
            }
        }

        if (heavyAttackCoolCounter > 0f)
        {
            heavyAttackCoolCounter -= Time.deltaTime;
            charge = true;
            attack = false;
        }

    }

    private void TentacleAttack()
    {
        if (Input.GetKeyDown(KeyCode.F) && attack == false && bullet.bullet == false && pa.demon[0] == true)
        {
            if (tentacleAttackCoolCounter <= 0 && tentacleAttackCounter <= 0)
            {
                if (charge == true)
                {
                    tentacleAttack = true;
                    attack = true;
                    tentacleAttackChargeCounter = tentacleAttackCharge;
                    charge = false;
                }
                if (GetComponent<GroundDetector>().grounded == false)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * (GetComponent<Jump>().force / 4));
                }

            }
        }
        if (tentacleAttackChargeCounter > 0)
        {
            tentacleAttackChargeCounter -= Time.deltaTime;
            if (tentacleAttackChargeCounter <= 0)
            {
                if (GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
                {
                    temp = Instantiate(weapon, transform.position + new Vector3(-(offset + 0.65f), 0, 0), transform.rotation);
                }
                else
                {
                    temp = Instantiate(weapon, transform.position + new Vector3(offset + 0.65f, 0, 0), transform.rotation);
                }
                tentacleAttackCounter = tentacleAttackDuration;
                temp.transform.parent = transform;
            }
        }

        if (tentacleAttackCounter > 0)
        {
            tentacleAttackCounter -= Time.deltaTime;
            if (tentacleAttackCounter > tentacleAttackDuration / 2)
            {
                if (GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
                {
                    temp.gameObject.transform.localScale += new Vector3(tentacleTranslation, 0, 0);
                    temp.gameObject.transform.position += new Vector3(-tentacleTranslation / 10, 0, 0);
                }
                else
                {
                    temp.gameObject.transform.localScale += new Vector3(tentacleTranslation, 0, 0);
                    temp.gameObject.transform.position += new Vector3(tentacleTranslation / 10, 0, 0);
                }
            }
            else if (tentacleAttackCounter < tentacleAttackDuration / 2)
            {
                if (GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
                {
                    temp.gameObject.transform.localScale -= new Vector3(tentacleTranslation, 0, 0);
                    temp.gameObject.transform.position -= new Vector3(-tentacleTranslation / 10, 0, 0);
                }
                else
                {
                    temp.gameObject.transform.localScale -= new Vector3(tentacleTranslation, 0, 0);
                    temp.gameObject.transform.position -= new Vector3(tentacleTranslation / 10, 0, 0);
                }
            }
            if (tentacleAttackCounter <= 0)
            {
                tentacleAttackCoolCounter = tentacleAttackTime;
                tentacleAttack = false;
                Destroy(temp, 0);
            }
        }

        if (tentacleAttackCoolCounter > 0f)
        {
            tentacleAttackCoolCounter -= Time.deltaTime;


            attack = false;
            charge = true;
        }
    }
}


