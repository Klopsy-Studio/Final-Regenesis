using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideAbilityRange : AbilityRange
{
    public Directions dir = Directions.North;
    public int reach;
    public override List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> retValue = new List<Tile>();
        List<Tile> tiles = new List<Tile>();

        switch (dir)
        {
            case Directions.North:
                for (int i = -1; i < 2; i++)
                {
                    if (board.GetTile(new Point(unit.currentPoint.x+i, unit.currentPoint.y +reach)) != null)
                    {
                        retValue.Add(board.GetTile(new Point(unit.currentPoint.x + reach, unit.currentPoint.y + i)));
                    }
                }
                break;
            case Directions.East:
                for (int i = -1; i < 2; i++)
                {
                    if (board.GetTile(new Point(unit.currentPoint.x -reach, unit.currentPoint.y +i)) != null)
                    {
                        retValue.Add(board.GetTile(new Point(unit.currentPoint.x + reach, unit.currentPoint.y + i)));
                    }
                }
                break;
            case Directions.South:
                for (int i = -1; i < 2; i++)
                {
                    if (board.GetTile(new Point(unit.currentPoint.x + i , unit.currentPoint.y - reach)) != null)
                    {
                        retValue.Add(board.GetTile(new Point(unit.currentPoint.x + reach, unit.currentPoint.y + i)));
                    }
                }
                break;
            case Directions.West:
                for (int i = -1; i < 2; i++)
                {
                    if (board.GetTile(new Point(unit.currentPoint.x + reach, unit.currentPoint.y + i)) != null)
                    {
                        retValue.Add(board.GetTile(new Point(unit.currentPoint.x + reach, unit.currentPoint.y + i)));
                    }
                }
                break;
            default:
                break;
        }
       

        return retValue;
    }

    public void ChangeDirections(Tile t)
    {
        dir = unit.tile.GetDirections(t);
    }
}
