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
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("demo Dani");
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Debug.Log("Loading");
    }
}
