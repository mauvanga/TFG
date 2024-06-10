using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Nuevo Objeto", menuName ="Inventario/Objeto")]
public class DatosObjeto : ScriptableObject
{
    public string nombre;
    public string descripcion;
    public Sprite imagenObjeto;

    public Efectos efecto;


}
