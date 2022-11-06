using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("you lost");
    }

}
