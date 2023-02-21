using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    EnemyMovement em;
    public float offset = 1.1f;

    public GameObject enemyWeapon;
    public bool attack;
    private bool charge;


    private float attackCoolCounter;
    private float attackCounter;
    private float attackChargeCounter;
    public float attackDuration = 0.4f;
    public float attackTime = 0.4f;
    public float attackCharge = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        charge = true;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject temp;
        em = GetComponent<EnemyMovement>();
        if (em.dif <= em.distance)
        {
            if (attackCoolCounter <= 0 && attackCounter <= 0)
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
                attackCounter = attackDuration;
                temp.transform.parent = transform;
                Destroy(temp, attackDuration);
            }
        }

        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
            if (attackCounter <= 0)
            {
                attackCoolCounter = attackTime;
                attack = false;
            }
        }

        if (attackCoolCounter > 0f)
        {
            attackCoolCounter -= Time.deltaTime;
            charge = true;
        }
    }
}
