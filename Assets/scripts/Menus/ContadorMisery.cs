using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorMisery : MonoBehaviour
{
    public GameObject player;
    public int misery;
    public float time = 0;
    private PlayerStatus playerM;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        playerM = player.GetComponent<PlayerStatus>();
        misery = playerM.misery;
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerM.misery > misery)
        {

            if (playerM.misery - misery > 100)
                time += 1000 * Time.deltaTime;

            else if (playerM.misery - misery > 10)
                time += 100 * Time.deltaTime;

            else
                time += 10 * Time.deltaTime;


            if (time >= 1)
            {
                misery++;
                time = 0;
            }
        }
        else if (playerM.misery < misery)
        {

            if (misery - playerM.misery > 100)
                time += 1000 * Time.deltaTime;

            else if (misery - playerM.misery > 10)
                time += 100 * Time.deltaTime;

            else
                time += 10 * Time.deltaTime;

            if (time >= 1)
            {
                misery--;
                time = 0;
            }
        }
        text.text = misery.ToString();

    }
}
