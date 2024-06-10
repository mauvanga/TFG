using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeIA : MonoBehaviour
{
    //Lista de estados del enemigo

    public Transform posInicioRayos;
    public Transform posFinalRayos;
    public GameObject prefabRayos;
    public float tiempoEntreDisparosRayos;
    private bool ataqueRayos;
    private bool finAtaqueRayos;
    private bool moverIzquierda;
    private int cantidadAtaques = 0;

    public Transform posIndefenso;
    private bool indefenso;
    private Vector3 lastTargetPos;
    private BoxCollider2D hitBox;

    public Transform posInicioLateral;
    public Transform posFinalLateral;
    public GameObject prefabLateral;
    public float tiempoEntreDisparosLateral;
    private bool ataqueLateral;
    private bool finAtaqueLateral;
    private bool moverIzquierdaLateral;
    private int cantidadAtaquesLateral = 0;


    bool canShoot = true;

    float timer = 0;
    enum AI
    {
        None,
        Indefenso,
        AtaqueRayos,
        AtaqueLateral
    }
    AI estado;

    private void Start()
    {
        estado = AI.None;
        hitBox = GetComponent<BoxCollider2D>();
    }

    public void IniciarBatalla()
    {
        int primerAtaque = Random.Range(0, 2);

        if (primerAtaque == 0)
        {
            estado = AI.AtaqueRayos;
        }
        else
        {
            estado = AI.AtaqueLateral;
        }
    }

    void Update()
    {

         //Ejecutar dependiendo del estado
        switch (estado)
        {
            case AI.Indefenso:
                Indefenso();
                break;
            case AI.AtaqueRayos:
                AtaqueRayos();
                break;
            case AI.AtaqueLateral:
                AtaqueLateral();
                break;
        }

    }

    void Indefenso()
    {
        if(indefenso)
        {
            timer += Time.deltaTime / 4;
            if (timer > 1)
            {
                indefenso = false;
                timer = 0;
                int ataque = Random.Range(0, 2);

                if (ataque == 0)
                {
                    estado = AI.AtaqueRayos;
                }
                else
                {
                    estado = AI.AtaqueLateral;
                }

            }
        }else
        {

            Debug.Log("Valor tiempo indefenso :  " + timer);
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(lastTargetPos, posIndefenso.position, timer);

            if(timer > 1)
            {
                timer = 0;
                indefenso = true;
                transform.position = posIndefenso.position;
            }
        }
    }

    void AtaqueRayos()
    {
        if (ataqueRayos)
        {
            if (cantidadAtaques > 2)
            {
                estado = AI.Indefenso;
                ataqueRayos = false;
                moverIzquierda = false;
                cantidadAtaques = 0;
                hitBox.enabled = false;
            } else
            {
                if (moverIzquierda)
                {
                    timer += Time.deltaTime / 3;
                    transform.position = Vector3.Lerp(posFinalRayos.position, posInicioRayos.position, timer);

                    if (timer > 1)
                    {
                        transform.position = posInicioRayos.position;
                        timer = 0;
                        moverIzquierda = false;
                        cantidadAtaques++;
                        lastTargetPos = posInicioRayos.position;
                    }
                }
                else
                {
                    timer += Time.deltaTime / 3;
                    transform.position = Vector3.Lerp(posInicioRayos.position, posFinalRayos.position, timer);

                    if (timer > 1)
                    {
                        lastTargetPos = posFinalRayos.position;
                        transform.position = posFinalRayos.position;
                        timer = 0;
                        moverIzquierda = true;
                        cantidadAtaques++;
                    }
                }
            }

            if(canShoot)
            {
                canShoot = false;

                Instantiate(prefabRayos, transform.position, Quaternion.identity);

                Invoke("ResetShoot", tiempoEntreDisparosRayos);
            }
           

        }
        else
        {
            timer += Time.deltaTime * 2;
            transform.position = Vector3.Lerp(posIndefenso.position, posInicioRayos.position, timer);
            if (timer > 1)
            {
                timer = 0;
                transform.position = posInicioRayos.position;
                ataqueRayos = true;
                hitBox.enabled = true;
            }
        }
    }
    void AtaqueLateral()
    {
        if (ataqueLateral)
        {
            if (cantidadAtaquesLateral > 2)
            {
                estado = AI.Indefenso;
                ataqueLateral = false;
                moverIzquierdaLateral = false;
                cantidadAtaquesLateral = 0;
                hitBox.enabled = false;
            } else
            {
                if (moverIzquierdaLateral)
                {
                    timer += Time.deltaTime / 2;
                    transform.position = Vector3.Lerp(posFinalLateral.position, posInicioLateral.position, timer);

                    if (timer > 1)
                    {
                        lastTargetPos = posInicioLateral.position;
                        transform.position = posInicioLateral.position;
                        timer = 0;
                        moverIzquierdaLateral = false;
                        cantidadAtaquesLateral++;
                    }
                }
                else
                {
                    Debug.Log(timer);
                    timer += Time.deltaTime / 3;
                    transform.position = Vector3.Lerp(posInicioLateral.position, posFinalLateral.position, timer);

                    if (timer > 1)
                    {
                        lastTargetPos = posFinalLateral.position;
                        transform.position = posFinalLateral.position;
                        timer = 0;
                        moverIzquierdaLateral = true;
                        cantidadAtaquesLateral++;
                    }
                }
            }

            if (canShoot)
            {
                canShoot = false;

                Instantiate(prefabLateral, transform.position, Quaternion.identity);

                Invoke("ResetShoot", tiempoEntreDisparosLateral);
            }

        }
        else
        {
            timer += Time.deltaTime * 2;
            transform.position = Vector3.Lerp(posIndefenso.position, posInicioLateral.position, timer);
            if (timer > 1)
            {
                timer = 0;
                transform.position = posInicioLateral.position;
                ataqueLateral = true;
                hitBox.enabled = true;
            }
        }
    }

    public void ResetShoot()
    {
        canShoot = true;
    }

}
