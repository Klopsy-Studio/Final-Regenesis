using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnemyTurnState : BattleState
{
    //Estado en que se elige al enemigo

    public override void Enter()
    {
        base.Enter();
        owner.isTimeLineActive = false;

        owner.turnStatusUI.ActivateBanner();
        owner.turnStatusUI.EnemyTurn();
        //tileSelectionIndicator.gameObject.SetActive(false);

        //StartCoroutine(EnemyTurnSequence());
        StartCoroutine(StartEnemyTurnCoroutine());
    }

    IEnumerator StartEnemyTurnCoroutine()
    {
        yield return null;
        owner.currentEnemyController.StartEnemy();
    }


    //Sistema anterior

    //IEnumerator EnemyTurnSequence()
    //{
    //    EnemyUnit enemy = owner.enemyUnits[0].GetComponent<EnemyUnit>();
    //    if (enemy != null)
    //    {
    //        owner.currentEnemyTurn = enemy;
    //        owner.turnStatusUI.ActivateEnemyTurn();

    //        yield return new WaitForSeconds(2);

    //        owner.turnStatusUI.DeactivateEnemyTurn();
    //        yield return null;
    //        owner.ChangeState<EnemyMoveTargetState>();
    //    }

    //}


}
