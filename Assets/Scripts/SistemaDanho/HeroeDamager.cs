using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroeDamager : MonoBehaviour
{
    public float attackTimer = 1;

    bool canDamage = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable target = collision.GetComponent<IDamageable>();
        if (target != null && canDamage && collision.gameObject.tag == "Enemy")
        {
            canDamage = false;
            target.GetDamage(1, this.transform.parent);
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
