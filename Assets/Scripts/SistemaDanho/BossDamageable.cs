using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDamageable : IDamageable
{
    public ParticleSystem particulasMuelte;
    public AudioSource dano;

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

        SceneManager.LoadScene(29);
    }

    public override void DamageEffect(Transform other)
    {
        dano.Play();

    }
}
