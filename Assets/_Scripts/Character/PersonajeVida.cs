using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PersonajeVida : VidaDefault
{
    public static Action EventoPersonajeDerrotado;

    public bool Derrotado { get; private set; }

    public bool puedeSerCurado => Salud < saludMax;

    private BoxCollider2D _boxCollider2D;
    private PersonajeTemperatura _personajeTemperatura;

    private void Awake()
    {
        _boxCollider2D = GetComponent < BoxCollider2D>();
        _personajeTemperatura = GetComponent <PersonajeTemperatura>();
    }


    protected override void Start()
    {
        base.Start();
        ActualizarBarraVida(Salud, saludMax);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            RecibirDaño(10);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            RestaurarSalud(10);
        }
    }
    public void RestaurarSalud(float cantidad)
    {
        if (puedeSerCurado)
        {
            if (Derrotado)
            {
                return;
            }

            Salud += cantidad;
            if(Salud > saludMax)
            {
                Salud = saludMax;
            }

            ActualizarBarraVida(Salud, saludMax); 
        }
    }

    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        UIManager.Instance.ActualizarVidaPersonaje(vidaActual, vidaMax);
    }


    protected override void PersonajeDerrotado()
    {
        
        _boxCollider2D.enabled = false;
        Derrotado = true;
        EventoPersonajeDerrotado?.Invoke();

        //if(EventoPersonajeDerrotado != null)
        //{
        //    EventoPersonajeDerrotado.Invoke();
        //}
        //Es lo mismo  que EventoPersonajeDerrotado?.Invoke();
    }

    public void RestaurarPersonaje()
    {
        _boxCollider2D.enabled = true;
        Derrotado = false;
        Salud = saludInicial;
        ActualizarBarraVida(Salud, saludInicial);
    }
    
}
