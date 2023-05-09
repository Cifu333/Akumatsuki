using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject weapon;
    private GameObject temp;
    public Animator anim;

    public float offset = 1.1f;

    PlayerStatus ps;

    Rigidbody2D rb;

    private float time;
    public bool attack;

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
        attack = false;
        ps = GetComponent<PlayerStatus>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HeavyAttack();
        anim.SetBool("Attack", attack);
    }

    private void HeavyAttack()
    {
        if (Input.GetKeyDown(KeyCode.E) && ps.free == true)
        {
            if (heavyAttackCoolCounter <= 0)
            {
                if (charge == true)
                {
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
                heavyAttackCoolCounter = heavyAttackTime;
            }
        }

        if (heavyAttackCoolCounter > 0f)
        {
            heavyAttackCoolCounter -= Time.deltaTime;
            if (heavyAttackCoolCounter <= 0)
            {
                charge = true;
                attack = false;
            }
        }

    }

    private void Instantiate()
    {
        if (GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
        {
            temp = Instantiate(weapon, transform.position + new Vector3(-(offset), 0, 0), transform.rotation);
        }
        else
        {
            temp = Instantiate(weapon, transform.position + new Vector3(offset, 0, 0), transform.rotation);
        }
        temp.transform.parent = transform;
    }

    private void Destroy()
    {

        Destroy(temp);

    }
}


