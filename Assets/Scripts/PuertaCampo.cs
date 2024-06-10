using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaCampo : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.HasKey("tieneLlave"))
        {
            Destroy(this.gameObject);
        }
    }
}
