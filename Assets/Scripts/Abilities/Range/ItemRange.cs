using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRange : AbilityRange
{
    public Tile tile;
    public int range;

    public bool removeContent = true;
    public override List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> retValue = board.Search(tile, ExpandSearch);

        List<Tile> garbage = new List<Tile>(retValue);

        if (removeContent)
        {
            foreach (Tile t in garbage)
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
