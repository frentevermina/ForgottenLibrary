using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TipoAtributo
{ 
    Fuerza,
    Resistencia,
    Percepcion,
    Inteligencia,
    Suerte,
}
public class AtributoButton : MonoBehaviour
{
    public static Action<TipoAtributo> EventoAgregarAtributo;
   [SerializeField]private TipoAtributo Tipo;

    public void AgregarAtributo()
    {
        EventoAgregarAtributo?.Invoke(Tipo);
    }


}
