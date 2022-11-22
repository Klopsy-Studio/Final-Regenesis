using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        owner.isTimeLineActive = false;
        StartCoroutine(Win());
    }


    IEnumerator Win()
    {
        owner.turnStatusUI.IndicateTurnStatus(owner.turnStatusUI.winTurn);
        yield return new WaitForSeconds(1);
        owner.turnStatusUI.DeactivateTurn();
        yield return new WaitForSeconds(1);
        owner.levelData.hasBeenCompleted = true;

        //Switch later to show Loot load camp scene 
        SceneManager.LoadScene("Battle");
    }
}
