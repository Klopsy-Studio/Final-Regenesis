using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideAbilityRange : AbilityRange
{
    public Directions sideDir;
    public int sideReach;
    public int sideLength;
    public override List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> retValue = new List<Tile>();

        switch (sideDir)
        {
            case Directions.North:

                for (int i = -sideLength; i < sideLength; i++)
                {
                    if (GetTileInPosition(new Point(i, sideReach), board) != null)
                    {
                        retValue.Add(GetTileInPosition(new Point(i, sideReach), board));
                    }
                }
                break;
            case Directions.East:
                for (int i = -sideLength; i < sideLength; i++)
                {
                    if (GetTileInPosition(new Point(-sideReach, i), board) != null)
                    {
                        retValue.Add(GetTileInPosition(new Point(i, sideReach), board));
                    }
                }
                break;
            case Directions.South:
                for (int i = -sideLength; i < sideLength; i++)
                {
                    if (GetTileInPosition(new Point(i, -sideReach), board) != null)
                    {
                        retValue.Add(GetTileInPosition(new Point(i, sideReach), board));
                    }
                }
                break;
            case Directions.West:
                for (int i = -sideLength; i < sideLength; i++)
                {
                    if (GetTileInPosition(new Point(sideReach, i), board) != null)
                    {
                        retValue.Add(GetTileInPosition(new Point(i, sideReach), board));
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
        sideDir = unit.tile.GetDirections(t);
    }

    public void AssignVariables(Directions newSideDir, int newSideReach, int newSideLength)
    {
        sideDir = newSideDir;
        sideReach = newSideReach;
        sideLength = newSideLength;
    }

}
