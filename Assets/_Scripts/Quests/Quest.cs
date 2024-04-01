using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
    public static Action<Quest> EventoQuestCompletada;

    [Header("Info")]
    [Tooltip("Nombre de la Quest")] public string Nombre;
    [Tooltip("Identificador para saber si está completa o no")] public string ID;
    [Tooltip("Cantidad objetivo de la quest")]public int CantidadObjetivo;

    [Header("Descripción")]
    [Tooltip("Item de referencia para el objetivo")] public Sprite ItemReferencia;
    public string Objetivo;
    [TextArea] public string Descripcion;

    [Header("Recompensas")]
    [Tooltip("La xp que ganas")] public int RecompensaExp;
    public QuestRewardItem RecompensaItem;

    [HideInInspector] public int CantidadActual;
    [HideInInspector] public bool QuestCompletadaCheck;

    public void AñadirProgreso(int cantidad)
    {
        CantidadActual += cantidad;
        VerificarQuestCompletada();
    }

    private void VerificarQuestCompletada()
    {
        if(CantidadActual >= CantidadObjetivo)
        {
            CantidadActual = CantidadObjetivo;
            QuestCompletada();
        }
    }

    private void QuestCompletada()
    {
        if (QuestCompletadaCheck)
        {
            return;
        }

        QuestCompletadaCheck = true;
        EventoQuestCompletada?.Invoke(this);

    }

    private void OnEnable()
    {
        QuestCompletadaCheck = false;
        CantidadActual = 0;
    }


}

[Serializable]
public class QuestRewardItem
{
    public InventarioItem Item;
    public int Cantidad;
}