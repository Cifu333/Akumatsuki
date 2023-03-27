using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAbilities : MonoBehaviour
{
    PlayerStatus ps;
    PlayerAttack pAttack;

    public GameObject tentacle;

    public bool ability;

    public float offset = 1.1f;

    private Disparo bullet;

    public bool charge;

    public bool tentacleAttack;

    PlayerAbilitys pa;

    Rigidbody2D rb;

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
        ability = false;
        tentacleAttack = false;
        ps = GetComponent<PlayerStatus>();
        pa = GetComponent<PlayerAbilitys>();
        rb = GetComponent<Rigidbody2D>();
        pAttack = GetComponent<PlayerAttack>();

    }

    // Update is called once per frame
    void Update()
    {
        TentacleAttack();

    }

    private void TentacleAttack()
    {
        if (Input.GetKeyDown(KeyCode.F) && ps.free == true && pa.demon[0] == true)
        {
            if (tentacleAttackCoolCounter <= 0 && tentacleAttackCounter <= 0)
            {
                if (charge == true)
                {
                    tentacleAttack = true;
                    ability = true;
                    tentacleAttackChargeCounter = tentacleAttackCharge;
                    charge = false;
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
                    temp = Instantiate(tentacle, transform.position + new Vector3(-(offset + 0.65f), 0, 0), transform.rotation);
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
                rb.gravityScale = 8;
                Destroy(temp, 0);
            }
        }

        if (tentacleAttackCoolCounter > 0f)
        {
            tentacleAttackCoolCounter -= Time.deltaTime;

            ability = false;
            charge = true;
        }
    }
}
