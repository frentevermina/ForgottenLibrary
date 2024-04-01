using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXType : MonoBehaviour
{
    public enum SoundType
    {
        PICK_ITEM,
        LEVELUP,
        UNLOCK_DOOR,
        LOCKED_DOOR,
        DOOR_OPENING,
        DOOR_OPENING_CORREDERA,
        PAPER_SHOWING,
        TABLE_OPENCLOSE
    }

    public SoundType type;
}
