using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats")] 
public class PersonajeStats : ScriptableObject
{
   
    [Header("Atributos")]
    public int Fuerza;
    public int Resistencia;
    public int Percepcion;
    public int Inteligencia;
    public int Suerte;

    [Header("Habilidades")]
    //Es seguro poner public todo
    public float Da�o = 1f;
    public float Defensa = 1f;
    public float Esoterismo = 0f;
    public float Buscar = 0f;
    public float Ocultismo = 0f;
    public float Medicina = 0f;
    public float Nivel;
    public float ExpActual;
    public float ExpRequeridaSiguienteNivel;
    /*[Range(0f, 100f)]*/
    public float PorcentajeCritico;
    /*[Range(0f, 100f)]*/
    public float PorcentajeEsquiva;

    [HideInInspector]public int PuntosDisponibles;

    //---------------->  FALTA ATRIBUTO AGILIDAD
    public void A�adirBonusPorAtributoFuerza()
    {
        Da�o += 1f;
        
        

    }

    public void A�adirBonusPorAtributoResistencia()
    {
        Defensa += 1f;
        PorcentajeEsquiva += 0.5f;
    }
    public void A�adirBonusPorAtributoInteligencia()
    {
        Esoterismo += 0.5f;
        //PorcentajeEsquiva += 0.2f;
      //  Defensa += 1f;
       // Da�o += 0.2f;
        //PorcentajeCritico += 2f;
    }

    public void A�adirBonusPorAtributoPercepcion()
    {
        Buscar += 1f;
    }

    public void A�adirBonusPorAtributoSuerte()
    {
        PorcentajeCritico += 0.5f;
        PorcentajeEsquiva += 0.5f;
      //  Esoterismo += 1f;
        Buscar += 0.5f;
    }

    public void A�adirBonusPorArma(Arma arma)
    {
        Da�o += arma.Da�o;
        PorcentajeCritico += arma.PorcentajeCritico;
        PorcentajeEsquiva += arma.PorcentajeBloqueo;
        Esoterismo += arma.Esoterismo;

    }
    public void A�adirBonusPorLibro(float cantidadEsoterismo, float cantidadOcultismo, float cantidadMedicina, float cantidadBuscar)
    {
        Esoterismo += cantidadEsoterismo;
        Ocultismo += cantidadOcultismo;
        Medicina += cantidadMedicina;
        Buscar += cantidadBuscar;
    }


    public void QuitarBonusPorArma(Arma arma)
    {
        Da�o -= arma.Da�o;
        PorcentajeCritico -= arma.PorcentajeCritico;
        PorcentajeEsquiva -= arma.PorcentajeBloqueo;
        Esoterismo -= arma.Esoterismo;
    }

    public void ResetearValores()
    {
        Da�o = 1f;
        Defensa = 1f;
        Esoterismo = 0f;
        Ocultismo = 0f;
        Medicina = 0f;
        Buscar = 0f;
        Nivel = 1;
        ExpActual = 0f;
        ExpRequeridaSiguienteNivel = 0f;
        PorcentajeEsquiva = 0f;
        PorcentajeCritico = 0f;

        Fuerza = 0;
        Resistencia = 0;
        Percepcion = 0;
        Inteligencia = 0;
        Suerte = 0;
        PuntosDisponibles = 0;

    }

}
