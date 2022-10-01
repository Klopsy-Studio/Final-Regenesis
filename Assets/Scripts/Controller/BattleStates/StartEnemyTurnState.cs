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

       
        //StartCoroutine(StartEnemyTurnCoroutine());
    }

    //IEnumerator StartEnemyTurnCoroutine()
    //{
    //    yield return null;
    //    owner.currentEnemyController.StartEnemy();
    //}


    IEnumerator StartEnemyTurnCoroutine()
    {
        yield return null;
        owner.monsterController.StartState();
    }


}
