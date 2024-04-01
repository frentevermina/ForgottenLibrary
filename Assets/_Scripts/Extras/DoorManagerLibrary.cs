using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManagerLibrary : Sigleton<DoorManagerLibrary>
{
    [Header("Configuración")]
    [Tooltip("El item donde obtendremos el id para equipar")]
    [SerializeField] private Item_Quests inventarioItemReferencia;

    [SerializeField] private bool needKey;
    [SerializeField] private bool isOpen;
    [SerializeField] private bool inside;
    [SerializeField] private bool playerHasKey;
    [SerializeField] private bool keyUsed;
    public string IDPuerta;
    public GameObject noSirveAqui;

    public bool NecesitaLlave => needKey;
    public bool Inside => inside;


    private BoxCollider2D _boxCollider2D;
    private Animator _animator;
    private bool interact;

    private readonly int NeedKey = Animator.StringToHash("NeedKey");
    private readonly int IsOpen = Animator.StringToHash("IsOpen");
    private readonly int PlayerHasKey = Animator.StringToHash("PlayerHasKey");


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

        keyUsed = false;

        IDPuerta = inventarioItemReferencia.ID;

        if (!needKey)
        {
            playerHasKey = true;

        }
        else
        {
            playerHasKey = false;
        }

        if (isOpen && !needKey)
        {
            _animator.SetBool(NeedKey, false);
            _animator.SetBool(IsOpen, true);
        }
        if (!isOpen)
        {
            _animator.SetBool(IsOpen, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            interact = true;
        }
        if (Input.GetButtonUp("Interact"))
        {
            interact = false;
        }
    }


    private IEnumerator IENoSirveAqui()
    {
        noSirveAqui.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        noSirveAqui.SetActive(false);
    }

    public void StartEnumerator()
    {
        StartCoroutine(IENoSirveAqui());
    }

    public void DesbloquearPuerta()
    {
        keyUsed = true;
        playerHasKey = true;
        //METER SONIDO DE DESBLOQUEO
    }

    public void AbrirPuerta()
    {

        _animator.SetBool(NeedKey, false);
        _animator.SetBool(PlayerHasKey, true);
        _animator.SetBool(IsOpen, true);
        isOpen = true;
        //METER SONIDO DE ABRIR PUERTA
    }

    private IEnumerator IEBloqueada()
    {
        _animator.SetBool(NeedKey, true);
        _animator.SetBool(PlayerHasKey, false);

        yield return new WaitForSeconds(1f);

        _animator.SetBool(NeedKey, false);

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inside = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && interact)
        {
            if (needKey && !playerHasKey && !isOpen)
            {

                StartCoroutine(IEBloqueada());
                //SONIDO DE PUERTA BLOQUEADA
            }



            if (!needKey && playerHasKey && !isOpen ||
                 needKey && playerHasKey && !isOpen && keyUsed)
            {

                AbrirPuerta();
                _boxCollider2D.enabled = false;

            }
            /* if (!needKey && !isOpen)
             {

                 AbrirPuerta();
             }*/
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inside = false;


        }
    }
}
