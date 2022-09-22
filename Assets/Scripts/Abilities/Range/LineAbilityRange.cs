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

        for (int i = 0; i < lineLength; i++)
        {
            switch (lineDir)
            {
                case Directions.North:
                    if(board.GetTile(startPos + new Point (0, 1)) != null)
                    {
                        startPos += new Point(0, 1);
                        retValue.Add(board.GetTile(startPos));
                    }
                    else
                    {
                        break;
                    }
                    break;
                case Directions.East:
                    if (board.GetTile(startPos + new Point(1, 0)) != null)
                    {
                        startPos += new Point(1, 0);
                        retValue.Add(board.GetTile(startPos));
                    }
                    else
                    {
                        break;
                    }
                    break;
                case Directions.South:
                    if (board.GetTile(startPos + new Point(0, -1)) != null)
                    {
                        startPos += new Point(0, -1);
                        retValue.Add(board.GetTile(startPos));
                    }
                    else
                    {
                        break;
                    }
                    break;
                case Directions.West:
                    if (board.GetTile(startPos + new Point(-1, 0)) != null)
                    {
                        startPos += new Point(-1, 0);
                        retValue.Add(board.GetTile(startPos));
                    }
                    else
                    {
                        break;
                    }
                    break;
                default:
                    break;
            }
        }

        return retValue;

    }

    public void ChangeDirection(Directions dir)
    {
        lineDir = dir;
    }
}
