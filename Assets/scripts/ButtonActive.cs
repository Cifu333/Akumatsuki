using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonActive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void  Activar (GameObject Bactive)
    {
        Bactive.SetActive (true);
    }
    public void Desactivar (GameObject Bdesactive)
    {
        Bdesactive.SetActive(false);
    }
}
