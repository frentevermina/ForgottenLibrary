using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestManager : Sigleton<QuestManager>
{
    [Header("Personaje")]
    [SerializeField] private Player personaje;


    [Header("Quests")]
    [Tooltip("Quest Disponibles")] [SerializeField] private Quest[] questAvailable;

    [Header("Doctor Quests")]
    [Tooltip("Prefab de la Quest")] [SerializeField] private DoctorQuestDescription doctorQuestPrefab;
    [Tooltip("Contenedor del Quest")] [SerializeField] private Transform doctorQuestContainer;


    [Header("Player Quests")]
    [Tooltip("Prefab de la Quest")] [SerializeField] private PlayerQuestDescription personajeQuestPrefab;
    [Tooltip("Contenedor del Quest")] [SerializeField] private Transform personajeQuestContainer;

    [Header("Panel Quest Completada")]
    [SerializeField] private GameObject panelQuestCompletada;
    [SerializeField] private TextMeshProUGUI questNombre;
    [SerializeField] private TextMeshProUGUI questRecompensaDinero;
    [SerializeField] private TextMeshProUGUI questRecompensaExp;
    [SerializeField] private TextMeshProUGUI questRecompensaItemCantidad;
    [SerializeField] private Image questRecompensaItemIcono;
    [SerializeField] private TextoAnimacionNivelUp _itemGanado;

    [SerializeField] private Quest quest1;
    [SerializeField] private Quest quest2;
    [SerializeField] private Quest quest3;

    public Quest Quest1 => quest1;
    public Quest Quest2 => quest2;
    public Quest Quest3 => quest3;

    public Quest QuestPorReclamar { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        LoadQuestInDoctor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            AñadirProgreso(quest1.ID, 1);
            AñadirProgreso(quest2.ID, 1);
            AñadirProgreso(quest3.ID, 1);
        }
    }

    // Update is called once per frame
    private void LoadQuestInDoctor()
    {
        for (int i = 0; i < questAvailable.Length; i++)
        {
            DoctorQuestDescription newQuest = Instantiate(doctorQuestPrefab, doctorQuestContainer);
            newQuest.ConfigQuestUI(questAvailable[i]);
        }
    }

    private void AddQuestToComplete(Quest questToComplete)
    {

        PlayerQuestDescription newQuest = Instantiate(personajeQuestPrefab, personajeQuestContainer);
        newQuest.ConfigQuestUI(questToComplete);
       
    }

    public void AddQuest(Quest questToComplete)
    {
        AddQuestToComplete(questToComplete);
    }

    public void ReclamarRecompensa()
    {
        if(QuestPorReclamar == null)
        {
            return;
        }

        personaje.PersonajeExperiencia.AñadirExperiencia(QuestPorReclamar.RecompensaExp);
        _itemGanado.RespuestaItemGanado();
        Inventario.Instance.AñadirItem(QuestPorReclamar.RecompensaItem.Item, QuestPorReclamar.RecompensaItem.Cantidad);
        panelQuestCompletada.SetActive(false);

        QuestPorReclamar = null;

    }



    public void AñadirProgreso(string questID, int cantidad)
    {
        Quest questPorActualizar = QuestExiste(questID);
        questPorActualizar.AñadirProgreso(cantidad);
    }

    private Quest QuestExiste(string questID)
    {
        for (int i = 0; i < questAvailable.Length; i++)
        {
            if(questAvailable[i].ID == questID)
            {
                return questAvailable[i];
            }
        }

        return null;
    }

    private void MostrarQuestCompletada(Quest questCompletada)
    {
        panelQuestCompletada.SetActive(true);
        questNombre.text = questCompletada.Nombre;
        questRecompensaExp.text = questCompletada.RecompensaExp.ToString() + " Exp";
        questRecompensaItemCantidad.text = questCompletada.RecompensaItem.Cantidad.ToString();
        questRecompensaItemIcono.sprite = questCompletada.RecompensaItem.Item.Icono;
    }

    private void QuestCompletadaRespuesta(Quest questCompletada)
    {
        QuestPorReclamar = QuestExiste(questCompletada.ID);
        if(QuestPorReclamar != null)
        {
            MostrarQuestCompletada(QuestPorReclamar);
        }
    }


    private void OnEnable()
    {
        
        Quest.EventoQuestCompletada += QuestCompletadaRespuesta;
    }

    private void OnDisable()
    {
        Quest.EventoQuestCompletada -= QuestCompletadaRespuesta;
    }




}
