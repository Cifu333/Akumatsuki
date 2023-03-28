using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAbilities : MonoBehaviour
{
    PlayerStatus ps;

    public GameObject tentacle;
    public GameObject fireColumns;

    public bool ability;

    public float offset = 1.1f;

    private Disparo bullet;

    public bool charge;

    PlayerAbilitys pa;

    Rigidbody2D rb;

    private float tentacleAttackCoolCounter;
    private float tentacleAttackCounter;
    private float tentacleAttackChargeCounter;
    public float tentacleAttackDuration = 0.75f;
    public float tentacleAttackTime = 0.5f;
    public float tentacleAttackCharge = 0.2f;
    public float tentacleTranslation = 0.3f;

    private float fireAttackCoolCounter;
    private float fireAttackCounter;
    private float fireAttackChargeCounter;
    public float fireAttackDuration = 0.75f;
    public float fireAttackTime = 0.7f;
    public float fireAttackCharge = 0.2f;
    public float fireTranslation = 0.3f;
    private bool twoFire;
    private bool threeFire;

    public int tentacleMisery;
    public int fireMisery;

    public GameObject[] temp;

    private bool tentacleCooldown;
    private bool fireCooldown;
    // Start is called before the first frame update
    void Start()
    {
        tentacleMisery = 20;
        fireMisery = 30;
        temp = new GameObject[6];
        tentacleCooldown = false;
        fireCooldown = false;
        twoFire = true;
        threeFire = true;
        charge = true;
        ability = false;
        ps = GetComponent<PlayerStatus>();
        pa = GetComponent<PlayerAbilitys>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        TentacleAttack();
        FireColumns();
    }

    private void TentacleAttack()
    {
        if (Input.GetKeyDown(KeyCode.F) && ps.free == true && pa.demon[0] == true && tentacleCooldown == false && ps.misery >= tentacleMisery)
        {
            if (tentacleAttackCoolCounter <= 0 && tentacleAttackCounter <= 0)
            {
                ps.misery -= tentacleMisery;
                if (charge == true)
                {
                    ability = true;
                    tentacleAttackChargeCounter = tentacleAttackCharge;
                    charge = false;
                    tentacleCooldown = true;
                }
                if (GetComponent<GroundDetector>().grounded == false)
                {
                    rb.velocity = Vector3.zero;
                    rb.gravityScale = 0;
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
                    temp[0] = Instantiate(tentacle, transform.position + new Vector3(-(offset + 0.65f), 0, 0), transform.rotation);
                }
                else
                {
                    temp[0] = Instantiate(tentacle, transform.position + new Vector3(offset + 0.65f, 0, 0), transform.rotation);
                }
                tentacleAttackCounter = tentacleAttackDuration;
                temp[0].transform.parent = transform;
            }
        }

        if (tentacleAttackCounter > 0)
        {
            tentacleAttackCounter -= Time.deltaTime;
            if (tentacleAttackCounter > tentacleAttackDuration / 2)
            {
                if (GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
                {
                    temp[0].gameObject.transform.localScale += new Vector3(tentacleTranslation, 0, 0);
                    temp[0].gameObject.transform.position += new Vector3(-tentacleTranslation / 10, 0, 0);
                }
                else
                {
                    temp[0].gameObject.transform.localScale += new Vector3(tentacleTranslation, 0, 0);
                    temp[0].gameObject.transform.position += new Vector3(tentacleTranslation / 10, 0, 0);
                }
            }
            else if (tentacleAttackCounter < tentacleAttackDuration / 2)
            {
                if (GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
                {
                    temp[0].gameObject.transform.localScale -= new Vector3(tentacleTranslation, 0, 0);
                    temp[0].gameObject.transform.position -= new Vector3(-tentacleTranslation / 10, 0, 0);
                }
                else
                {
                    temp[0].gameObject.transform.localScale -= new Vector3(tentacleTranslation, 0, 0);
                    temp[0].gameObject.transform.position -= new Vector3(tentacleTranslation / 10, 0, 0);
                }
            }
            if (tentacleAttackCounter <= 0)
            {
                tentacleAttackCoolCounter = tentacleAttackTime;
                rb.gravityScale = 8;
                Destroy(temp[0], 0);
                ability = false;
                charge = true;
            }
        }

        if (tentacleAttackCoolCounter > 0f)
        {
            tentacleAttackCoolCounter -= Time.deltaTime;
            if (tentacleAttackCoolCounter <= 0)
            {
                tentacleCooldown = false;
            }
        }
    }

    private void FireColumns()
    {
        if (Input.GetKeyDown(KeyCode.G) && ps.free == true && pa.demon[1] == true && fireCooldown == false && ps.misery >= fireMisery)
        {
            if (fireAttackCoolCounter <= 0 && fireAttackCounter <= 0)
            {
                ps.misery -= fireMisery;
                if (charge == true)
                {
                    ability = true;
                    fireAttackChargeCounter = fireAttackCharge;
                    charge = false;
                    fireCooldown = true;
                }
                if (GetComponent<GroundDetector>().grounded == false)
                {
                    rb.velocity = Vector3.zero;
                    rb.gravityScale = 0;
                }

            }
        }
        if (fireAttackChargeCounter > 0)
        {
            fireAttackChargeCounter -= Time.deltaTime;
            if (fireAttackChargeCounter <= 0)
            {
                temp[0] = Instantiate(fireColumns, transform.position + new Vector3(-(offset + 0.65f), 0, 0), transform.rotation);
                temp[1] = Instantiate(fireColumns, transform.position + new Vector3(offset + 0.65f, 0, 0), transform.rotation);

                fireAttackCounter = fireAttackDuration;
                temp[0].transform.parent = transform;
                temp[1].transform.parent = transform;
            }
        }

        if (fireAttackCounter > 0)
        {
            fireAttackCounter -= Time.deltaTime;
            if (fireAttackCounter < fireAttackDuration / 3)
            {
                if (threeFire)
                {
                    temp[4] = Instantiate(fireColumns, transform.position + new Vector3(-(offset * 4), 0, 0), transform.rotation);
                    temp[5] = Instantiate(fireColumns, transform.position + new Vector3(offset * 4, 0, 0), transform.rotation);
                    temp[4].transform.parent = transform;
                    temp[5].transform.parent = transform;
                    threeFire = false;
                }

                temp[0].gameObject.transform.localScale += new Vector3(0, fireTranslation, 0);
                temp[1].gameObject.transform.localScale += new Vector3(0, fireTranslation, 0);
                temp[0].gameObject.transform.localScale += new Vector3(0, fireTranslation, 0);
                temp[1].gameObject.transform.localScale += new Vector3(0, fireTranslation, 0);
                temp[2].gameObject.transform.localScale += new Vector3(0, fireTranslation, 0);
                temp[3].gameObject.transform.localScale += new Vector3(0, fireTranslation, 0);
            }
            else if (fireAttackCounter < (fireAttackDuration / 3) * 2)
            {
                if (twoFire)
                {
                    temp[2] = Instantiate(fireColumns, transform.position + new Vector3(-(offset * 3), 0, 0), transform.rotation);
                    temp[3] = Instantiate(fireColumns, transform.position + new Vector3(offset * 3, 0, 0), transform.rotation);
                    temp[2].transform.parent = transform;
                    temp[3].transform.parent = transform;
                    twoFire = false;
                }

                temp[0].gameObject.transform.localScale += new Vector3(0, fireTranslation, 0);
                temp[1].gameObject.transform.localScale += new Vector3(0, fireTranslation, 0);
                temp[2].gameObject.transform.localScale += new Vector3(0, fireTranslation, 0);
                temp[3].gameObject.transform.localScale += new Vector3(0, fireTranslation, 0);
            }
            else if (fireAttackCounter < fireAttackDuration)
            {
                temp[0].gameObject.transform.localScale += new Vector3(0, fireTranslation, 0);
                temp[1].gameObject.transform.localScale += new Vector3(0, fireTranslation, 0);
            }
            if (fireAttackCounter <= 0)
            {
                fireAttackCoolCounter = fireAttackTime;
                rb.gravityScale = 8;
                Destroy(temp[0], 0);
                Destroy(temp[1], 0);
                Destroy(temp[2], 0);
                Destroy(temp[3], 0);
                Destroy(temp[4], 0);
                Destroy(temp[5], 0);
                ability = false;
                twoFire = true;
                threeFire = true;
                charge = true;
            }
        }

        if (fireAttackCoolCounter > 0f)
        {
            fireAttackCoolCounter -= Time.deltaTime;

            if (fireAttackCoolCounter <= 0)
            {
                fireCooldown = false;
            }
        }
    }
}
