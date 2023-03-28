using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorHp : MonoBehaviour
{
    public GameObject player;
    public float hp;
    public float time = 0;
    private PlayerStatus playerM;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        playerM = player.GetComponent<PlayerStatus>();
        hp = playerM.hp;
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerM.hp > hp)
        {

            if (playerM.hp - hp > 100)
                time += 1000 * Time.deltaTime;

            else if (playerM.hp - hp > 10)
                time += 100 * Time.deltaTime;

            else
                time += 10 * Time.deltaTime;


            if (time >= 1)
            {
                hp++;
                time = 0;
            }
        }
        else if (playerM.hp < hp)
        {

            if (hp - playerM.hp > 100)
                time += 1000 * Time.deltaTime;

            else if (hp - playerM.hp > 10)
                time += 100 * Time.deltaTime;

            else
                time += 10 * Time.deltaTime;

            if (time >= 1)
            {
                hp--;
                time = 0;
            }
        }
        text.text = hp.ToString();

    }
}
