using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAbilities : MonoBehaviour
{
    PlayerStatus ps;

    public GameObject tentacle;
    public GameObject fire;

    public bool ability;

    public float offset = 1.1f;

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
    public float groundDistance = 1f;
    public LayerMask groundMask;
    public bool attach;

    private float fireCoolCounter;
    private float fireCounter;
    private float fireReiterateCounter;
    public float fireDuration = 3f;
    public float fireTime = 3f;

    public int tentacleMisery;
    public int fireMisery;

    public GameObject temp;

    private bool tentacleCooldown;
    private bool fireCooldown;
    // Start is called before the first frame update
    void Start()
    {
        tentacleMisery = 20;
        fireMisery = 30;
        fireReiterateCounter = 0;
        tentacleCooldown = false;
        fireCooldown = false;
        attach = false;
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
        Fire();
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
                    temp = Instantiate(tentacle, transform.position + new Vector3(-(offset + 0.45f), 0, 0), transform.rotation);
                    temp.transform.localScale = new Vector2(-temp.transform.localScale.x,temp.transform.localScale.y);
                }
                else
                {
                    temp = Instantiate(tentacle, transform.position + new Vector3(offset + 0.65f, 0, 0), transform.rotation);
                }
                tentacleAttackCounter = tentacleAttackDuration;
                temp.transform.parent = transform;
            }
        }

        if (tentacleAttackCounter > 0)
        {
            tentacleAttackCounter -= Time.deltaTime;
            if (tentacleAttackCounter > tentacleAttackDuration / 2 && attach == false)
            {
                if (GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
                {
                    temp.gameObject.transform.localScale += new Vector3(tentacleTranslation, 0, 0) * Time.deltaTime;
                }
                else
                {
                    temp.gameObject.transform.localScale += new Vector3(tentacleTranslation, 0, 0) * Time.deltaTime;
                }
            }
            else if (tentacleAttackCounter < tentacleAttackDuration / 2)
            {
                if (attach == false)
                {
                    if (GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
                    {
                        temp.gameObject.transform.localScale -= new Vector3(tentacleTranslation, 0, 0) * Time.deltaTime;
                    }
                    else
                    {
                        temp.gameObject.transform.localScale -= new Vector3(tentacleTranslation, 0, 0) * Time.deltaTime;
                    }
                }
                else
                {

                    if (GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
                    {
                        if (temp.gameObject.transform.localScale.x > 0)
                        {
                            temp.gameObject.transform.localScale -= new Vector3(tentacleTranslation * 1.5f, 0, 0) * Time.deltaTime;
                            transform.position -= new Vector3(tentacleTranslation / 2.5f, 0, 0) * Time.deltaTime;
                        }
                    }
                    else
                    {
                        if (temp.gameObject.transform.localScale.x > 0)
                        {
                            temp.gameObject.transform.localScale -= new Vector3(tentacleTranslation * 1.5f, 0, 0) * Time.deltaTime;
                            transform.position -= new Vector3(-tentacleTranslation / 2.5f, 0, 0) * Time.deltaTime;
                        }
                    }
                }
            }
            if (tentacleAttackCounter <= 0)
            {
                tentacleAttackCoolCounter = tentacleAttackTime;
                rb.gravityScale = 8;
                Destroy(temp, 0);
                ability = false;
                charge = true;
                attach = false;
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

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.G) && ps.free == true && pa.demon[1] == true && fireCooldown == false && ps.misery >= fireMisery)
        {
            if (fireCoolCounter <= 0)
            {
                fireCooldown = true;
                temp = Instantiate(fire, transform.position, transform.rotation);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
                temp.transform.parent = transform;

                fireCoolCounter = fireTime;
                fireCounter = fireDuration;
                ps.misery -= fireMisery;
            }
        }

        if (fireCounter > 0f)
        {
            fireCounter -= Time.deltaTime;
            fireReiterateCounter += Time.deltaTime;
            if (fireReiterateCounter >= 1)
            {
                fireReiterateCounter = 0;
                Destroy(temp, 0);
                temp = Instantiate(fire, transform.position, transform.rotation);
            }
            if (fireCounter <= 0)
            {
                fireReiterateCounter = 0;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
                Destroy(temp, 0);
            }
        }

        if (fireCoolCounter > 0f)
        {
            fireCoolCounter -= Time.deltaTime;
            if (fireCoolCounter <= 0)
            {
                fireCooldown = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<Collider2D>() != null)
        {
            if (collision.gameObject.layer == 3)
                attach = true;
        }
    }
}
