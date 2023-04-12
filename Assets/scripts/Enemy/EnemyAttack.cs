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


    private float attackCoolCounter;
    private float attackChargeCounter;
    public float attackTime = 0.4f;
    public float attackCharge = 0.2f;

    public bool stun;
    public float stunCounter;

    // Start is called before the first frame update
    void Start()
    {
        stun = false;
        charge = true;
        em = GetComponent<EnemyMovement>();
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
                        break;
                    case EnemyStatus.Type.FLYING:
                        break;
                }
            }
        }
        else
        {
            stunCounter -= Time.deltaTime;
            if (stunCounter <= 0)
            {
                stun = false;
            }
        }
    }

    private void MeleeAttack()
    {
        if (em.dif <= em.distance && (transform.position.y + attackHeight) >= em.target.transform.position.y && (transform.position.y - attackHeight) <= em.target.transform.position.y)
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
            }
        }

        Destroy(temp, Time.fixedDeltaTime);

        if (attackCoolCounter > 0f)
        {
            attackCoolCounter -= Time.deltaTime;
            if (attackCoolCounter <= 0f)
            {
                charge = true;
                attack = false;
            }
        }
    }

    private void RangedAttack()
    {
        if (em.dif >= em.distance && em.dif <= em.detect && (transform.position.y + attackHeight) >= em.target.transform.position.y && (transform.position.y - attackHeight) <= em.target.transform.position.y)
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
                Destroy(temp, 6);
            }
        }

        if (attackCoolCounter > 0f)
        {
            attackCoolCounter -= Time.deltaTime;
            if (attackCoolCounter <= 0f)
            {
                charge = true;
                attack = false;
            }
        }
    }
}


