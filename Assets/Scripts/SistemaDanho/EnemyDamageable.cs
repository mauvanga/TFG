using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageable : IDamageable
{

    public ParticleSystem particulasMuelte;
    public AudioSource dano;

    public EnemigosMovimiento movimiento;

    void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void Die()
    {
        dano.Play();

        particulasMuelte.Play();
        GetComponent<BoxCollider2D>().enabled = false;

        Destroy(transform.parent.gameObject, 0.5f);
    }

    public override void DamageEffect(Transform other)
    {
        dano.Play();



        movimiento.esDanado = true;
        movimiento.empuDir = other.position - transform.position;

    }

    

}
