using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaperInteraction : MonoBehaviour
{
    [Header("Info Avisador")]
    [SerializeField] [Tooltip("GameObject de Imagen de lupa")] private GameObject paperInteractionButton;
    [SerializeField] [Tooltip("GameObject del Item para desactivar")] private GameObject paperItem;
    [SerializeField] [Tooltip("ScriptablObject de la info a mostrar")] private PaperMessage paperMessage;

    [Header("Info Investigación")]
    [SerializeField] [Tooltip("GameObject de PanelPaper")] private GameObject panelPaper;
    [SerializeField] [Tooltip("Hijo de PanelPaper PaperMessage-TMP")] private TextMeshProUGUI paperMessageTMP;

    [Header("Sonido")]
    [SerializeField] private TextoAnimacionNivelUp sonidoPapel;

    private bool isInside;
    private bool infoShowed;
    
    public InvestigationConversation InvestigationConversation { get; set; }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && isInside && !infoShowed)
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            ConfigPanelInfo();
            levelManager.PauseNormal();
            return;
        }

        if (Input.GetButtonDown("Interact") && infoShowed && isInside)
        {


            DeactivateItemPanel();
            
            
        }
    }

    public void DeactivateItemPanel()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        DoorManager doorManager = FindObjectOfType<DoorManager>();
        OpenCloseInvestigationPanel(false);
        levelManager.UnPauseNormal();
        doorManager.ActivateCapsuleCollider2D();
        gameObject.SetActive(false);

        paperItem.SetActive(false);
    }

    private void ConfigPanelInfo()
    {
        OpenCloseInvestigationPanel(true);
        sonidoPapel.SonidoPapelCogido();
        infoShowed = true;
        paperMessageTMP.text = paperMessage.informacion;
        // PauseGame();
    }

    private void OpenCloseInvestigationPanel(bool state)
    {
        panelPaper.SetActive(state);

    }       
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = true;
            paperInteractionButton.SetActive(true);
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
            paperInteractionButton.SetActive(false);
        }

    }
}
