using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Contador : MonoBehaviour
{
    GameObject player;
    public int babyParts;
    public float time = 0;
    private PlayerStatus playerM;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerM = player.GetComponent<PlayerStatus>();
        babyParts = playerM.babyParts;
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerM.babyParts > babyParts)
        {
            
            if (playerM.babyParts - babyParts > 100)
                time += 1000 * Time.deltaTime;

            else if (playerM.babyParts - babyParts > 10)
                time += 100 * Time.deltaTime;

            else
                time += 10 * Time.deltaTime;


            if (time >= 1)
            {
                babyParts++;
                time = 0;
            }
        }
        text.text = babyParts.ToString() + "/3" + " memories";

    }
}
