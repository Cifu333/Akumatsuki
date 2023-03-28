using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Money;
    public int Misery;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetInt("Money", 0);
        Money = PlayerPrefs.GetInt("Money");
        PlayerPrefs.GetInt("Misery", 0);
        Misery = PlayerPrefs.GetInt("Misery");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().money = Money;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().misery = Misery;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("Money", Money);
        
        PlayerPrefs.SetInt("Misery", Misery);
    }
}
