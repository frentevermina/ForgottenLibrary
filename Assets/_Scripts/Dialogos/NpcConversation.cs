using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum InteractionExtraNPC
{
    Quests,
    Tienda,
    Crafting
}
[CreateAssetMenu]
public class NpcConversation : ScriptableObject
{
    [Header("Info")]
    [Tooltip("Nombre")] public string Name;
    [Tooltip("icono")] public Sprite Icon;
    [Tooltip("Si tiene interacciones extra")] public bool HaveExtraInteraction;
    [Tooltip("Interacción extra")] public InteractionExtraNPC InteractionExtra;


    [Header("Saludo")]
    [TextArea] public string Greeting;

    [Header("Conversación")]
    public DialogTex[] Conversation;

    [Header("Posibles respuestaas")]
    private List<AnswerText> answerTexts;

    [Header("Despedida")]
    [TextArea] public string Goodbye;


    [Serializable]
    public class DialogTex
    {
        [TextArea] public string Sentence;

    }

    public class AnswerText
    {
        [TextArea] public string Answer;
    }

}
