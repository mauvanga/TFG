using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tienda : MonoBehaviour
{
    [SerializeField] private GameObject tienda;
    private bool isPlayerInRange;
    public GameObject inventarioUI;



    void Update()
    {

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (tienda.activeSelf)
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                tienda.SetActive(false);
                inventarioUI.SetActive(false);

            }
            else
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                tienda.SetActive(true);
                inventarioUI.SetActive(true);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) ;
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 1;
            isPlayerInRange = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            tienda.SetActive(false);
            inventarioUI.SetActive(false);
        }
    }

    public void Desactivar()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        tienda.SetActive(false);
        inventarioUI.SetActive(false);
    }
}
