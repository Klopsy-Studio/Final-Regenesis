using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        owner.isTimeLineActive = false;
        StartCoroutine(LoseState());
    }


    IEnumerator LoseState()
    {
        owner.turnStatusUI.IndicateTurnStatus(owner.turnStatusUI.loseTurn);
        yield return new WaitForSeconds(1);
        owner.turnStatusUI.DeactivateTurn();
        yield return new WaitForSeconds(1);

        //Switch later to show Loot load camp scene 
        SceneManager.LoadScene("Battle");
    }

}
