using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsMenu : MonoBehaviour
{
    public static PowerUpsMenu instance;
    public GameObject pauseMenu;
    public GameObject player;
    private PlayerAbilitys pa;
    public int d1Cost;
    public int d2Cost;
    public int d3Cost;
    public int d4Cost;
    public int h1Cost;
    public int h2Cost;
    public int h3Cost;
    public int h4Cost;

    public bool isPaused
    {
        get;
        private set;
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        pa = player.GetComponent<PlayerAbilitys>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
                pauseMenu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
                pauseMenu.SetActive(true);
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void Tentacle()
    {
        if (pa.human[3] != true && player.GetComponent<PlayerLife>().money >= d1Cost)
        {
            pa.demon[0] = true;
            player.GetComponent<PlayerLife>().money -= d1Cost;
        }
    }
    public void AbilityD2()
    {
        if (pa.human[2] != true && pa.demon[0] == true && player.GetComponent<PlayerLife>().money >= d2Cost)
        {
            pa.demon[1] = true;
            player.GetComponent<PlayerLife>().money -= d2Cost;
        }
    }
    public void AbilityD3()
    {
        if (pa.human[1] != true && pa.demon[1] == true && player.GetComponent<PlayerLife>().money >= d3Cost)
        {
            pa.demon[2] = true;
            player.GetComponent<PlayerLife>().money -= d3Cost;
        }
    }
    public void AbilityD4()
    {
        if (pa.human[0] != true && pa.demon[2] == true && player.GetComponent<PlayerLife>().money >= d4Cost)
        {
            pa.demon[3] = true;
            player.GetComponent<PlayerLife>().money -= d4Cost;
        }
    }



    public void AbilityH1()
    {
        if (pa.demon[3] != true && player.GetComponent<PlayerLife>().money >= h1Cost)
        {
            pa.human[0] = true;
            player.GetComponent<PlayerLife>().money -= h1Cost;
        }
    }
    public void AbilityH2()
    {
        if (pa.demon[2] != true && pa.human[0] == true && player.GetComponent<PlayerLife>().money >= h2Cost)
        {
            pa.human[1] = true;
            player.GetComponent<PlayerLife>().money -= h2Cost;
        }
    }
    public void AbilityH3()
    {
        if (pa.demon[1] != true && pa.human[1] == true && player.GetComponent<PlayerLife>().money >= h3Cost)
        {
            pa.human[2] = true;
            player.GetComponent<PlayerLife>().money -= h3Cost;
        }
    }
    public void AbilityH4()
    {
        if (pa.demon[0] != true && pa.human[2] == true && player.GetComponent<PlayerLife>().money >= h4Cost)
        {
            pa.human[3] = true;
            player.GetComponent<PlayerLife>().money -= h4Cost;
        }
    }
}
