using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoDeteccion
{
    Rango,
    Melee
}

public class EnemigoInteraccion : MonoBehaviour
{
    [SerializeField] private GameObject SeleccionRangoFX;
    [SerializeField] private GameObject seleccionMeleeFX;

    public void MostarEnemigoSeleccionado(bool estado, TipoDeteccion tipo)
    {
        if (tipo == TipoDeteccion.Rango)
        {
            SeleccionRangoFX.SetActive(estado);

        }
        else
        {
            seleccionMeleeFX.SetActive(estado);
        }
    }

    public void DesactivarSpriteSeleccion()
    {
        seleccionMeleeFX.SetActive(false);
        SeleccionRangoFX.SetActive(false);
    }

}
