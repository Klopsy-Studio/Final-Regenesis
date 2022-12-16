using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageModifier
{
    public int modifierCount;
    public virtual bool SpendModifier()
    {
        modifierCount--;
        if (modifierCount <= 0)
            return true;
        else
            return false;
    }
}
