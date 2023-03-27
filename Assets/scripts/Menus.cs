using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public static Menus instance;
    public GameObject pauseMenu;
    public PlayerStatus ps;
    public Door d;
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
        if (ps.hp <= 0)
        {
            pauseMenu.SetActive(true);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("demo");
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Debug.Log("Loading");
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
