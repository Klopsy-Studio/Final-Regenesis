using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearObstacleScript : MonoBehaviour
{
    [SerializeField] SquareAbilityRange squareRange;
    [SerializeField] float obstacleDamage;
    public List<Tile> Explode(Point pos, Board board)
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
}
