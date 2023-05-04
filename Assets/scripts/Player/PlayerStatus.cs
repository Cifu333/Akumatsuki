using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float hp = 100;
    public float maxHP = 100;
    public Vector3 respawn;
    public int money = 0;
    public int misery = 0;
    public int babyParts;
    public bool free;

    public bool initiate;

    Token d;
    PlayerAttack pa;
    DemonAbilities demonA;
    HorizontalMovement hm;
    HumanAbilities humanA;

    public Color colorO;

    public bool invulneravility;
    public float invulneravilityCounter;
    // Start is called before the first frame update
    void Start()
    {
        free = true;
        respawn = transform.position;
        invulneravility = false;
        demonA = GetComponent<DemonAbilities>();
        d = transform.GetChild(0).GetChild(0).GetComponent<Token>();
        pa = GetComponent<PlayerAttack>();
        hm = GetComponent<HorizontalMovement>();
        humanA = GetComponent<HumanAbilities>();
        colorO = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        Muerte();
        FreeViability();
        InvulneravilityCount();
    }

    private void FixedUpdate()
    {
        
    }

    private void InvulneravilityCount()
    {
        if (invulneravilityCounter > 0)
        {
            invulneravilityCounter -= Time.deltaTime;
            if (invulneravilityCounter <= 0)
            {
                invulneravility = false;
            }
        }
    }

    private void FreeViability()
    {
        if (pa.attack == false && demonA.ability == false && hm.stun == false && humanA.ability == false)
            free = true;
        else
            free = false;
    }

    private void Muerte()
    {
        if (hp <= 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BabyParts")
        {
            babyParts++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == 6)
        {
            hp -= 10;
            transform.position = GetComponent<GroundDetector>().positionS;
        }

        if (collision.gameObject.tag == "ElectricDoor")
        {
            if (!collision.GetComponent<ElectricDoor>().off)
            {
                hm.stun = true;
                hm.stunCounter = 1;
                hp -= 10;
                if (hm.dir == HorizontalMovement.Direction.LEFT)
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(300, 300));
                else
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-300, 300));
            }
        }
    }
}
