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
    public bool free;

    Disparo d;
    PlayerAttack pa;
    DemonAbilities demonA;
    HorizontalMovement hm;
    HumanAbilities humanA;

    public bool invulneravility;
    public float invulneravilityCounter;
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
        humanA = GetComponent<HumanAbilities>();
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
        if (d.justShot == false && pa.attack == false && demonA.ability == false && hm.stun == false && humanA.ability == false)
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
