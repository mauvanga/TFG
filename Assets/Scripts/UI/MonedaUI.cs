using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonedaUI : MonoBehaviour
{
    public int totalMonedas;
    [SerializeField] private TMP_Text textoMonedas;
    public AudioSource sonidoMoneda;

    private void Start()
    {
        Moneda.sumaMoneda += SumarMonedas; 
        int monedas = SaveManager.Instancia.datosGuardados.monedas;
        if (monedas != 0)
        {
            totalMonedas = monedas;
            textoMonedas.text = "Monedas: " + monedas;
        }
    }

    public void SumarMonedas(int moneda)
    {
        sonidoMoneda.Play();
        totalMonedas += moneda;
        SaveManager.Instancia.datosGuardados.monedas += moneda;
        textoMonedas.text = "Moedas: " + totalMonedas.ToString();
    }

    private void OnDestroy()
    {
        Moneda.sumaMoneda -= SumarMonedas;
    }
}
