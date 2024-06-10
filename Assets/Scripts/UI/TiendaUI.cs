using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiendaUI : MonoBehaviour
{

    public Inventario inventario;
    public MonedaUI moneda;

    public void PrecioObjeto(string objeto)
    {
        switch (objeto)
        {
            case "SaludPeque":

                break;
            case "SaludMedia":

                break;
        }
    }

    public void AdquirirObjeto(ObjetoTienda objeto)
    {
        if (moneda.totalMonedas >= objeto.precio)
        {
            moneda.SumarMonedas(-objeto.precio);
            inventario.AddObject(objeto.datosObjeto, objeto.cantidad);
        }
    }
}
