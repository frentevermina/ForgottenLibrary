using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirObjeto : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] public TextoAnimacionNivelUp sonidoApertura;

    //Variables privadas para controlar la interacción con el jugador
    private bool isInside;
    private bool interact;
    private bool isOpen;

    //Obtenemos los componentes con los que trabajar
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;

    //Asignar el Animator a la variable ISOPEN
    private readonly int ISOPEN = Animator.StringToHash("IsOpen");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
     //   sonidoApertura = GameObject.Find("CanvasEvents").GetComponent<TextoAnimacionNivelUp>();
    }
    private void Update()
    {
        if(Input.GetButtonDown("Interact") && isInside)
        {
            StartCoroutine(IEInteraccion());
        }
        
        if (interact && !isOpen)
        {
            sonidoApertura.SonidoMobiliario();
            StartCoroutine(IEAbrirMobiliario());
        }
        if(interact && isOpen)
        {
            sonidoApertura.SonidoMobiliario();
            StartCoroutine(IECerrarMobiliario());
        }
    }

    private IEnumerator IEInteraccion()
    {
        interact = true;
        yield return new WaitForSeconds(0.3f);
        interact = false;
    }

    private IEnumerator IEAbrirMobiliario()
    {
        _animator.SetBool(ISOPEN, true);
        yield return new WaitForSeconds(0.1f);
        isOpen = true;
    }

    private IEnumerator IECerrarMobiliario()
    {
        _animator.SetBool(ISOPEN, false);
        yield return new WaitForSeconds(0.1f);
        isOpen = false;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInside = false;
        }
    }

}
