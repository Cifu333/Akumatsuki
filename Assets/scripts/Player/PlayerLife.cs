using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public float hp = 100;
    public float maxHP = 100;
    public Vector3 respawn;
    public int money = 0;

    public float invulneravilityFrames = 1;
    public bool invulneravility;
    public float invulneravilityCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        respawn = transform.position;
        invulneravility = false;
    }

    // Update is called once per frame
    void Update()
    {
        Muerte();
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

    private void Muerte()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
