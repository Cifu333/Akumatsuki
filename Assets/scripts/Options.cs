using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    [SerializeField] private AudioMixer AudioMixer;
   public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void CambiarVolumen(float volumen)
    {
        AudioMixer.SetFloat("Volumen", volumen);
    }
}
