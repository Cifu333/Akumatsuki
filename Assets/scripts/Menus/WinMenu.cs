using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public static WinMenu instance;
    public GameObject BadMenu;
    public GameObject GoodMenu;
    public GameObject NeutralMenu;
    GameObject player;
    PlayerAbilitys pa;
    public bool isPaused
    {
        get;
        private set;
    }

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        pa = player.GetComponent<PlayerAbilitys>();
    }

    // Start is called before the first frame update
    void Start()
    {
        BadMenu.SetActive(false);
        GoodMenu.SetActive(false);
        NeutralMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerStatus>().win == true && pa.demon[0] == true && pa.demon[1] == true)
        {
            isPaused = true;
            Time.timeScale = 0;
            BadMenu.SetActive(true);

        }
        if (player.GetComponent<PlayerStatus>().win == true && pa.demon[0] == true && pa.human[0] == true)
        {
            isPaused = true;
            Time.timeScale = 0;
            NeutralMenu.SetActive(true);

        }
        if (player.GetComponent<PlayerStatus>().win == true && pa.human[0] == true && pa.human[1] == true)
        {
            isPaused = true;
            Time.timeScale = 0;
            GoodMenu.SetActive(true);

        }
    }

    public void Continue()
    {
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
