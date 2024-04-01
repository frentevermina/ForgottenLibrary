using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PersonajeTemperatura : MonoBehaviour
{
    public static Action EventoPersonajeCongelado;


    [SerializeField] private float temperaturaInicial;
    [SerializeField] private float temperaturaMax;
    [SerializeField] private float perdidaPorSegundo;
    [SerializeField] private float recuperacionPorSegundo;

    public float TemperaturaActual { get; set; }
    public bool SePuedeRestaurar => TemperaturaActual < temperaturaMax;
    public bool isSafe;
    public bool Congelado;


    private BoxCollider2D _boxCollider2D;
    private PersonajeVida _personajeVida;
    private void Awake()
    {
        _personajeVida = GetComponent<PersonajeVida>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

    }
    void Start()
    {


        TemperaturaActual = temperaturaInicial;
        ActualizarBarraTemperatura();
        if (isSafe)
        {
            ZonaSegura();
        }
        else
        {
            ZonaInsegura();
        }


    }

    public void UsarMana(float cantidad)
    {
        if(TemperaturaActual >= cantidad)
        {
            TemperaturaActual -= cantidad;
        }
    }

    public void RestaurarTemperatura(float cantidad)
    {
        if (TemperaturaActual >= temperaturaMax)
        {
            return;
        }
        TemperaturaActual += cantidad;
        if (TemperaturaActual > temperaturaMax)
        {
            TemperaturaActual = temperaturaMax;
        }
        UIManager.Instance.ActualizarTemperaturaPersonaje(TemperaturaActual, temperaturaMax);
    }



    private void Update()
    {

    }




    private void RegenerarTemperatura()
    {
        if (_personajeVida.Salud > 0f && TemperaturaActual < temperaturaMax)
        {
            TemperaturaActual += recuperacionPorSegundo;

            ActualizarBarraTemperatura();
        }
    }

    private void PerderTemperatura()
    {
        if (_personajeVida.Salud > 0f && TemperaturaActual <= temperaturaMax && TemperaturaActual > 0)
        {
            TemperaturaActual -= perdidaPorSegundo;
            ActualizarBarraTemperatura();
            if (TemperaturaActual <= 0)
            {
                PersonajeCongelado();
            }


        }
    }

    private void PersonajeCongelado()
    {
        Debug.Log("me he iniciado pero no hago nada");
        _boxCollider2D.enabled = false;
        Congelado = true;
        EventoPersonajeCongelado?.Invoke();
        CancelInvoke(nameof(EventoPersonajeCongelado));
    }

    public void RecuperarTemperatura()
    {

        TemperaturaActual = temperaturaMax;
        ActualizarBarraTemperatura();
    }
    public void ZonaSegura()
    {
        CancelInvoke(nameof(PerderTemperatura));
        InvokeRepeating(nameof(RegenerarTemperatura), 1, 1);

    }

    public void ZonaInsegura()
    {
        CancelInvoke(nameof(RegenerarTemperatura));
        InvokeRepeating(nameof(PerderTemperatura), 1, 1);


    }

    private void ActualizarBarraTemperatura()
    {
        UIManager.Instance.ActualizarTemperaturaPersonaje(TemperaturaActual, temperaturaMax);
    }



}
