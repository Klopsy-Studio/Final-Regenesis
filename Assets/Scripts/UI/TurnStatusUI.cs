using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnStatusUI : MonoBehaviour
{
    [SerializeField] GameObject playerTurn;
    [SerializeField] GameObject enemyTurn;
    [SerializeField] GameObject victoryTurn;
    [SerializeField] GameObject gameOverTurn;


    [Header("New Turn Status")]
    [SerializeField] Image turnStatus;

    [SerializeField] Sprite playerTurnImage;
    [SerializeField] Sprite enemyTurnImage;
    [SerializeField] Sprite eventTurnImage;

    [SerializeField] Animator turnStatusAnim;

    public void ActivatePlayerTurn()
    {
        playerTurn.SetActive(true);
    }

    public void ActivateEnemyTurn()
    {
        enemyTurn.SetActive(true);
    }

    public void DeactivatePlayerTurn()
    {
        playerTurn.SetActive(false);
    }

    public void DeactivateEnemyTurn()
    {
        enemyTurn.SetActive(false);
    }

    public void ActivateVictoryTurn()
    {
        victoryTurn.SetActive(true);
    }

    public void ActivateGameOverTurn()
    {
        gameOverTurn.SetActive(true);
    }

    public void DeactivateVictoryTurn()
    {
        victoryTurn.SetActive(false);
    }

    public void DeactivateGameOverTurn()
    {
        gameOverTurn.SetActive(false);
    }

    public void DeactivateBanner()
    {
        turnStatus.gameObject.SetActive(false);
    }

    public void ActivateBanner()
    {
        turnStatus.gameObject.SetActive(true);
    }

    public void PlayerTurn()
    {
        turnStatusAnim.SetBool("inScreen", true);
        turnStatus.sprite = playerTurnImage;
    }

    public void EnemyTurn()
    {
        turnStatusAnim.SetBool("inScreen", true);
        turnStatus.sprite = enemyTurnImage;
    }

    public void EventTurn()
    {
        turnStatusAnim.SetBool("inScreen", true);
        turnStatus.sprite = eventTurnImage;
    }

    public void Disappear()
    {
        turnStatusAnim.SetBool("inScreen", false);
    }
}
