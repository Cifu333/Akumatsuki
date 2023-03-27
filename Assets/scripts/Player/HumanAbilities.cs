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

    private GameObject[] temp;

    private float semtexCoolCounter;
    private float semtexCounter;
    public float semtexDuration = 0.75f;
    public float semtexTime = 3f;
    public float semtexForceX;
    public float semtexForceY;
    // Start is called before the first frame update
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
    }

    private void semtexAtack()
    {
        if (Input.GetKeyDown(KeyCode.V) && ps.free == true && semtexCooldown == false)
        {
            if (semtexCoolCounter <= 0)
            {
                semtexCooldown = true;
                if (GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
                {
                    temp[0] = Instantiate(semtex, transform.position + new Vector3(-offset, 0, 0), transform.rotation);
                }
                else
                {
                    temp[0] = Instantiate(semtex, transform.position + new Vector3(offset, 0, 0), transform.rotation);
                }
                semtexCoolCounter = semtexTime;
                temp[0].GetComponent<Rigidbody2D>().AddForce(new Vector2(semtexForceX,semtexForceY));
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
}
