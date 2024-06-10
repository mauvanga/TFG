using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public delegate void SumaMoneda(int moneda);
    public static event SumaMoneda sumaMoneda;

    public SeguimientoMoneda seguimiento;

    [SerializeField] public int cantidadMonedas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (sumaMoneda != null)
            {
                SumarMoneda();
                Destroy(this.gameObject);
            }
        }
    }

    private void SumarMoneda()
    {
        sumaMoneda(cantidadMonedas);
    }

}
