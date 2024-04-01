using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponType : ArmaManager
{
    [Header("Configuración")]
    [SerializeField] [Tooltip("El item donde obtendremos el id para equipar")]
    private Item_Arma inventarioItemReferencia;
    
    public string ID;
    private bool equiped;

    public bool Equiped { get; set; }

    private void Awake()
    {
        equiped = Equiped;
        ID = inventarioItemReferencia.ID;
    }

}
