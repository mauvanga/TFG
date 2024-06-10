using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemigosMovimiento : MonoBehaviour
{
    public Transform personaje;
    public Transform[] puntosRuta;
    private NavMeshAgent agente;
    public LayerMask layer;

    public bool esDanado;
    public Vector3 empuDir;
    public SpriteRenderer sprite;

    bool enDanoCorutine;

    private void Awake()
    {

        agente = GetComponent<NavMeshAgent>();
        
    }

    private void Start()
    {
        agente.updateRotation = false;
        agente.updateUpAxis = false;
        personaje = FindObjectOfType<Personaje>().gameObject.transform;
    }



    // Update is called once per frame
    private void Update()
    {
        //Ray2D rayo = new Ray2D((personaje.position-transform.position).normalized,transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (personaje.position - transform.position).normalized,Mathf.Infinity,layer);

        if (hit && hit.transform.CompareTag("Player") && !esDanado)
        {
            if (Vector3.Distance(personaje.position, transform.position) <= 20)
            {
                agente.SetDestination(personaje.position);
            }

            if (Vector3.Distance(personaje.position, transform.position) <= 4f)
            {
                agente.SetDestination(transform.position);
            }
        } else if (esDanado)
        {
            if (!enDanoCorutine)
            {
                StartCoroutine(EmpujeDano());
            }
        }

    }

    IEnumerator EmpujeDano()
    {
        enDanoCorutine = true;

        agente.SetDestination(transform.position + -empuDir.normalized * 2);

        agente.velocity *= 3;
        sprite.color = Color.red;

        yield return new WaitForSeconds(0.75f);

        sprite.color = Color.white;
        esDanado = false;
        enDanoCorutine = false;
        agente.velocity /= 3;


    }
    
}
