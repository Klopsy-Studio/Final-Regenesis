using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAbilityRange : AbilityRange
{
    public int lineLength = 2;
    public Directions lineDir;
    public override List<Tile> GetTilesInRange(Board board)
    {
        Point startPos = unit.tile.pos;
        List<Tile> retValue  = new List<Tile>();


        switch (lineDir)
        {
            case Directions.North:
                for (int i = 0; i < lineLength+1; i++)
                {
                    if (board.GetTile(startPos + new Point(0, i)) != null)
                    {
                        retValue.Add(board.GetTile(startPos + new Point(0, i)));
                    }
                    else
                    {
                        break;
                    }
                 
                }
                break;
            case Directions.East:
                for (int i = 0; i < lineLength+1; i++)
                {
                    if (board.GetTile(startPos + new Point(-i, 0)) != null)
                    {
                        retValue.Add(board.GetTile(startPos+ new Point(-i, 0)));
                    }
                    else
                    {
                        break;
                    }

                }
                break;
            case Directions.South:
                for (int i = 0; i < lineLength+1; i++)
                {
                    if (board.GetTile(startPos + new Point(0,-i)) != null)
                    {
                        retValue.Add(board.GetTile(startPos + new Point(0, -i)));
                    }
                    else
                    {
                        break;
                    }

                }
                break;
            case Directions.West:
                for (int i = 0; i < lineLength+1; i++)
                {
                    if (board.GetTile(startPos + new Point(i, 0)) != null)
                    {
                        retValue.Add(board.GetTile(startPos + new Point(i, 0)));
                    }
                    else
                    {
                        break;
                    }

                }
                break;
            default:
                break;
        }

        return retValue;

    }

    public void ChangeDirection(Directions dir)
    {
        lineDir = dir;
    }

    public override void AssignVariables(RangeData rangeData)
    {
        lineDir = rangeData.lineDir;
        lineLength = rangeData.lineLength;
    }
}
