using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    public static DeadMenu instance;
    public GameObject pauseMenu;
    PlayerStatus ps;
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
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.hp <= 0)
        {
            pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        }
    }

    public void Retry()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().hp = ps.maxHP;
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(-97, -4);
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().colorO;
    }
}
