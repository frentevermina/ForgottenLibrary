using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogManager : Sigleton<DialogManager>
{
    [Header("Diálogos")]
    [SerializeField] [Tooltip("Panel de Dialogo")] private GameObject panelDialogo;
    [SerializeField] [Tooltip("icono del NPC")] private Image npcIcono;
    [SerializeField] [Tooltip("Nombre del NPC")] private TextMeshProUGUI npcNombreTMP;
    [SerializeField] [Tooltip("Conversación del NPC")] private TextMeshProUGUI npcConversacionTMP;

    
    [SerializeField] private TextMeshProUGUI playerConversacionTMP;
    

    private bool dialogAnimated;
    private bool goodByeShowed;

    /// <summary>
    /// Have answer? if yes...
    /// Load Asnwer 
    /// </summary>


    public NpcInteraction NPCDisponible { get; set; }
   // public InvestigationInteraction InvestigationDisponible { get; set; }

    private Queue<string> dialogSequence;
   
    private void Start()
    {
        dialogSequence = new Queue<string>();
        
    }

    private void Update()
    {
        if(NPCDisponible == null )
        {
            return;
        }

        if (Input.GetButtonDown("Interact"))
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();

            levelManager.ShowTextWithoutPause();
            ConfigPanel(NPCDisponible.Dialog);
        }

      

        if (Input.GetKeyDown(KeyCode.C))
        {
           
            if (goodByeShowed)
            {
                LevelManager levelManager = FindObjectOfType<LevelManager>();
                levelManager.EndShowTextWithoutPause();
                OpenCloseDialogPanel(false);
                goodByeShowed = false;
                return;
            }

            if (NPCDisponible.Dialog.HaveExtraInteraction)
            {
                UIManager.Instance.OpenPanelInteraction(NPCDisponible.Dialog.InteractionExtra);
                
                LevelManager levelManager = FindObjectOfType<LevelManager>();
                levelManager.EndShowTextWithoutPause();
                OpenCloseDialogPanel(false);
                return;
            }

            



            if (dialogAnimated)
            {
                ContinueDialog();
            }
        }
    
    
    }
    public void OpenCloseDialogPanel(bool state)
    {
        panelDialogo.SetActive(state);
    }


    private void ConfigPanel(NpcConversation npcConversation)
    {
        OpenCloseDialogPanel(true);
        LoadDialogSequence(npcConversation);

        npcIcono.sprite = npcConversation.Icon;
        npcNombreTMP.text = $"{npcConversation.Name}:";
        ShowTextWithAnimation(npcConversation.Greeting);
    }

    

    private void LoadDialogSequence (NpcConversation npcConversation)
    {
        if(npcConversation.Conversation == null || npcConversation.Conversation.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < npcConversation.Conversation.Length; i++)
        {
            dialogSequence.Enqueue(npcConversation.Conversation[i].Sentence);    
        }
    }

   

    private void ContinueDialog()
    {
        if(NPCDisponible == null)
        {
            return;
        }

        if (goodByeShowed)
        {
            return;
        }

        if(dialogSequence.Count == 0)
        {
            string goodBye = NPCDisponible.Dialog.Goodbye;
            ShowTextWithAnimation(goodBye);
            goodByeShowed = true;
            return;
        }

        string nextDialog = dialogSequence.Dequeue();
        ShowTextWithAnimation(nextDialog);
    }

    private IEnumerator AnimateText(string sentence)
    {
        dialogAnimated = false;
        npcConversacionTMP.text = "";
        char[] letters = sentence.ToCharArray();
        for (int i = 0; i < letters.Length; i++)
        {
            npcConversacionTMP.text += letters[i];
            yield return new WaitForSeconds(0.03f);
        }


        dialogAnimated = true;
    }
    private void ShowTextWithAnimation (string sentence)
    {
        StartCoroutine(AnimateText(sentence));
    }





}
