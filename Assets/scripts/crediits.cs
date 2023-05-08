using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class crediits : MonoBehaviour
{

    public float duration = 10;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BackToMenu", duration);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    } 
}
