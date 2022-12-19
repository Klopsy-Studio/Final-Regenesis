using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearObstacleScript : MonoBehaviour
{
    [SerializeField] SquareAbilityRange squareRange;
    [SerializeField] CrossAbilityRange crossRange;
    [SerializeField] MonsterAbility obstacleAbility;

    public MonsterController controller;
    public Point pos;
    [SerializeField] Animator obstacleAnimations;

    public List<Tile> Explode(Board board, BattleController battleController)
    {
        obstacleAnimations.SetTrigger("explode");
        List<Tile> tiles = squareRange.GetTilesInRangeWithoutUnit(board, pos);
        board.SelectAttackTiles(tiles);
        foreach(Tile t in tiles)
        {
            if(t.content != null)
            {
                if(t.content.GetComponent<PlayerUnit>() != null)
                {
                    PlayerUnit p = t.content.GetComponent<PlayerUnit>();
                    obstacleAbility.UseAbility(p, controller.currentEnemy, battleController);
                }
            }
        }
        board.GetTile(pos).content = null;
        return tiles;
    }

    public void GetDestroyed(Board board)
    {
        board.GetTile(pos).content = null;
        controller.obstaclesInGame.Remove(this);

        if (controller.validObstacles.Contains(this))
        {
            controller.validObstacles.Remove(this);
        }

        this.gameObject.SetActive(false);

    }
    public bool IsObstacleValid(Board board)
    {
        foreach(Tile t in crossRange.GetTilesOnRangeWithoutUnit(pos, board))
        {
            if(t.CheckSurroundings(board) != null)
            {
                return true;
            }
        }

        return false;
    }


    public List<Tile> GetValidTiles(Board board)
    {
        List<Tile> tiles = crossRange.GetTilesOnRangeWithoutUnit(pos, board);
        List<Tile> validTiles = new List<Tile>();

        foreach(Tile t in tiles)
        {
            if (t.CheckSurroundings(board) != null)
            {
                validTiles.Add(t);
            }
        }

        return validTiles;
    }
}
