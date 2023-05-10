using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoolDownUI : MonoBehaviour
{
    public GameObject TentacleCool;
    public GameObject TentacleCoolText;
    public GameObject FireCool;
    public GameObject FireCoolText;
    public GameObject EMPCool;
    public GameObject EMPCoolText;
    public GameObject RuneCool;
    public GameObject RuneCoolText;
    GameObject player;
    DemonAbilities da;
    HumanAbilities ha;
    PlayerAbilitys pa;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        da = player.GetComponent<DemonAbilities>();
        ha = player.GetComponent<HumanAbilities>();
        pa = player.GetComponent<PlayerAbilitys>();
        TentacleCoolText.SetActive(false);
        FireCoolText.SetActive(false);
        EMPCoolText.SetActive(false);
        RuneCoolText.SetActive(false);
        TentacleCool.SetActive(false);
        FireCool.SetActive(false);
        EMPCool.SetActive(false);
        RuneCool.SetActive(false);
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

        if (TentacleCool.activeSelf == true) { }
    }
}
