using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemTienda : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private Image itemIcono; 
    [SerializeField] private TextMeshProUGUI itemNombre; 
    [SerializeField] private TextMeshProUGUI itemPrecio; 
    [SerializeField] private TextMeshProUGUI cantidadPorComprar;

    public ItemVenta ItemCargado { get; private set; }

    private int cantidad;
    private int precioInicial;
    private int precioActual;

    private void Update()
    {
        cantidadPorComprar.text = cantidad.ToString();
        itemPrecio.text = precioActual.ToString();
    }

    public void ConfigurarItemEnVenta(ItemVenta itemVenta)
    {
        ItemCargado = itemVenta;
        itemIcono.sprite = itemVenta.Item.Icono;
        itemNombre.text = itemVenta.Item.Nombre;
        itemPrecio.text = itemVenta.Precio.ToString();
        cantidad = 1;
        precioInicial = itemVenta.Precio;
        precioActual = itemVenta.Precio;
    }

    public void ComprarItem()
    {
        if(MonedasFavorManager.Instance.MonedasTotales >= precioActual)
        {
            Inventario.Instance.AñadirItem(ItemCargado.Item, cantidad);
            MonedasFavorManager.Instance.RemoverMonedas(precioActual);
            cantidad = 1;
            precioActual = precioInicial;
        }
    }

    public void SumarItemPorComprar()
    {
        int precioDeCompra = precioInicial * (cantidad + 1);
        if(MonedasFavorManager.Instance.MonedasTotales >= precioDeCompra)
        {
            cantidad++;
            precioActual = precioInicial * cantidad;
        }
    }
    public void RestarItemPorComprar()
    {
        if(cantidad == 1)
        {
            return;
        }

        cantidad--;
        precioActual = precioInicial * cantidad;
    }



}
