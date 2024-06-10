using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float attackTimer = 1;

    bool canDamage = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        IDamageable target = collision.GetComponent<IDamageable>();
        if (target != null && canDamage && collision.gameObject.tag == "Player")
        {
            canDamage = false;
            target.GetDamage(1, this.transform);
            //corutinas para esperar tiempo
            StartCoroutine(AttackCD());
        }
    }

    IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(attackTimer);
        
        canDamage = true;
    }

}