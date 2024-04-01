using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum TipoDeInteraccion
{
    Click,
    Usar,
    Equipar,
    Eliminar
}

public class InventarioSlot : MonoBehaviour
{
    public static Action<TipoDeInteraccion, int> EventoSlotInteraccion;

    [SerializeField] private Image itemIcono;
    [SerializeField] private GameObject fondoCantidad;
    [SerializeField] private TextMeshProUGUI cantidadTMP;
    public int Index { get; set; }

    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    public void ActualizarSlot(InventarioItem item, int cantidad)
    {
        itemIcono.sprite = item.Icono;
        cantidadTMP.text = cantidad.ToString();
    }

    public void ActivarSlotUI(bool estado) 
    {
        itemIcono.gameObject.SetActive(estado);
        fondoCantidad.SetActive(estado);
    }

    public void SeleccionarSlot()
    {
        _button.Select();
    }



    public void Clickslot()
    {
        EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Click, Index);
        //mover item
        if (InventarioUi.Instance.IndexSlotInicialPorMover != -1)
        {
            if (InventarioUi.Instance.IndexSlotInicialPorMover != Index)
            {
                //Mover
                Inventario.Instance.MoverItem(InventarioUi.Instance.IndexSlotInicialPorMover, Index);
            }
        }
    }
    
    public void SlotUsarItem()
    {
        if (Inventario.Instance.ItemsInventario[Index] != null)
        {
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Usar, Index);
        }
    }

    public void SlotEquiparItem()
    {
        if (Inventario.Instance.ItemsInventario[Index] != null)
        {
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Equipar, Index);
        }
    }

    public void SlotRemoverItem()
    {
        if (Inventario.Instance.ItemsInventario[Index] != null)
        {
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Eliminar, Index);
        }
    }

}
