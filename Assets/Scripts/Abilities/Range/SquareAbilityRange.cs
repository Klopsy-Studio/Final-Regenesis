using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareAbilityRange : AbilityRange
{
    public int squareReach = 1;
    public override List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> retValue = new List<Tile>();

        retValue.Add(GetTileInPosition(new Point(0, 0), board));

        for (int i = 1; i <= squareReach; i++)
        {
            if(GetTileInPosition(new Point(i, 0), board) != null)
            {
                retValue.Add(GetTileInPosition(new Point(i, 0), board));
            }

            if(GetTileInPosition(new Point(-i, 0), board) != null)
            {
                retValue.Add(GetTileInPosition(new Point(-i, 0), board));
            }

            if(GetTileInPosition(new Point(0, -i), board) != null)
            {
                retValue.Add(GetTileInPosition(new Point(0, -i), board));
            }

            if(GetTileInPosition(new Point(0, i), board) != null)
            {
                GetTileInPosition(new Point(0, i), board);
            }

            if(GetTileInPosition(new Point(0, i), board) != null)
            {
                retValue.Add(GetTileInPosition(new Point(0, i), board));
            }

            if(GetTileInPosition(new Point(i, i), board) != null)
            {
                retValue.Add(GetTileInPosition(new Point(i, i), board));
            }

            if(GetTileInPosition(new Point(-i, -i), board) != null)
            {
                retValue.Add(GetTileInPosition(new Point(-i, -i), board));
            }
            
            if(GetTileInPosition(new Point(i, -i), board) != null)
            {
                retValue.Add(GetTileInPosition(new Point(i, -i), board));
            }
            
            if(GetTileInPosition(new Point(-i, i), board))
            {
                retValue.Add(GetTileInPosition(new Point(-i, i), board));
            }
            
        }

        return retValue;
    }


    
    protected Tile GetTileInPosition(Point pos, Board board)
    {
        if(board.GetTile(unit.currentPoint + pos) != null)
        {
            return board.GetTile(unit.currentPoint + pos);
        }
        else
        {
            return null;
        }
    }
}
