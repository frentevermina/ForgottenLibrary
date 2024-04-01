using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerQuestDescription : QuestDescription
{
    [SerializeField] private TextMeshProUGUI tareaObjetivo;
   // [SerializeField] private TextMeshProUGUI recompensaFavor;
   // [SerializeField] private TextMeshProUGUI recompensaExp;

    [Header("Item")]
 //   [SerializeField] private Image recompensaItemIcono;
  //  [SerializeField] private TextMeshProUGUI recompensaItemCantidad;
    [SerializeField] private Image objetivoReferencia;



    private void Update()
    {
        if (QuestPorCompletar.QuestCompletadaCheck)
        {
            return;
        }

        tareaObjetivo.text = $"{QuestPorCompletar.CantidadActual}/{QuestPorCompletar.CantidadObjetivo}";
    }

    public override void ConfigQuestUI(Quest quest)
    {
        base.ConfigQuestUI(quest);
      //  recompensaFavor.text = quest.RecompensaFavor.ToString();
       // recompensaExp.text = quest.RecompensaExp.ToString();
        tareaObjetivo.text = $"{quest.CantidadActual}/{quest.CantidadObjetivo}";
        objetivoReferencia.sprite = quest.ItemReferencia;
       // recompensaItemIcono.sprite = quest.RecompensaItem.Item.Icono;
       // recompensaItemCantidad.text = quest.RecompensaItem.Cantidad.ToString();

    }

    private void QuestCompletadaRespuesta(Quest questCompletada)
    {
        if(questCompletada.ID == QuestPorCompletar.ID)
        {
            tareaObjetivo.text = $"{QuestPorCompletar.CantidadActual}/{QuestPorCompletar.CantidadObjetivo}";
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (QuestPorCompletar.QuestCompletadaCheck)
        {
            gameObject.SetActive(false);
        }

        Quest.EventoQuestCompletada += QuestCompletadaRespuesta;
    }

    private void OnDisable()
    {
        Quest.EventoQuestCompletada -= QuestCompletadaRespuesta;

    }

}

