using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoolDownUI : MonoBehaviour
{
    public GameObject TentacleCool;
    public TextMeshProUGUI TentacleCoolText;
    public GameObject FireCool;
    public TextMeshProUGUI FireCoolText;
    public GameObject EMPCool;
    public TextMeshProUGUI EMPCoolText;
    public GameObject RuneCool;
    public TextMeshProUGUI RuneCoolText;
    GameObject player;
    DemonAbilities da;
    HumanAbilities ha;
    PlayerAbilitys pa;
    private Color coolColor;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        da = player.GetComponent<DemonAbilities>();
        ha = player.GetComponent<HumanAbilities>();
        pa = player.GetComponent<PlayerAbilitys>();
        TentacleCool.SetActive(false);
        FireCool.SetActive(false);
        EMPCool.SetActive(false);
        RuneCool.SetActive(false);
        coolColor = new Color(1, 1, 1, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (pa.demon[0] == true)
        {
            TentacleCool.SetActive(true);
        }
        if (pa.demon[1] == true)
        {
            FireCool.SetActive(true);
        }
        if (pa.human[0] == true)
        {
            EMPCool.SetActive(true);
        }
        if (pa.human[1] == true)
        {
            RuneCool.SetActive(true);
        }

        if (TentacleCool.activeSelf == true)
        {
            if (da.tentacleAttackCoolCounter > 0)
            {
                if (da.tentacleAttackCoolCounter == da.tentacleAttackTime)
                    TentacleCool.transform.GetChild(0).GetComponent<Image>().color = coolColor;
                int a = (int)da.tentacleAttackCoolCounter + 1;
                TentacleCoolText.text = a.ToString();
            }
            else
            {
                TentacleCool.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                TentacleCoolText.text = "";
            }

        }

        if (RuneCool.activeSelf == true)
        {
            if (ha.runeCoolCounter > 0)
            {
                if (ha.runeCoolCounter == ha.runeTime)
                    RuneCool.transform.GetChild(0).GetComponent<Image>().color = coolColor;
                int a = (int)ha.runeCoolCounter + 1;
                RuneCoolText.text = a.ToString();
            }
            else
            {
                RuneCool.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                RuneCoolText.text = "";
            }

        }

        if (EMPCool.activeSelf == true)
        {
            if (ha.semtexCoolCounter > 0)
            {
                if (ha.semtexCoolCounter == ha.semtexTime)
                    EMPCool.transform.GetChild(0).GetComponent<Image>().color  = coolColor;
                int a = (int)ha.semtexCoolCounter + 1;
                EMPCoolText.text = a.ToString();
            }
            else
            {
                EMPCool.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                EMPCoolText.text = "";
            }

        }

        if (FireCool.activeSelf == true)
        {
            if (da.fireCoolCounter > 0)
            {
                if (da.fireCoolCounter == da.fireTime)
                    FireCool.transform.GetChild(0).GetComponent<Image>().color = coolColor;
                int a = (int)da.fireCoolCounter + 1;
                FireCoolText.text = a.ToString();
            }
            else
            {
                FireCool.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                FireCoolText.text = "";
            }

        }
    }
}
