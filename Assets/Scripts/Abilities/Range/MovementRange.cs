using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRange : AbilityRange
{
    public Tile tile;
    public int range;

    public bool removeContent = false;
    public override List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> retValue = board.Search(tile, ExpandSearch);

        if (removeContent)
        {
            List<Tile> trash = new List<Tile>(retValue);

            foreach (Tile t in trash)
            {
                if (t.content != null)
                {
                    retValue.Remove(t);
                }
            }
        }
        
        return retValue;
    }

    protected virtual bool ExpandSearch(Tile from, Tile to)
    {
        return (from.distance + 1) <= range;
    }
}
