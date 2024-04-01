using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static bool playerCreated;

    [SerializeField] private PersonajeStats stats;
    public PersonajeExperiencia PersonajeExperiencia { get; private set; }
    public PersonajeVida PersonajeVida { get; private set; }
    public AnimationPlayer PersonajeAnimaciones { get; private set; }
    public PersonajeTemperatura PersonajeTemperatura { get; private set; }
    public PersonajeAtaque PersonajeAtaque { get; private set; }


    public string nextID;

 


    private void Awake()
    {
        PersonajeVida = GetComponent<PersonajeVida>();
        PersonajeAnimaciones = GetComponent<AnimationPlayer>();
        PersonajeTemperatura = GetComponent<PersonajeTemperatura>();
        PersonajeExperiencia = GetComponent<PersonajeExperiencia>();
        PersonajeAtaque = GetComponent<PersonajeAtaque>();
    }

    private void Start()
    {

            playerCreated = true;

    }
    public void RestaurarPersonaje()
    {
        PersonajeVida.RestaurarPersonaje();
        PersonajeAnimaciones.RevivirPersonaje();
        PersonajeTemperatura.RecuperarTemperatura();
    }

    private void AtributoRespuesta(TipoAtributo tipo)
    {
        if(stats.PuntosDisponibles <= 0)
        {
            return;
        }
        switch (tipo)
        {
            case TipoAtributo.Fuerza:
                stats.Fuerza++;
                stats.A�adirBonusPorAtributoFuerza();
                break;
            case TipoAtributo.Percepcion:
                stats.Percepcion++;
                stats.A�adirBonusPorAtributoPercepcion();
                break;
            case TipoAtributo.Resistencia:
                stats.Resistencia++;
                stats.A�adirBonusPorAtributoResistencia();
                break;
            case TipoAtributo.Inteligencia:
                stats.Inteligencia++;
                stats.A�adirBonusPorAtributoInteligencia();
                break;
            case TipoAtributo.Suerte:
                stats.Suerte++;
                stats.A�adirBonusPorAtributoSuerte();
                break;
        }

        stats.PuntosDisponibles -= 1;

    }

    
    private void OnEnable()
    {
        AtributoButton.EventoAgregarAtributo += AtributoRespuesta;
    }

    private void OnDisable()
    {
        AtributoButton.EventoAgregarAtributo -= AtributoRespuesta; 
    }

}
