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

        for (int x = -squareReach; x < squareReach + 1; x++)
        {
            for (int y = -squareReach; y < squareReach + 1; y++)
            {
                if (IsTileValid(board, x, y))
                {
                    retValue.Add(board.GetTile(new Point(unit.currentPoint.x + x, unit.currentPoint.y + y)));
                }
            }
        }

        return retValue;
    }



    protected Tile GetTileInPosition(Point pos, Board board)
    {
        if (board.GetTile(unit.currentPoint + pos) != null)
        {
            return board.GetTile(unit.currentPoint + pos);
        }
        else
        {
            return null;
        }
    }


    public bool IsTileValid(Board board, int offsetX, int offsetY)
    {
        if (board.GetTile(new Point(unit.currentPoint.x + offsetX, unit.currentPoint.y + offsetY)) != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
