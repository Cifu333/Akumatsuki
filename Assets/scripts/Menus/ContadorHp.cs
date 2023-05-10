using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorHp : MonoBehaviour
{
    public float hp;
    public float time = 0;
    private PlayerStatus ps;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        text = GetComponent<TextMeshProUGUI>();
        hp = ps.hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.hp > hp)
        {

            if (ps.hp - hp > 100)
                time += 1000 * Time.deltaTime;

            else if (ps.hp - hp > 10)
                time += 100 * Time.deltaTime;

            else
                time += 10 * Time.deltaTime;


            if (time >= 1)
            {
                hp++;
                time = 0;
            }
        }
        else if (ps.hp < hp)
        {

            if (hp - ps.hp > 100)
                time += 1000 * Time.deltaTime;

            else if (hp - ps.hp > 10)
                time += 100 * Time.deltaTime;

            else
                time += 10 * Time.deltaTime;

            if (time >= 1)
            {
                hp--;
                time = 0;
            }
        }
        text.text = hp.ToString() + "/" + ps.maxHP.ToString();

    }
}
