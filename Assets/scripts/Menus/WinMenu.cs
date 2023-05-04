using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public static WinMenu instance;
    public GameObject pauseMenu;
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
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Continue()
    {
        SceneManager.LoadScene("demo");
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Debug.Log("Loading");
    }
}
