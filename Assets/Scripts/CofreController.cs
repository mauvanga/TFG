using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofreController : MonoBehaviour
{
    public GameObject mensaje;

    bool dentroTrigger = false;
    bool unoUOtro = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        dentroTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dentroTrigger = false;
    }

    private void Update()
    {
        if (mensaje.activeSelf && unoUOtro)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1;
                unoUOtro = false;
                mensaje.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && dentroTrigger && unoUOtro)
        {
            Time.timeScale = 0;
            mensaje.SetActive(true);
            unoUOtro = false;
            PlayerPrefs.SetInt("tieneLlave",1);
        }

        unoUOtro = true;
    }

    void DesactivarMensaje()
    {
        mensaje.SetActive(false);
    }
}
