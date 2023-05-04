using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Contador : MonoBehaviour
{
    GameObject player;
    public int money;
    public float time = 0;
    private PlayerStatus playerM;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerM = player.GetComponent<PlayerStatus>();
        money = playerM.money;
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerM.money > money)
        {
            
            if (playerM.money - money > 100)
                time += 1000 * Time.deltaTime;

            else if (playerM.money - money > 10)
                time += 100 * Time.deltaTime;

            else
                time += 10 * Time.deltaTime;


            if (time >= 1)
            {
                money++;
                time = 0;
            }
        }
        else if (playerM.money < money)
        {

            if (money - playerM.money > 100)
                time += 1000 * Time.deltaTime;

            else if (money - playerM.money > 10)
                time += 100 * Time.deltaTime;

            else
                time += 10 * Time.deltaTime;

            if (time >= 1)
            {
                money--;
                time = 0;
            }
        }
        text.text = money.ToString();

    }
}
