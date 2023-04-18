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
    private float groundDistance = 1f;
    public LayerMask groundMask;
    public bool attach;

    private float fireCoolCounter;
    private float fireCounter;
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
                    temp = Instantiate(tentacle, transform.position + new Vector3(-(offset + 0.65f), 0, 0), new Quaternion());
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
            if (tentacleAttackCounter > tentacleAttackDuration / 2)
            {
                if (GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
                {
                    temp.gameObject.transform.localScale += new Vector3(tentacleTranslation, 0, 0);
                    temp.gameObject.transform.position += new Vector3(-tentacleTranslation / 10, 0, 0);
                    Debug.DrawRay(temp.transform.position + new Vector3(tentacleTranslation / 10, 0, 0), transform.right * groundDistance * -1, Color.red);
                    if (Physics2D.Raycast(temp.transform.position - new Vector3(tentacleTranslation / 10,0,0), transform.right * -1, groundDistance, groundMask))
                        attach = true;
                }
                else
                {
                    temp.gameObject.transform.localScale += new Vector3(tentacleTranslation, 0, 0);
                    temp.gameObject.transform.position += new Vector3(tentacleTranslation / 10, 0, 0);
                    Debug.DrawRay(temp.transform.position + new Vector3(temp.transform.position.x * 1.5f, 0, 0), transform.right * groundDistance, Color.red);
                    if (Physics2D.Raycast(temp.transform.position + new Vector3(tentacleTranslation / 10, 0, 0), transform.right, groundDistance, groundMask))
                        attach = true;
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
                rb.gravityScale = 8;
                Destroy(temp, 0);
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
            if (fireCounter <= 0)
            {
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
}
