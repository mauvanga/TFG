using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class Objeto : MonoBehaviour
{
    [SerializeField]
    public string nombre;
    public string descripcion;
    public Sprite imagenObjeto;

    public Efectos efecto;

    [SerializeField]
    public int cantidad;

    ObjetoInventario objetoInterfaz;

    public void Start()
    {
        objetoInterfaz = GetComponent<ObjetoInventario>();
        objetoInterfaz.Inicializar(imagenObjeto, cantidad, nombre);

        GetComponent<Button>().onClick.AddListener(LlamarEfecto);
    }

    public void AnadirCantidad(int addAmmount)
    {
        cantidad += addAmmount;

        objetoInterfaz.AnadirCantidad(addAmmount);

    }



    public void LlamarEfecto()
    {
        if(efecto != Efectos.Nada)
        {
            cantidad--;
            objetoInterfaz.SetCantidad(cantidad);
            GetComponentInParent<EfectosObjetoManager>().AplicarEfecto(efecto);

            if (cantidad == 0)
            {
                GetComponentInParent<Inventario>().QuitarObjeto(nombre);
            }
        }
    }


}
