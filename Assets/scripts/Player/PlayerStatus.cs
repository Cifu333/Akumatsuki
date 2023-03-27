using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float hp = 100;
    public float maxHP = 100;
    public Vector3 respawn;
    public int money = 0;
    public bool free;

    Disparo d;
    PlayerAttack pa;
    DemonAbilities demonA;
    HorizontalMovement hm;

    public float invulneravilityFrames = 1;
    public bool invulneravility;
    public float invulneravilityCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        free = true;
        respawn = transform.position;
        invulneravility = false;
        demonA = GetComponent<DemonAbilities>();
        d = GetComponent<Disparo>();
        pa = GetComponent<PlayerAttack>();
        hm = GetComponent<HorizontalMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Muerte();
        FreeViability();
    }

    private void FixedUpdate()
    {
        if (invulneravility == true)
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
        if (d.bullet == false && pa.attack == false && demonA.ability == false && hm.stun == false)
            free = true;
        else
            free = false;
    }

    private void Muerte()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
