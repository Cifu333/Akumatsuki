using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsMenu : MonoBehaviour
{
    public static PowerUpsMenu instance;
    public GameObject pauseMenu;
    GameObject player;
    PlayerAbilitys pa;
    PlayerStatus status;
    private bool isThere;
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
        player = GameObject.FindGameObjectWithTag("Player");
        isPaused = false;
        isThere = false;
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
        if (isThere == true && Input.GetKeyDown(KeyCode.X))
        {
            Time.timeScale = 0;
            isPaused = true;
            pauseMenu.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isThere = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isThere = false;
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
        if (pa.human[1] != true && status.money >= d1Cost && pa.demon[0] == false)
        {
            pa.demon[0] = true;
            status.money -= d1Cost;
        }
    }
    public void AbilityD2()
    {
        if (pa.human[0] != true && pa.demon[0] == true && status.money >= d2Cost && pa.demon[1] == false)
        {
            pa.demon[1] = true;
            status.money -= d2Cost;
        }
    }



    public void AbilityH1()
    {
        if (pa.demon[1] != true && status.money >= h1Cost && pa.human[0] == false)
        {
            pa.human[0] = true;
            status.money -= h1Cost;
        }
    }
    public void AbilityH2()
    {
        if (pa.demon[0] != true && pa.human[0] == true && status.money >= h2Cost && pa.human[1] == false)
        {
            pa.human[1] = true;
            status.money -= h2Cost;
        }
    }
}
