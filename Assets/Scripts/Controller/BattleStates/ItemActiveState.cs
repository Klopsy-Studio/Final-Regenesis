using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActiveState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        owner.isTimeLineActive = false;
        StartCoroutine(ItemCoroutine());
    }

    IEnumerator ItemCoroutine()
    {

        owner.currentItem.Apply();

        yield return new WaitForSeconds(2);
        owner.ChangeState<TimeLineState>();
    }

    public override void Exit()
    {
        base.Exit();
        owner.isTimeLineActive = true;
    }
}
