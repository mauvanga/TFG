using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IDamageable : MonoBehaviour
{

    protected int vida;
    public int vidaMax;

    protected void Start()
    {
        vida = vidaMax;
    }

    public void GetDamage(int danho, Transform other)
    {
        vida -= danho;

        if (vida <= 0)
        {
            Die();
        }
        else
        {
            DamageEffect(other);
        }

    }

    public virtual void Die()
    {

    }

    public virtual void DamageEffect(Transform other)
    {

    }

}
