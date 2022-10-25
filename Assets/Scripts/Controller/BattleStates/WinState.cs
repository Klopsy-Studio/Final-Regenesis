using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Win());
        
    }


    IEnumerator Win()
    {
        owner.placeholderWinScreen.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Battle");
    }
}
