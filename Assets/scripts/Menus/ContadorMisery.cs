using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorMisery : MonoBehaviour
{
    public int misery;
    public float time = 0;
    PlayerStatus ps;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.misery > misery)
        {

            if (ps.misery - misery > 100)
                time += 1000 * Time.deltaTime;

            else if (ps.misery - misery > 10)
                time += 100 * Time.deltaTime;

            else
                time += 10 * Time.deltaTime;


            if (time >= 1)
            {
                misery++;
                time = 0;
            }
        }
        else if (ps.misery < misery)
        {

            if (misery - ps.misery > 100)
                time += 1000 * Time.deltaTime;

            else if (misery - ps.misery > 10)
                time += 100 * Time.deltaTime;

            else
                time += 10 * Time.deltaTime;

            if (time >= 1)
            {
                misery--;
                time = 0;
            }
        }
        text.text = misery.ToString() + "/" + ps.maxMisery.ToString();

    }
}
