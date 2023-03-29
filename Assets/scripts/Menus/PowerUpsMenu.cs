using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsMenu : MonoBehaviour
{
    public static PowerUpsMenu instance;
    public GameObject pauseMenu;
    public GameObject player;
    PlayerAbilitys pa;
    PlayerStatus status;
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
        status = player.GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isPaused)
            {
                isPaused = false;
                pauseMenu.SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.X))
        {
            Time.timeScale = 0;
            isPaused = true;
            pauseMenu.SetActive(true);
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
        if (pa.human[3] != true && status.money >= d1Cost && pa.demon[0] == false)
        {
            pa.demon[0] = true;
            status.money -= d1Cost;
        }
    }
    public void AbilityD2()
    {
        if (pa.human[2] != true && pa.demon[0] == true && status.money >= d2Cost && pa.demon[1] == false)
        {
            pa.demon[1] = true;
            status.money -= d2Cost;
        }
    }
    public void AbilityD3()
    {
        if (pa.human[1] != true && pa.demon[1] == true && status.money >= d3Cost && pa.demon[2] == false)
        {
            pa.demon[2] = true;
            status.money -= d3Cost;
        }
    }
    public void AbilityD4()
    {
        if (pa.human[0] != true && pa.demon[2] == true && status.money >= d4Cost && pa.demon[3] == false)
        {
            pa.demon[3] = true;
            status.money -= d4Cost;
        }
    }



    public void AbilityH1()
    {
        if (pa.demon[3] != true && status.money >= h1Cost && pa.human[0] == false)
        {
            pa.human[0] = true;
            status.money -= h1Cost;
        }
    }
    public void AbilityH2()
    {
        if (pa.demon[2] != true && pa.human[0] == true && status.money >= h2Cost && pa.human[1] == false)
        {
            pa.human[1] = true;
            status.money -= h2Cost;
        }
    }
    public void AbilityH3()
    {
        if (pa.demon[1] != true && pa.human[1] == true && status.money >= h3Cost && pa.human[2] == false)
        {
            pa.human[2] = true;
            status.money -= h3Cost;
        }
    }
    public void AbilityH4()
    {
        if (pa.demon[0] != true && pa.human[2] == true && status.money >= h4Cost && pa.human[3] == false)
        {
            pa.human[3] = true;
            status.money -= h4Cost;
        }
    }
}
