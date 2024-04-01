using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Bebida Reconfortante")]


public class Item_BebidaReconfortante : InventarioItem
{
    [Header("Bebida Info")]
    public float TemperaturaRestauracion;

    public override bool UsarItem()
    {
        if (Inventario.Instance.Personaje.PersonajeTemperatura.SePuedeRestaurar)
        {
            Inventario.Instance.Personaje.PersonajeTemperatura.RestaurarTemperatura(TemperaturaRestauracion);
            return true;
        }

        return false;

    }
}
