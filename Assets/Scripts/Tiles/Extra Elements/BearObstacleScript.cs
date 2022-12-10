using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearObstacleScript : MonoBehaviour
{
    [SerializeField] SquareAbilityRange squareRange;
    [SerializeField] CrossAbilityRange crossRange;
    [SerializeField] float obstacleDamage;

    public Point pos;
    public List<Tile> Explode(Board board)
    {
        List<Tile> tiles = squareRange.GetTilesInRangeWithoutUnit(board, pos);
        board.SelectAttackTiles(tiles);
        foreach(Tile t in tiles)
        {
            if(t.content != null)
            {
                if(t.content.GetComponent<PlayerUnit>() != null)
                {
                    PlayerUnit p = t.content.GetComponent<PlayerUnit>();
                    p.ReceiveDamage(obstacleDamage);
                    p.Damage();
                    
                }
            }
        }

        board.GetTile(pos).content = null;
        return tiles;
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
