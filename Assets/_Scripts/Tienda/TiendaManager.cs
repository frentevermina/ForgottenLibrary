using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiendaManager : MonoBehaviour
{
    [Header("Configuraci�n")]
    [SerializeField] private ItemTienda itemTiendaPrefab;
    [SerializeField] private Transform panelContenedor;

    [Header("Items")]
    [SerializeField] private ItemVenta[] itemsDisponibles;

    private void Start()
    {
        CargarItemsEnventa();
    }

    private void CargarItemsEnventa()
    {
        for (int i = 0; i < itemsDisponibles.Length; i++)
        {
            ItemTienda itemTienda = Instantiate(itemTiendaPrefab, panelContenedor);
            itemTienda.ConfigurarItemEnVenta(itemsDisponibles[i]);
        }
    }

}
