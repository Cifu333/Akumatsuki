using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAbilities : MonoBehaviour
{
    PlayerStatus ps;

    public GameObject semtex;

    public bool ability;

    public float offset = 1.1f;

    private Disparo bullet;

    public bool charge;

    PlayerAbilitys pa;

    Rigidbody2D rb;

    private bool semtexCooldown;
    private bool runeCooldown;

    private GameObject[] temp;

    private float semtexCoolCounter;
    private float semtexCounter;
    public float semtexDuration = 0.75f;
    public float semtexTime = 3f;
    public float semtexForceX;
    public float semtexForceY;

    private float runeCoolCounter;
    private float runeCounter;
    public float runeDuration = 3f;
    public float runeTime = 5f;

    public float runePower = 3;
    void Start()
    {
        temp = new GameObject[6];
        semtexCooldown = false;
        charge = true;
        ability = false;
        ps = GetComponent<PlayerStatus>();
        pa = GetComponent<PlayerAbilitys>();
    }

    // Update is called once per frame
    void Update()
    {
        semtexAtack();
        Rune();
    }

    private void semtexAtack()
    {
        if (Input.GetKeyDown(KeyCode.V) && ps.free == true && semtexCooldown == false)
        if (Input.GetKeyDown(KeyCode.V) && ps.free == true && pa.human[0] == true && semtexCooldown == false)
        {
            if (semtexCoolCounter <= 0)
            {
                semtexCooldown = true;
                if (GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
                {
                    temp[0] = Instantiate(semtex, transform.position + new Vector3(-offset, 0, 0), transform.rotation);
                    temp[0].GetComponent<Rigidbody2D>().AddForce(new Vector2(-semtexForceX, semtexForceY));
                }
                else
                {
                    temp[0] = Instantiate(semtex, transform.position + new Vector3(offset, 0, 0), transform.rotation);
                    temp[0].GetComponent<Rigidbody2D>().AddForce(new Vector2(semtexForceX, semtexForceY));
                }
                semtexCoolCounter = semtexTime;
            }
        }

        if (semtexCoolCounter > 0f)
        {
            semtexCoolCounter -= Time.deltaTime;
            if (semtexCoolCounter <= 0)
            {
                semtexCooldown = false;
            }
        }
    }

    private void Rune()
    {
        if (Input.GetKeyDown(KeyCode.C) && ps.free == true && pa.human[1] == true && runeCooldown == false)
        {
            if (runeCoolCounter <= 0)
            {
                runeCooldown = true;
                gameObject.GetComponent<PlayerStatus>().invulneravility = true;
                gameObject.GetComponent<PlayerStatus>().invulneravilityFrames = runePower;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.15f, 1f, 1f);
                runeCoolCounter = runeTime;
                runeCounter = runeDuration;
            }
        }

        if (runeCoolCounter > 0f)
        {
            runeCounter -= Time.deltaTime;
            if (runeCounter <= 0)
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        }

        if (runeCoolCounter > 0f)
        {
            runeCoolCounter -= Time.deltaTime;
            if (runeCoolCounter <= 0)
            {
                runeCooldown = false;
            }
        }
    }
}
