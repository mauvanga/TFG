using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventario : MonoBehaviour
{
    public GameObject panelObjetos;
    Dictionary<string, Objeto> objetosInventario = new Dictionary<string, Objeto>();
    List<DatosInventario> datosInventarioGuardar = new List<DatosInventario>();

    public GameObject objetoTemplate;
    List<DatosObjeto> listaDatosObjeto = new List<DatosObjeto>();

    public GameObject invent;

    void Start()
    {
        if(SaveManager.Instancia.datosGuardados.objetosInventario != null)
        {
            objetosInventario = new Dictionary<string, Objeto>();
            foreach(string obj in SaveManager.Instancia.datosGuardados.objetosInventario)
            {
                DatosInventario di = JsonUtility.FromJson<DatosInventario>(obj);

                AddObject(di.nombre, di.descripcion, di.imagenObjeto, di.efecto, di.cantidad);


            }
        }
        invent.SetActive(false);
    }


    void Update()
    {
       
    }

    public void AddObject(string nombr, string descrip, Sprite imag, Efectos effect, int cantidad)
    {
        Debug.Log("asdas "+ nombr + " asda " + objetosInventario.ContainsKey(nombr));
        if (objetosInventario.ContainsKey(nombr))
        {
            Objeto salida;
            objetosInventario.TryGetValue(nombr, out salida);

            salida.AnadirCantidad(cantidad);

        }
        else
        {
            GameObject visualObjeto = Instantiate(objetoTemplate, panelObjetos.transform);

            Objeto nuevoObjeto = visualObjeto.AddComponent<Objeto>();

            nuevoObjeto.nombre = nombr;
            nuevoObjeto.descripcion = descrip;
            nuevoObjeto.imagenObjeto = imag;
            nuevoObjeto.efecto = effect;

            nuevoObjeto.cantidad = cantidad;

            objetosInventario.TryAdd(nombr, nuevoObjeto);
        }
    }


    public void AddObject(DatosObjeto objetoAnadido, int cantidad)
    {
        if (objetosInventario.ContainsKey(objetoAnadido.nombre))
        {
            Objeto salida;
            objetosInventario.TryGetValue(objetoAnadido.nombre, out salida);

            salida.AnadirCantidad(cantidad);

            DatosInventario di = new DatosInventario();
            di.nombre = objetoAnadido.nombre;
            di.cantidad = cantidad;
            di.descripcion = objetoAnadido.descripcion;
            di.efecto = objetoAnadido.efecto;
            di.imagenObjeto = objetoAnadido.imagenObjeto;

            int oldCant = 0;
            string dataToRemove = "";
            foreach(string data in SaveManager.Instancia.datosGuardados.objetosInventario)
            {
                DatosInventario aux = JsonUtility.FromJson<DatosInventario>(data);

                if (aux.nombre == di.nombre)
                {
                    dataToRemove = data;
                    oldCant = aux.cantidad;
                    break;
                }
            }

            SaveManager.Instancia.datosGuardados.objetosInventario.Remove(dataToRemove);

            di.cantidad += oldCant;
            SaveManager.Instancia.datosGuardados.objetosInventario.Add(JsonUtility.ToJson(di));

        }
        else
        {
            GameObject visualObjeto = Instantiate(objetoTemplate, panelObjetos.transform);

            Objeto nuevoObjeto = visualObjeto.AddComponent<Objeto>();

            nuevoObjeto.nombre = objetoAnadido.nombre;
            nuevoObjeto.descripcion = objetoAnadido.descripcion;
            nuevoObjeto.imagenObjeto = objetoAnadido.imagenObjeto;
            nuevoObjeto.efecto = objetoAnadido.efecto;

            nuevoObjeto.cantidad = cantidad;

            objetosInventario.TryAdd(objetoAnadido.nombre, nuevoObjeto);
            DatosInventario di = new DatosInventario();
            di.nombre = objetoAnadido.nombre;
            di.cantidad = cantidad;
            di.descripcion = objetoAnadido.descripcion;
            di.efecto = objetoAnadido.efecto;
            di.imagenObjeto = objetoAnadido.imagenObjeto;

            SaveManager.Instancia.datosGuardados.objetosInventario.Add(JsonUtility.ToJson(di));
        }
    }

    internal void QuitarObjeto(string name)
    {
        if (objetosInventario.ContainsKey(name))
        {
            Objeto salida;
            objetosInventario.TryGetValue(name, out salida);

            objetosInventario.Remove(name);

            Destroy(salida.gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickUpObjeto objetoSulo = collision.gameObject.GetComponent<PickUpObjeto>();

        if (objetoSulo != null)
        {
            AddObject(objetoSulo.datosObjeto, objetoSulo.cantidad);
        }
    }

    [System.Serializable]
    struct DatosInventario
    {

        public string nombre;
        public int cantidad;
        public string descripcion;
        public Sprite imagenObjeto;

        public Efectos efecto;

    }

    [System.Serializable]
    class DatosFinales
    {

       [SerializeField]public List<DatosObjeto> datos = new List<DatosObjeto>();
       [SerializeField]public List<int> cantidades = new List<int>();

    }

}


