using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiseryBar : MonoBehaviour
{
    public float misery;
    private PlayerStatus ps;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        image = GetComponent<Image>();
        misery = ps.misery;
    }


    // Update is called once per frame
    void Update()
    {
        misery = ps.misery;
        image.fillAmount = misery / ps.maxMisery;
    }
}
