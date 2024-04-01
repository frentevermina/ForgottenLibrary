using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Personaje/Libro")]
public class Libro : ScriptableObject
{
    [Header("Bonificadores")]
    public float Esoterismo;
    public float Ocultismo;
    public float Medicina;
    public float Buscar;

}
