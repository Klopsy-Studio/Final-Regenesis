using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapUnit : TileModifier
{
    //public TrapObject trap;
    public override void Effect(Unit unit)
    {
        unit.Stun();

        t.modifiers.Remove(this);
    }
}
