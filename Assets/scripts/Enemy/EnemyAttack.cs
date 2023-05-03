using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    EnemyMovement em;
    public float offset = 1.1f;
    public float attackHeight = 2.25f;

    public GameObject enemyWeapon;
    public bool attack;
    private bool charge;
    GameObject temp;
    Rigidbody2D rb;


    private float attackCoolCounter;
    private float attackChargeCounter;
    public float attackTime = 0.4f;
    public float attackCharge = 0.2f;

    //Only flying
    public float dashForce = 15;

    public float dashDuration = 0.5f;
    private float dashCounter;
    //

    Animator anim;

    public bool stun;
    public float stunCounter;

    public float vector;
    public float sin;
    public float cos;

    public float dashMovement;

    private bool dashR;
    private bool dashH;
    public bool dashB;
    public float dashTime;

    // Start is called before the first frame update
    void Start()
    {
        dashR = false;
        dashH = true;
        dashB = false;
        stun = true;
        stunCounter = 1;
        charge = true;
        attack = false;
        em = GetComponent<EnemyMovement>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (em.target != null)
        {
            if (!stun)
            {
                switch (em.es.type)
                {
                    case EnemyStatus.Type.MELEE:
                        MeleeAttack();
                        break;
                    case EnemyStatus.Type.RANGED:
                        RangedAttack();
                        break;
                    case EnemyStatus.Type.TANK:
                        MeleeAttack();
                        break;
                    case EnemyStatus.Type.FLYING:
                        FlyingAttack();
                        break;
                }
            }
            else
            {
                attack = false;
                attackCoolCounter = 0;
                charge = true;
                stunCounter -= Time.deltaTime;
                if (stunCounter <= 0)
                {
                    stun = false;
                }
            }
        }
        anim.SetBool("Attack", attack);
    }

    private void MeleeAttack()
    {
        if (em.difX <= em.distance && (transform.position.y + attackHeight) >= em.target.transform.position.y && (transform.position.y - attackHeight) <= em.target.transform.position.y)
        {
            if (attackCoolCounter <= 0)
            {
                if (charge == true)
                {
                    attack = true;
                    attackChargeCounter = attackCharge;
                    charge = false;
                }
            }
        }

        if (attackChargeCounter > 0)
        {
            attackChargeCounter -= Time.deltaTime;
            if (attackChargeCounter <= 0)
            {
                if (GetComponent<EnemyMovement>().dir == EnemyMovement.Direction.LEFT)
                {
                    temp = Instantiate(enemyWeapon, transform.position + new Vector3(-offset, 0, 0), transform.rotation);
                    temp.transform.localScale = new Vector3(-temp.transform.localScale.x, temp.transform.localScale.y);
                }
                else
                {
                    temp = Instantiate(enemyWeapon, transform.position + new Vector3(offset, 0, 0), transform.rotation);
                }
                attackCoolCounter = attackTime;
                temp.transform.parent = transform;
                if (em.es.type == EnemyStatus.Type.TANK)
                {
                    GameObject.FindGameObjectWithTag("MainCamera").transform.parent.GetComponent<camShake>().pressToShake1 = true;
                }
            }
        }

        Destroy(temp, Time.fixedDeltaTime);

        if (attackCoolCounter > 0f)
        {
            attackCoolCounter -= Time.deltaTime;
            if (attackCoolCounter < attackTime / 2)
                attack = false;
            if (attackCoolCounter <= 0f)
            {
                charge = true;
            }
        }
    }
    private void FlyingAttack()
    {
        if (em.difX <= em.distance && em.difY <= em.distance)
        {
            if (attackCoolCounter <= 0)
            {
                if (charge == true)
                {
                    attack = true; 
                    vector = Mathf.Sqrt(((em.target.transform.position.y - transform.position.y) * (em.target.transform.position.y - transform.position.y)) + ((em.target.transform.position.x - transform.position.x) * (em.target.transform.position.x - transform.position.x)));
                    sin = (em.target.transform.position.y - transform.position.y) / vector;
                    cos = (em.target.transform.position.x - transform.position.x) / vector;
                    attackChargeCounter = attackCharge;
                    charge = false;
                }
            }
        }

        if (attackChargeCounter > 0)
        {
            attackChargeCounter -= Time.deltaTime;
            if (attackChargeCounter <= 0)
            {

                temp = Instantiate(enemyWeapon, transform.position + new Vector3(0, 0, 0), transform.rotation);
                rb.AddForce(new Vector2(dashForce * cos, dashForce * sin));
                dashCounter = dashDuration;

                temp.transform.parent = transform;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                attackCoolCounter = attackTime;
                Destroy(temp);
                rb.velocity = Vector2.zero;
                attack = false;
            }
        }

        if (attackCoolCounter > 0f)
        {
            attackCoolCounter -= Time.deltaTime;
            if (attackCoolCounter <= 0f)
            {
                charge = true;
            }
        }
    }

    private void RangedAttack()
    {
        if (em.difX >= em.distance && em.difX <= em.detect && (transform.position.y + attackHeight) >= em.target.transform.position.y && (transform.position.y - attackHeight) <= em.target.transform.position.y)
        {
            if (attackCoolCounter <= 0)
            {
                if (charge == true)
                {
                    attack = true;
                    attackChargeCounter = attackCharge;
                    charge = false;
                }
            }
        }

        if (attackChargeCounter > 0)
        {
            attackChargeCounter -= Time.deltaTime;
            if (attackChargeCounter <= 0)
            {
                if (GetComponent<EnemyMovement>().dir == EnemyMovement.Direction.LEFT)
                {
                    temp = Instantiate(enemyWeapon, transform.position + new Vector3(-offset, 0, 0), transform.rotation);
                }
                else
                {
                    temp = Instantiate(enemyWeapon, transform.position + new Vector3(offset, 0, 0), transform.rotation);
                }
                attackCoolCounter = attackTime;
                temp.transform.parent = transform;
                attack = false;
                Destroy(temp, 4);
            }
        }

        if (attackCoolCounter > 0f)
        {
            attackCoolCounter -= Time.deltaTime;
            if (attackCoolCounter <= 0f)
            {
                charge = true;
            }
        }

        if (em.difX < em.distance / 2 && dashR == false && (transform.position.y + attackHeight) >= em.target.transform.position.y && (transform.position.y - attackHeight) <= em.target.transform.position.y) 
        {
            dashR = true;
            dashB = true;
            attack = true;
            if (GetComponent<EnemyMovement>().dir == EnemyMovement.Direction.LEFT)
            {
                transform.position = new Vector3(transform.position.x - dashMovement, transform.position.y);
                temp = Instantiate(enemyWeapon, transform.position - new Vector3(dashMovement + offset, 0, 0), transform.rotation);
                temp.transform.parent = transform;
                Destroy(temp, 4);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + dashMovement, transform.position.y);
                temp = Instantiate(enemyWeapon, transform.position + new Vector3(dashMovement + offset, 0, 0), transform.rotation);
                temp.transform.parent = transform;
                Destroy(temp, 4);
            }
            dashCounter = dashTime;
            anim.SetBool("Dash", true);
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= dashTime / 10 * 9)
            {
                anim.SetBool("Dash", false);
                if (dashH)
                {
                    dashB = false;
                    dashH = false;
                    if (GetComponent<EnemyMovement>().dir == EnemyMovement.Direction.LEFT)
                    {
                        transform.position = new Vector3(transform.position.x - dashMovement, transform.position.y);
                    }
                    else
                    {
                        transform.position = new Vector3(transform.position.x + dashMovement, transform.position.y);
                    }
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
                }
                attack = false;
            }
            if (dashCounter <= 0)
            {
                dashR = false;
                dashH = true;
            }
        }

    }
}


