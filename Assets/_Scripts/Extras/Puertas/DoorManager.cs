using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : Sigleton<DoorManager>
{
    [Header("Configuración")]
    [SerializeField] private List<GameObject> doors;

    [SerializeField] public bool needKey;
    [SerializeField] public bool playerHasKey;
    [SerializeField] public TextoAnimacionNivelUp sonidoPuerta;
    [SerializeField] public TextoAnimacionNivelUp sonidoPuertaCorredera;
    [SerializeField] public GameObject noSirveAqui;
    [SerializeField] public bool isOpen;
    [SerializeField] public bool inside;
    [SerializeField] public bool IsCorredera;


    public bool interact;
    public bool keyUsed;


    public List<GameObject> GetAllDoors()
    {
        return doors;
    }

    private void Start()
    {
        doors = new List<GameObject>();
        
        foreach (Transform door in transform)
        {
            doors.Add(door.gameObject);
        }

       /* for (int i = 0; i < doors.Count; i++)
        {
            
            if (doors[i].GetComponent<DoorType>().needKey == false)
            {
                doors[i].GetComponent<DoorType>().playerHasKey = true;
                
            }

        }*/
    }

    public void GetPlayerHasTheKey(string ID)
    {
        
        for (int i = 0; i < doors.Count; i++)
        {
            if (doors[i].GetComponent<DoorType>().needKey == true && ID == doors[i].GetComponent<DoorType>().ID)
            {
                doors[i].GetComponent<DoorType>().playerHasKey = true;
            }
                /* if(doors[i].GetComponent<DoorType>().needKey == true)
                 {
                     if (ID == doors[i].GetComponent<DoorType>().ID)
                     {
                         doors[i].GetComponent<DoorType>().playerHasKey = true;


                     }
                 }*/

        }

    }

    public void NoSirveAqui(string ID)
    {
        for (int i = 0; i < doors.Count; i++)
        {
            
            if(doors[i].GetComponent<DoorType>().needKey && ID == doors[i].GetComponent<DoorType>().ID)
            {
                doors[i].GetComponent<DoorType>().StartEnumerator();
            }
            

            
        }
    }

    public void GetPlayerUsedKey(string ID)
    {
        for (int i = 0; i < doors.Count; i++)
        {
            if (doors[i].GetComponent<DoorType>().needKey && ID == doors[i].GetComponent<DoorType>().ID)
            {
                doors[i].GetComponent<DoorType>().keyUsed = true;
                doors[i].GetComponent<DoorType>().StartSonidoPuerta();
            }
        }
    }

    public void ActivateCapsuleCollider2D()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            if (!doors[i].GetComponent<DoorType>()._capsuleCollider2D.isActiveAndEnabled)
            {
                doors[i].GetComponent<DoorType>()._capsuleCollider2D.enabled = true;
            }
        }
    }


    /* [Header("Configuración")]
     [Tooltip("El item donde obtendremos el id para equipar")]
     [SerializeField] private InventarioItem inventarioItemReferencia;
     [SerializeField] private TextoAnimacionNivelUp sonidoPuerta;
     [SerializeField] private Player player;
     public string IDPuerta;


     [SerializeField] private bool needKey;
     [SerializeField] private bool isOpen;
     [SerializeField] private bool inside;
     [SerializeField] private bool playerHasKey;
     private bool interact;
     private bool keyUsed;


     public bool NecesitaLlave => needKey;
     public bool Inside => inside;

     public GameObject noSirveAqui;
     private BoxCollider2D _boxCollider2D;
     public CapsuleCollider2D _capsuleCollider2D;
     private Animator _animator;


     private readonly int NeedKey = Animator.StringToHash("NeedKey");
     private readonly int IsOpen = Animator.StringToHash("IsOpen");
     private readonly int PlayerHasKey = Animator.StringToHash("PlayerHasKey");


     // Start is called before the first frame update
     void Start()
     {
         _animator = GetComponent<Animator>();
         _boxCollider2D = GetComponent<BoxCollider2D>();
         _capsuleCollider2D = GetComponent<CapsuleCollider2D>();

         keyUsed = false;

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
         if(!isOpen)
         {
             _animator.SetBool(IsOpen, false);
         }
     }

     void Update()
     {            
         if (Input.GetButtonDown("Interact") && inside)
         {
             StartCoroutine(IEInteraccionPuerta());
         }
         if (interact)
         {
             if (!needKey && playerHasKey && !isOpen || keyUsed)
             {
                 StartCoroutine(OpenDoor());
             }
             if (needKey && !playerHasKey && !isOpen )
             {
                 StartCoroutine(IEBloqueada());
             }
         }
     }

     private IEnumerator IEInteraccionPuerta()
     {
         interact = true;
         yield return new WaitForSeconds(0.3f);
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

     public void DesbloquearPuerta()
     {
         keyUsed = true;
         playerHasKey = true;
         //METER SONIDO DE DESBLOQUEO
         sonidoPuerta.SonidoDesbloqueo();
     }

     public IEnumerator OpenDoor()
     {
         _animator.SetBool(NeedKey, false);
         _animator.SetBool(PlayerHasKey, true);
         _animator.SetBool(IsOpen, true);
         isOpen = true;
         sonidoPuerta.SonidoAbrir();

         yield return new WaitForSeconds(1f);

         _boxCollider2D.enabled = false;

     }

     private IEnumerator IEBloqueada()
     {
         _animator.SetBool(NeedKey, true);
         _animator.SetBool(PlayerHasKey, false);
         sonidoPuerta.SonidoBloqueado();

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

     private void OnTriggerExit2D(Collider2D other)
     {
         if (other.CompareTag("Player"))
         {
             inside = false;


         }
     }*/

}
