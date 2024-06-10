using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{

    public static SaveManager Instancia { get; private set; } //singleton estatico para que todo el mundo pueda acceder a el pero solo podemos editarlo desde la clase
    public SaveData datosGuardados;

    public bool loadingData;

    public bool enableLoadData;

    void Awake()
    {
        //si existe una Instancia y no soy yo implosiono
        if (Instancia != null && Instancia != this)
        {
            Destroy(this);
        }
        else
        {
            Instancia = this;
            DontDestroyOnLoad(this.gameObject);
        }

        //PlayerPrefs.DeleteAll();  //SOLO USAR SI EL GUARDADO DA ERRORES

        LoadData();
    }

    private void Start()
    {
        if (datosGuardados.escena != null && datosGuardados.escena.Length > 0)
        {
            SceneManager.LoadScene(datosGuardados.escena);
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    SaveData();
        //}
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("datosGuardados") && enableLoadData)
        {
            string datosCargados = PlayerPrefs.GetString("datosGuardados");
            datosGuardados = JsonUtility.FromJson<SaveData>(datosCargados);
            

        }
        else
        {
            datosGuardados = new SaveData();
        }

    }

    public void Continue()
    {
        if (PlayerPrefs.HasKey("datosGuardados"))
        {
            string datosCargados = PlayerPrefs.GetString("datosGuardados");
            datosGuardados = JsonUtility.FromJson<SaveData>(datosCargados);
            loadingData = true;
            SceneManager.LoadScene(datosGuardados.escena);
        }
    }

    public void SaveData()
    {
        /*SeguimientoMoneda[] monedas = FindObjectsOfType<SeguimientoMoneda>(true);
        datosGuardados.seguimientoMonedas = monedas;*/

        string datos = JsonUtility.ToJson(datosGuardados);
        PlayerPrefs.SetString("datosGuardados", datos);
    }


}

[Serializable]
public class SaveData
{
    public Vector3 spawnPosition;
    public int vida;
    public string escena;
    public int monedas;
    public List<string> objetosInventario = new List<string>();
    public int sceneIndex;
    //public SeguimientoMoneda[] seguimientoMonedas;
}
