using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoArma
{
    Invocacion,
    Distancia,
    Melee
}

[CreateAssetMenu(menuName = "Personaje/Arma")]
public class Arma : ScriptableObject
{
    [Header("Config")]
    public Sprite IconoArma;
    public Sprite IconoSkill;
    public TipoArma Tipo;
    public float Daño;

    [Header("Arma Mágica")]
    public float ManaRequerido;
    public Proyectil ProyectilPrefab;

    [Header("Bonificadores")]
    public float PorcentajeCritico;
    public float PorcentajeBloqueo;
    public float Esoterismo;

}
