using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public Transform position;

    public TMP_Text textoGuardado;

    private bool isPlayerInRange;

    public TMP_Text guardadoCorrecto;

    void Update()
    {

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SaveManager.Instancia.datosGuardados.spawnPosition = position.position;
            SaveManager.Instancia.datosGuardados.sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SaveManager.Instancia.datosGuardados.escena = SceneManager.GetActiveScene().name;

            Debug.Log(SaveManager.Instancia.datosGuardados.escena);
            SaveManager.Instancia.SaveData();
            guardadoCorrecto.enabled = true;
            Invoke("OcultarMensaje", 2f);
        }




    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //mirar ese ;
        if (collision.gameObject.CompareTag("Player")) 

        {
            isPlayerInRange = true;
            textoGuardado.enabled = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            isPlayerInRange = false;
            textoGuardado.enabled = false;
        }
    }

    void OcultarMensaje()
    {
        guardadoCorrecto.enabled = false;
    }
}

