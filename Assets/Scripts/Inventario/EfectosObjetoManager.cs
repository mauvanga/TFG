using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Efectos
{
    Nada, 
    PocionSimple,
    PocionNormal,
    PocionFuerte
}


public class EfectosObjetoManager : MonoBehaviour
{

    public VidaUI vidaJugador;
    public HeroeDamageable vidaManager;
    int vida;

    public void AplicarEfecto(Efectos efecto)
    {
        int vidaDatosGuardados = SaveManager.Instancia.datosGuardados.vida;
        vida = vidaDatosGuardados;


        switch (efecto)
        {
            case Efectos.Nada:
                break;
            case Efectos.PocionSimple:
                
                vidaManager.Heal(1);
                vidaJugador.CurarCorazon(vida + 1);
                break;
            case Efectos.PocionNormal:

                vidaManager.Heal(2);
                vidaJugador.CurarCorazon(vida + 2);
                break;
        }
    }

}
