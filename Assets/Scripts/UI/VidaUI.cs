using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaUI : MonoBehaviour
{
    public GameObject corazon;
    public List<GameObject> corazonesActuales = new List<GameObject>();

    public void Inicializar(int vidaMax)
    {
        for(int i=0; i < 3; i++)
        {
            GameObject corazonCreado = Instantiate(corazon, transform.position, Quaternion.identity);
            corazonCreado.transform.SetParent(transform);
            //vector3.one = (1,1,1)
            corazonCreado.transform.localScale = Vector3.one;
            corazonesActuales.Add(corazonCreado);
        }
    }

    public void QuitarVida(int vidaActual)
    {
        for(int i=0; i< corazonesActuales.Count;i++)
        {
            if (i < vidaActual)
            {
                corazonesActuales[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                corazonesActuales[i].GetComponent<Image>().color = Color.black;
            }
        }
    }

    public void CurarCorazon(int vidaActual)
    {
        for (int i = 0; i < corazonesActuales.Count; i++)
        {
            if (i < vidaActual)
            {
                corazonesActuales[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                corazonesActuales[i].GetComponent<Image>().color = Color.black;
            }
        }
    }

}
