using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class DoctorQuestDescription : QuestDescription
{

   // [SerializeField] private TextMeshProUGUI questRecompensa;
   // [SerializeField] private Image objetivoReferencia;



    public override void ConfigQuestUI(Quest quest )
    {
        base.ConfigQuestUI(quest);
        /*questRecompensa.text = $"-{quest.RecompensaFavor} Favor" +
                               $"\n-{quest.RecompensaExp} Exp" +
                               $"\n-{quest.RecompensaItem.Cantidad} {quest.RecompensaItem.Item.Nombre}";*/

        //objetivoReferencia.sprite = questItemReferencia.ItemReferencia;
       // objetivoReferencia.sprite = quest.ItemReferencia;
       
    
    }

    public void AcceptQuest()
    {
        if(QuestPorCompletar == null)
        {
            return;
        }
        
        QuestManager.Instance.AddQuest(QuestPorCompletar);
        gameObject.SetActive(false);
    }

}


