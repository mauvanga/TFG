using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPausa : MonoBehaviour
{
    
    public void MenuInicio()
    {
        SceneManager.LoadScene(0);
    }

    public void Continuar()
    {
        if (PlayerPrefs.HasKey("datosGuardados"))
        {
            SaveManager.Instancia.Continue();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }



}
