using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjetoInventario : MonoBehaviour
{
    public Image imagen;
    public TMP_Text cantidad;
    public TMP_Text nombre;

    int cantidadTotal = 0;
    public void Inicializar(Sprite imagenObjeto, int cantidadObjeto, string nombreObjeto)
    {
        cantidadTotal += cantidadObjeto;
        imagen.sprite = imagenObjeto;
        cantidad.text = cantidadTotal.ToString();
        nombre.text = nombreObjeto;
    }

    public void AnadirCantidad(int cantidadObjeto)
    {
        cantidadTotal += cantidadObjeto;
        cantidad.text = cantidadTotal.ToString();
    }
    public void SetCantidad(int cantidadObjeto)
    {
        cantidadTotal = cantidadObjeto;
        cantidad.text = cantidadTotal.ToString();
    }
}
