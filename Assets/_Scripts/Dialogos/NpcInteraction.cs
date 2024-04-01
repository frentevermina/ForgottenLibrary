using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteraction : MonoBehaviour
{
    [SerializeField] private GameObject npcInteractionButton;
    [SerializeField] private NpcConversation npcDialogo;

    public NpcConversation Dialog => npcDialogo; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogManager.Instance.NPCDisponible = this;
            npcInteractionButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            DialogManager.Instance.NPCDisponible = null;
            npcInteractionButton.SetActive(false);
        }

    }


}
