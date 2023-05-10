using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public float hp;
    private PlayerStatus ps;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        image = GetComponent<Image>();
        hp = ps.hp;
    }


    // Update is called once per frame
    void Update()
    {
        hp = ps.hp;
        image.fillAmount = hp / ps.maxHP;
    }
}
