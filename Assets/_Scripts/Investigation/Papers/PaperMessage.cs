using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class PaperMessage : ScriptableObject
{
    [Header("Información")]
    [TextArea] public string informacion;
}
