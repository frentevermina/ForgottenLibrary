using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class InvestigationConversation : ScriptableObject
{
    [Header("Informaci�n")]
    [TextArea] public string informacion;

    //[TextArea] public string AddInfo;
    //[TextArea] public string StatsNecesarios;
}
