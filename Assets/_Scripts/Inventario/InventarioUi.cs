using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventarioUi : Sigleton<InventarioUi>
{
    [Header("Panel Inventario Descripción")]
    [SerializeField] private GameObject panelInventarioDescripcion;
    [SerializeField] private GameObject panelInventario;
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemDescripcion;

    [SerializeField] private InventarioSlot slotPrefab; //referencia del prefab
    [SerializeField] private Transform contenedor;

    [SerializeField] private GameObject panelArmaSkill;

    public GameObject PanelInventarioDescripcion { get; set; }
    public int IndexSlotInicialPorMover { get; private set; }

    public InventarioSlot SlotSeleccionado { get; private set; }

    List<InventarioSlot> slotsDisponibles = new List<InventarioSlot>();




    void Start()
    {


        InicializarInventario();
        IndexSlotInicialPorMover = -1;

    }

    private void Update()
    {
        ActualizarSlotSeleccionado();
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (SlotSeleccionado != null)
            {
                IndexSlotInicialPorMover = SlotSeleccionado.Index;
            }
        }


    }

    private void InicializarInventario() //para instanciar todos los slots en el contenedor
    {
        for (int i = 0; i < Inventario.Instance.NumeroDeSlots; i++)
        {
            InventarioSlot nuevoSlot = Instantiate(slotPrefab, contenedor);
            nuevoSlot.Index = i;
            slotsDisponibles.Add(nuevoSlot);
        }
    }

    private void ActualizarSlotSeleccionado()
    {
        GameObject goSeleccionado = EventSystem.current.currentSelectedGameObject;
        if (goSeleccionado == null)
        {
            return;
        }

        InventarioSlot slot = goSeleccionado.GetComponent<InventarioSlot>();

        if (slot != null)
        {

            SlotSeleccionado = slot;


            //slotArma = slotArma.ChangeWeapon();
        }



    }


    public void DibujarItemEnInventario(InventarioItem itemPorAñadir, int cantidad, int itemIndex)
    {
        InventarioSlot slot = slotsDisponibles[itemIndex];
        if (itemPorAñadir != null)
        {
            slot.ActivarSlotUI(true);
            slot.ActualizarSlot(itemPorAñadir, cantidad);

        }
        else
        {
            slot.ActivarSlotUI(false);
        }

    }


    private void ActualizarInventarioDescripcion(int index)
    {
        if (Inventario.Instance.ItemsInventario[index] != null)
        {
            itemIcono.sprite = Inventario.Instance.ItemsInventario[index].Icono;
            itemNombre.text = Inventario.Instance.ItemsInventario[index].Nombre;
            itemDescripcion.text = Inventario.Instance.ItemsInventario[index].Descripcion;
            panelInventarioDescripcion.SetActive(true);
        }

        else
        {

            panelInventarioDescripcion.SetActive(false);
        }
    }

    public void UsarItem()
    {

        if (SlotSeleccionado != null)
        {
            UIManager uIManager = FindObjectOfType<UIManager>();


            SlotSeleccionado.SlotUsarItem();
            SlotSeleccionado.SeleccionarSlot();
            // panelInventarioDescripcion.SetActive(false);//
            // panelInventario.SetActive(false);//
            uIManager.CerrarInventario();
        }
    }

    public void EquiparItem()
    {
        if (SlotSeleccionado != null)
        {
            UIManager uIManager = FindObjectOfType<UIManager>();
            ContenedorArma contenedorArma = FindObjectOfType<ContenedorArma>();

            SlotSeleccionado.SlotRemoverItem();
            SlotSeleccionado.SlotEquiparItem();
            SlotSeleccionado.SeleccionarSlot();
            //  panelInventarioDescripcion.SetActive(false);//
            //  panelInventario.SetActive(false);//

            uIManager.CerrarInventario();
        }
    }

    public void RemoverItem()
    {
        UIManager uIManager = FindObjectOfType<UIManager>();
        SlotSeleccionado.SlotRemoverItem();
        SlotSeleccionado.SeleccionarSlot();
        uIManager.CerrarInventario();
    }

    #region Evento
    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int index)
    {
        if (tipo == TipoDeInteraccion.Click)
        {
            ActualizarInventarioDescripcion(index);
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
