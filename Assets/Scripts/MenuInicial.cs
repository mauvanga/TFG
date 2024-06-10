using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{

    public TMP_Text textoComenzar;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("datosGuardados"))
        {
            textoComenzar.text = "CARGAR PARTIDA";
        }

    }

    public void Comezar(){
        if (PlayerPrefs.HasKey("datosGuardados"))
        {
            SaveManager.Instancia.Continue();
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Sair(){
        Debug.Log("Sair...");
        Application.Quit();
    }
}
