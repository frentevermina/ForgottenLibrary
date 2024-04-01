using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuestDescription : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI questNombre;
    [SerializeField] private TextMeshProUGUI questObjetivo;
    [SerializeField] private Image imagenReferencia;

    public Quest QuestPorCompletar { get; set; }


   public virtual void ConfigQuestUI(Quest quest)
    {
        QuestPorCompletar = quest;
        questNombre.text = quest.Nombre;
        questObjetivo.text = quest.Objetivo;
        imagenReferencia.sprite = quest.ItemReferencia;
    }
}
