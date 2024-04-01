using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPorAgregar : MonoBehaviour
{

    [Header("Configuración")]
    [SerializeField] private InventarioItem inventarioItemReferencia;
    [SerializeField] private int cantidadPorAgregar;
    [SerializeField] private Animator playerAnimation;
    [SerializeField] private TextoAnimacionNivelUp sonidoGetItem;

    [Header("Propiedades")]
    [SerializeField] private bool isOnTheGround;

    //  private bool pickingUp;
    private bool isInside;

    private const string PICKUP = "PickingUp";
    private const string GETITEM = "GetItem";

    public string ID;
    public DoorManager _doorManager;
    public Item_Quests llaves;
    //***********

    private void Start()
    {
        ID = inventarioItemReferencia.ID;
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && isInside && isOnTheGround)
        {
            StartCoroutine(IEPickUp());
        }
        if (Input.GetButtonDown("Interact") && isInside && !isOnTheGround)
        {
            StartCoroutine(IEGetItemStand());
        }

    }

    private void GetItem()
    {
        if (inventarioItemReferencia.Tipo == TiposDeItem.Llaves)
        {
            _doorManager.GetPlayerHasTheKey(ID);
            Inventario.Instance.AñadirItem(inventarioItemReferencia, cantidadPorAgregar);

        }
        if (inventarioItemReferencia.Tipo != TiposDeItem.Llaves)
        {
            Inventario.Instance.AñadirItem(inventarioItemReferencia, cantidadPorAgregar);

        }

        Destroy(gameObject);

        //******

    }

    private IEnumerator IEPickUp()
    {
        //  pickingUp = true;
        playerAnimation.SetBool(GETITEM, false);
        playerAnimation.SetBool(PICKUP, true);
        isInside = false;
        sonidoGetItem.GetItemSound();


        yield return new WaitForSeconds(.3f);

        //   pickingUp = false;
        playerAnimation.SetBool(PICKUP, false);
        GetItem();


    }

    private IEnumerator IEGetItemStand()
    {
        playerAnimation.SetBool(GETITEM, true);
        playerAnimation.SetBool(PICKUP, false);
        isInside = false;
        sonidoGetItem.GetItemSound();


        yield return new WaitForSeconds(.3f);

        //   pickingUp = false;
        playerAnimation.SetBool(GETITEM, false);
        GetItem();
    }


    //***********
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false;
        }
    }


}
