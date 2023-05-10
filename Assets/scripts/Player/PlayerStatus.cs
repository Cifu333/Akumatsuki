using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float hp = 100;
    public float maxHP = 100;
    private float lastHP;
    public Vector3 respawn;
    public int money = 0;
    public int misery = 0;
    public int maxMisery = 1000;
    public int babyParts;
    public bool free;

    public bool initiate;

    public bool stun;
    public float stunCounter;

    Token d;
    PlayerAttack pa;
    DemonAbilities demonA;
    HorizontalMovement hm;
    HumanAbilities humanA;

    public Color colorO;

    public bool invulneravility;
    public float invulneravilityCounter;

    private float time;

    private float timeDeath;
    public float timeHit;

    public bool inThorns;
    public int inThornsCount;

    public GameObject babyPartsAudio;

    public bool win;

    // Start is called before the first frame update
    void Start()
    {
        win = false;
        free = true;
        inThorns = false;
        respawn = transform.position;
        invulneravility = false;
        stun = false;
        time = 1;
        timeHit = 0;
        demonA = GetComponent<DemonAbilities>();
        d = transform.GetChild(0).GetChild(0).GetComponent<Token>();
        pa = GetComponent<PlayerAttack>();
        hm = GetComponent<HorizontalMovement>();
        humanA = GetComponent<HumanAbilities>();
        colorO = GetComponent<SpriteRenderer>().color;
        lastHP = hp;
    }

    // Update is called once per frame
    void Update()
    {
        Muerte();
        FreeViability();
        InvulneravilityCount();
        if (stun)
        {
            stunCounter -= Time.deltaTime;
            if (stunCounter <= 0)
            {
                stun = false;
            }
        }
        if (inThorns && humanA.runeCounter <= 0)
        {
            time += Time.deltaTime;
            if (time >= 1)
            {
                hp -= 30;
                time = 0;
            }
        }
        if (misery > maxMisery)
            misery = maxMisery;
        if (hp < 0)
        {
            hp = 0;
        }

        if (hp < lastHP && hp > 0)
        {
            timeHit += Time.deltaTime;
            Debug.Log("Guayaba");
            if (timeHit < 0.25f)
                GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 2) * Time.deltaTime;
            else if (timeHit < 0.5f)
                GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 2) * Time.deltaTime;
            else if (timeHit < 0.75f)
                GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 2) * Time.deltaTime;
            else if (timeHit < 1.0f)
                GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 2) * Time.deltaTime;
            else
            {
                timeHit = 0;
                lastHP = hp;
                GetComponent<SpriteRenderer>().color = colorO;
            }
        }
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
        if (pa.attack == false && demonA.ability == false && stun == false && humanA.ability == false && hp > 0)
            free = true;
        else
            free = false;
    }

    private void Muerte()
    {
        if (hp <= 0)
        {
            GetComponent<Animator>().SetBool("Death", true);
            timeDeath += Time.deltaTime;
            if (timeDeath >= 2)
            {
                timeDeath = 0;
                hp = maxHP;
                lastHP = maxHP;
                misery = 0;
                transform.position = new Vector3(180, 27);
                GetComponent<Animator>().SetBool("Death", false);
                GameObject.FindGameObjectWithTag("SpawnerHandler").GetComponent<SpawnerHandler>().spawnAll = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BabyParts")
        {
            babyParts++;
            Destroy(collision.gameObject);
            Instantiate(babyPartsAudio);
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
                stun = true;
                stunCounter = 1;
                hp -= 10;
                if (hm.dir == HorizontalMovement.Direction.LEFT)
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(300, 300));
                else
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-300, 300));
            }
        }

        if (collision.gameObject.tag == "Win")
        {
            win = true;
        }
    }
}
