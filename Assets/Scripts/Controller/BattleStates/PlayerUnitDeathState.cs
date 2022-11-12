using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitDeathState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(DeathSequence());
    }


    IEnumerator DeathSequence()
    {
        SelectTile(owner.currentUnit.currentPoint);
        owner.currentUnit.Die(owner);

        yield return new WaitForSeconds(1f);
        owner.ChangeState<TimeLineState>();
    }
}
