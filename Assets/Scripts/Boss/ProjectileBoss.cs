using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBoss : MonoBehaviour
{

    public float speed;
    public Vector3 dir;

    void Update()
    {
        transform.position += dir * Time.deltaTime * speed;        
    }
}
