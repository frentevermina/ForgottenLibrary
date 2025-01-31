using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventario : Sigleton<Inventario>
{
    [Header("Items")]
    [SerializeField] private InventarioItem[] itemsInventario;
    [SerializeField] private Player personaje;
    //
    [SerializeField] private PersonajeStats personajeStats;
    //
    [SerializeField] private int numeroDeSlots;

    //
    public PersonajeStats PersonajeStats => personajeStats;
    //
    public Player Personaje => personaje;
    public int NumeroDeSlots => numeroDeSlots;
    public InventarioItem[] ItemsInventario => itemsInventario;
    
    private void Start()
    {
        itemsInventario = new InventarioItem[numeroDeSlots];
    }

    public void AņadirItem(InventarioItem itemPorAņadir, int cantidad)
    {
        if (itemPorAņadir == null)
        {
            return;
        }

        // Verificacion en caso tener ya un item similar en inventario
        List<int> indexes = VerificarExistencias(itemPorAņadir.ID);
        if (itemPorAņadir.EsAcumulable)
        {
            if (indexes.Count > 0)
            {
                for (int i = 0; i < indexes.Count; i++)
                {
                    if (itemsInventario[indexes[i]].Cantidad < itemPorAņadir.AcumulacionMax)
                    {
                        itemsInventario[indexes[i]].Cantidad += cantidad;
                        if (itemsInventario[indexes[i]].Cantidad > itemPorAņadir.AcumulacionMax)
                        {
                            int diferencia = itemsInventario[indexes[i]].Cantidad - itemPorAņadir.AcumulacionMax;
                            itemsInventario[indexes[i]].Cantidad = itemPorAņadir.AcumulacionMax;
                            AņadirItem(itemPorAņadir, diferencia);
                        }

                        InventarioUi.Instance.DibujarItemEnInventario(itemPorAņadir,
                            itemsInventario[indexes[i]].Cantidad, indexes[i]);
                        return;
                    }
                }
            }
        }

        if (cantidad <= 0)
        {
            return;
        }

        if (cantidad > itemPorAņadir.AcumulacionMax)
        {
            AņadirItemEnSlotDisponible(itemPorAņadir, itemPorAņadir.AcumulacionMax);
            cantidad -= itemPorAņadir.AcumulacionMax;
            AņadirItem(itemPorAņadir, cantidad);
        }
        else
        {
            AņadirItemEnSlotDisponible(itemPorAņadir, cantidad);
        }
    }

    private List<int> VerificarExistencias(string itemID)
    {
        List<int> indexesDelItem = new List<int>();
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] != null)
            {
                if (itemsInventario[i].ID == itemID)
                {
                    indexesDelItem.Add(i);
                }
            }
        }

        return indexesDelItem;
    }

    private void AņadirItemEnSlotDisponible(InventarioItem item, int cantidad)
    {
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] == null)
            {
                itemsInventario[i] = item.CopiarItem();
                itemsInventario[i].Cantidad = cantidad;
                InventarioUi.Instance.DibujarItemEnInventario(item, cantidad, i);
                return;
            }
        }
    }

    private void EliminarItem(int index)
    {
        if (ItemsInventario[index].EsConsumible)
        {        
            ItemsInventario[index].Cantidad--;
            if (itemsInventario[index].Cantidad <= 0)
            {
                itemsInventario[index].Cantidad = 0;
                itemsInventario[index] = null;
                InventarioUi.Instance.DibujarItemEnInventario(null, 0, index);
            }
        
        else
            {
                InventarioUi.Instance.DibujarItemEnInventario(itemsInventario[index],
                itemsInventario[index].Cantidad, index);
            }
        }
    }

    public void MoverItem(int indexInicial, int indexFinal)
    {
        if (itemsInventario[indexInicial] == null || itemsInventario[indexFinal] != null)
        {
            return;
        }

        // Copiar item en slot final
        InventarioItem itemPorMover = itemsInventario[indexInicial].CopiarItem();
        itemsInventario[indexFinal] = itemPorMover;
        InventarioUi.Instance.DibujarItemEnInventario(itemPorMover, itemPorMover.Cantidad, indexFinal);

        // Borramos Item de Slot inicial
        itemsInventario[indexInicial] = null;
        InventarioUi.Instance.DibujarItemEnInventario(null, 0, indexInicial);
    }

    private void UsarItem(int index)
    {
        if (itemsInventario[index] == null)
        {
            return;
        }

        if (itemsInventario[index].UsarItem())
        {
            EliminarItem(index);
        }
    }

    private void EquiparItem(int index)
    {
        if(itemsInventario[index] == null)
        {
            return;
        }

        if (itemsInventario[index].Tipo != TiposDeItem.Armas)
        {
            return;
        }

        itemsInventario[index].EquiparItem();
    }

    private void RemoverItem(int index)
    {
        if (itemsInventario[index] == null)
        {
            return;
        }

        if (itemsInventario[index].Tipo != TiposDeItem.Armas)
        {
            return;
        }

        itemsInventario[index].RemoverItem();

    }

    #region Eventos

    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int index)
    {
        switch (tipo)
        {
            case TipoDeInteraccion.Usar:
                UsarItem(index);
                break;
            case TipoDeInteraccion.Equipar:
                EquiparItem(index);
                break;
            case TipoDeInteraccion.Eliminar:
                RemoverItem(index);
                break;
        }
    }

    private void OnEnable()
    {
        InventarioSlot.EventoSlotInteraccion += SlotInteraccionRespuesta;
    }

    private void OnDisable()
    {
        InventarioSlot.EventoSlotInteraccion -= SlotInteraccionRespuesta;
    }

    #endregion
}
