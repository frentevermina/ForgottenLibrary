using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum TipoDeHabsRequeridos
{
    Inteligencia,
    Buscar,
    Esoterismo
}

public class InvestigationInteraction : MonoBehaviour
{

    [Header("Propiedades")]
    [SerializeField] private bool interactable;
    [SerializeField] private bool tieneItem;
    [SerializeField] private int cantidadPorAgregar;



    [Header("Info Avisador")]
    [SerializeField] [Tooltip("GameObject de Imagen de lupa")] private GameObject investigationInteractionButton;
    [SerializeField] [Tooltip("ScriptablObject de la info a mostrar")] private InvestigationConversation investigationConversation;

    [Header("Info Investigación")]
    [SerializeField] [Tooltip("GameObject de PanelInfo")] private GameObject panelInvestigar;
    [SerializeField] [Tooltip("Hijo de PanelInfo Info-TMP")] private TextMeshProUGUI investigacionConversacionTMP;

    /* [Header("Stats Necesarios")]
     [SerializeField] [Tooltip("ScriptableObject de stats")] private PersonajeStats stats;
     [SerializeField] [Tooltip("GameObject del mensaje si tienes bajos stats")] private GameObject MensajeStats;
     [SerializeField] [Tooltip("El mensaje si tienes bajos stats")] private  TextMeshProUGUI statsNecesariosTMP;*/

    [Header("Configuración")]
    [SerializeField] private InventarioItem inventarioItemReferencia;
    [SerializeField] private TextoAnimacionNivelUp _itemGanado;
    [SerializeField] private GameObject objetoOculto;
    [SerializeField] private GameObject panelPregunta;
    /* [SerializeField] [Tooltip("Si requiere algún stat o habilidad en determinado nivel")] private bool needStats;
     public TipoDeHabsRequeridos TipoDeHabilidadNecesaria;
     [SerializeField] private float puntuacionStatNecesaria;*/

    private bool isInside;
    private bool infoShowed;
    private bool started;
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;
    // private bool addInfoShowed;
    // private bool needStatsShowed;
    public InvestigationConversation InvestigationConversation { get; set; }

    private readonly int MOVED = Animator.StringToHash("Moved");

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Interact") && isInside && !infoShowed && !started)
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            ConfigPanelInfo();
            levelManager.ShowTextWithoutPause();
            return;
        }

        if(Input.GetButtonDown("Interact") && infoShowed && isInside && interactable && !started)
        {
            OpenClosePreguntaPanel(true);
            OpenCloseInvestigationPanel(false);
            /*LevelManager levelManager = FindObjectOfType<LevelManager>();
            OpenCloseInvestigationPanel(false);
            StartCoroutine(AnimationStone());
            objetoOculto.SetActive(true);
            _boxCollider2D.enabled = false;*/
        }

        if (Input.GetButtonDown("Interact") && infoShowed && isInside && !interactable && !started)
        {

            LevelManager levelManager = FindObjectOfType<LevelManager>();

            

            if (tieneItem)
            {

                OpenCloseInvestigationPanel(false);
                levelManager.EndShowTextWithoutPause();
                GetItem();
                ActivarItemGanado();
                gameObject.SetActive(false);
                return;
            }
            else
            {

                OpenCloseInvestigationPanel(false);
                infoShowed = false;
                levelManager.EndShowTextWithoutPause();
                return;
            }
        }
    }


    private void ConfigPanelInfo()
    {
        OpenCloseInvestigationPanel(true);
        infoShowed = true;
        investigacionConversacionTMP.text = investigationConversation.informacion;
        ShowTextWithAnimation(investigationConversation.informacion);
        // PauseGame();
    }

    private void OpenCloseInvestigationPanel(bool state)
    {
        panelInvestigar.SetActive(state);

    }

    public void OpenClosePreguntaPanel(bool state)
    {
        if (interactable)
        {
            infoShowed = false;
        }
        panelPregunta.SetActive(state);
        
    }

    public void StartCoroutineStone()
    {
        StartCoroutine(AnimationStone());
    }
    
    private IEnumerator AnimationStone()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        OpenClosePreguntaPanel(false);
        _animator.SetBool(MOVED, true);
        objetoOculto.SetActive(true);
        _boxCollider2D.enabled = false;
        yield return new WaitForSeconds(1);
        
        levelManager.EndShowTextWithoutPause();

    }

    private void GetItem()
    {
        Inventario.Instance.AñadirItem(inventarioItemReferencia, cantidadPorAgregar);
    }

    private void ActivarItemGanado()
    {
        _itemGanado.RespuestaItemGanado();
    }

    private IEnumerator AnimateText(string sentence)
    {
        started = true;
        investigacionConversacionTMP.text = "";
        char[] letters = sentence.ToCharArray();
        for (int i = 0; i < letters.Length; i++)
        {
            investigacionConversacionTMP.text += letters[i];
            yield return new WaitForSeconds(0.03f);
        }
        started = false;

    }
    private void ShowTextWithAnimation(string sentence)
    {
        StartCoroutine(AnimateText(sentence));
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = true;
            investigationInteractionButton.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false;
            investigationInteractionButton.SetActive(false);
        }

    }
}
