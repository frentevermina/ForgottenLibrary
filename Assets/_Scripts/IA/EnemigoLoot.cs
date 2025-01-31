using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoLoot : MonoBehaviour
{
    [Header("Exp")]
    [SerializeField] private float ExperienciGanada;

    [Header("Loot")]
    [SerializeField] private DropItem[] lootDisponible;

    private List<DropItem> lootSeleccionado = new List<DropItem>();
    public List<DropItem> LootSeleccionado => lootSeleccionado;
    public float ExpGanada => ExperienciGanada;
    

    private void Start()
    {
        SeleccionarLoot();
    }
    private void SeleccionarLoot()
    {
        foreach (DropItem item in lootDisponible)
        {
            float probabilidad = Random.Range(0, 100);
            if(probabilidad <= item.PorcentajeDrop)
            {
                lootSeleccionado.Add(item);
            }
        }
    }
}
