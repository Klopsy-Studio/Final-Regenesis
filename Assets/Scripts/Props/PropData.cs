using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PropData
{
    public PropType type;
    public string displayName;
    public int typeIndex;
    [HideInInspector] public bool occupiesSpace;
}
