using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorType : DoorManager
{

    [Header("Configuración")]
    [SerializeField]
    [Tooltip("La llave donde obtendremos el id para equipar")]
    private Item_Quests inventarioLlaveReferencia;
    



    private BoxCollider2D _boxCollider2D;
    public CapsuleCollider2D _capsuleCollider2D;
    private Animator _animator;
    




    private readonly int NeedKey = Animator.StringToHash("NeedKey");
    private readonly int IsOpen = Animator.StringToHash("IsOpen");
    private readonly int PlayerHasKey = Animator.StringToHash("PlayerHasKey");


    public string ID;



    private void Start()
    {
        if(inventarioLlaveReferencia != null)
        {
            ID = inventarioLlaveReferencia.ID;

        }
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();

    }

    void Update()
    {
        if (Input.GetButtonDown("Interact") && inside)
        {
            StartCoroutine(IEInteraccionPuerta());
        
            if (interact)
            {
                if (playerHasKey)
                {
                    
                    StartCoroutine(IEDesbloquearPuerta());
                    needKey = false;
                    keyUsed = false;
                    playerHasKey = false;
                    
                    return;
                }
                if (!needKey && !isOpen || needKey && keyUsed && !isOpen)
                {
                    StartCoroutine(OpenDoor());
                   
                    return;
                }
            
                if (needKey && !keyUsed && !isOpen)
                {
                    StartCoroutine(IEBloqueada());
                    return;
                }
                
                if (isOpen && !needKey || needKey && keyUsed && isOpen)
                {
                    StartCoroutine(CloseDoor());
                    return;
                }
            }
        }
    }

    private IEnumerator IEInteraccionPuerta()
    {
        interact = true;
        yield return new WaitForSeconds(0.1f);
        interact = false;
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

    public void StartSonidoPuerta()
    {
        StartCoroutine(IEDesbloquearPuerta());
    }

    private IEnumerator IEDesbloquearPuerta()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        sonidoPuerta.SonidoDesbloqueo();
        levelManager.ShowTextWithoutPause();
        yield return new WaitForSeconds(2f);
        levelManager.EndShowTextWithoutPause();


    }

    private IEnumerator OpenDoor()
    {
        
        TipoSonido();
        _animator.SetBool(NeedKey, false);
        _animator.SetBool(PlayerHasKey, true);
        _animator.SetBool(IsOpen, true);
        isOpen = true;
       
        yield return new WaitForSeconds(1f);

        _boxCollider2D.enabled = !_boxCollider2D.enabled;
    }

    private IEnumerator CloseDoor()
    {
        TipoSonido();
        _animator.SetBool(NeedKey, false);
        _animator.SetBool(PlayerHasKey, true);
        _animator.SetBool(IsOpen, false);
        isOpen = false;

        yield return new WaitForSeconds(1f);

        _boxCollider2D.enabled = !_boxCollider2D.enabled;
    }

    private IEnumerator IEBloqueada()
    {
        _animator.SetBool(NeedKey, true);
        _animator.SetBool(PlayerHasKey, false);
        sonidoPuerta.SonidoBloqueado();

        yield return new WaitForSeconds(1f);

        _animator.SetBool(NeedKey, false);

    }

    private void TipoSonido()
    {
        if (!IsCorredera)
        {
            sonidoPuerta.SonidoAbrir();
        }
        if (IsCorredera)
        {
            sonidoPuertaCorredera.SonidoAbrirCorredera();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inside = false;
        }
    }
}
