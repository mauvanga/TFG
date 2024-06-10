using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroeDamageable : IDamageable
{
    public static bool isAlive;
    public static bool isDamaged;
    public VidaUI vidaJugador;
    public GameObject pantallaMuerte;
    public AudioSource dano;
    public AudioSource muerte;

    void Start()
    {
        base.Start();

        int vidaDatosGuardados = SaveManager.Instancia.datosGuardados.vida;

        
        if (vidaDatosGuardados != 0)
        {
            vida = vidaDatosGuardados;
        }
        else
        {
            SaveManager.Instancia.datosGuardados.vida = vida;
        }

        vidaJugador.Inicializar(vidaMax);
        vidaJugador.QuitarVida(vida);
        isAlive = true;

    }


    public override void Die()
    {
        if (isAlive)
        {
            muerte.Play();
        }
        vidaJugador.QuitarVida(vida);
        isAlive = false;
        Cursor.lockState = CursorLockMode.Confined;
        StartCoroutine(MostrarPantallaMuerte());
    }

    public override void DamageEffect(Transform other)
    {
        if (isAlive)
        {
            Debug.Log("Ouch");

            dano.Play();

            isDamaged = true;

            Invoke("ResetDamaged", 0.3f);

            Vector2 direccion = transform.position - other.position;

            GetComponentInParent<Rigidbody2D>().AddForce(direccion.normalized * 5, ForceMode2D.Impulse);

            SaveManager.Instancia.datosGuardados.vida = vida;
            vidaJugador.QuitarVida(vida);
        }

    }

    IEnumerator MostrarPantallaMuerte()
    {
        yield return new WaitForSeconds(2);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        pantallaMuerte.SetActive(true);
    }

    void ResetDamaged()
    {
        isDamaged = false;
    }

    public void Heal(int amount)
    {
        if(vida + amount > vidaMax)
        {
            vida = vidaMax;
        }else
        {
            vida += amount;
        }
        SaveManager.Instancia.datosGuardados.vida = vida;
    }

}
