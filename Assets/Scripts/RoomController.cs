using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviour
{
    public GameObject[] enemigos;

    public GameObject[] puertas;

    void Start()
    {
        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "murcielagos"))
        {
            foreach (GameObject ene in enemigos)
            {
                Destroy(ene);
            }

            foreach(GameObject puer in puertas)
            {
                Destroy(puer);
            }

            Destroy(this.gameObject);
        }        
    }

    void Update()
    {
        int nullCount = 0;
        foreach (GameObject ene in enemigos)
        {
            if (ene == null) nullCount++;
        }

        if (enemigos.Length == nullCount)
        {
            foreach (GameObject puer in puertas)
            {
                Destroy(puer);
            }
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name+"murcielagos", 1);
            Destroy(this.gameObject);
        }
    }
}
