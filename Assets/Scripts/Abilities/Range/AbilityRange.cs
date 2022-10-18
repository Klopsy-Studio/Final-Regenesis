using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityRange : MonoBehaviour
{
    public int horizontal = 1;
    public int vertical = int.MaxValue;
    public virtual bool directionOriented { get { return false; } }
    public Unit unit;
    public abstract List<Tile> GetTilesInRange(Board board);

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


    public virtual void AssignVariables(RangeData rangeData)
    {

    }

}
