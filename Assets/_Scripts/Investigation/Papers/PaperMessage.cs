using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class PaperMessage : ScriptableObject
{
    [Header("Informaci�n")]
    [TextArea] public string informacion;
}
