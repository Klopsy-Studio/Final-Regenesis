using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideAbilityRange : SquareAbilityRange
{
    public Directions dir = Directions.North;
    public override List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> retValue = new List<Tile>();
        List<Tile> tiles = new List<Tile>();

        if(GetTileInPosition(new Point(0, 0), board)!= null)
        {
            tiles.Add(GetTileInPosition(new Point(0, 0), board));
        }

        if (GetTileInPosition(new Point(1, 0), board) != null) ;
        {
            tiles.Add(GetTileInPosition(new Point(1, 0), board));
        }
        
        if(GetTileInPosition(new Point(-1, 0), board) != null)
        {
            tiles.Add(GetTileInPosition(new Point(-1, 0), board));
        }
        
        if(GetTileInPosition(new Point(0, -1), board) != null)
        {
            tiles.Add(GetTileInPosition(new Point(0, -1), board));
        }

        if(GetTileInPosition(new Point(0, 1), board) != null)
        {
            tiles.Add(GetTileInPosition(new Point(0, 1), board));
        }
        
        if(GetTileInPosition(new Point(1, 1), board) != null)
        {
            tiles.Add(GetTileInPosition(new Point(1, 1), board));
        }
        
        if(GetTileInPosition(new Point(-1, -1), board) != null)
        {
            tiles.Add(GetTileInPosition(new Point(-1, -1), board));
        }
        
        if(GetTileInPosition(new Point(1, -1), board) != null)
        {
            tiles.Add(GetTileInPosition(new Point(1, -1), board));
        }
        
        if(GetTileInPosition(new Point(-1, 1), board) != null)
        {
            tiles.Add(GetTileInPosition(new Point(-1, 1), board));
        }

        foreach (Tile t in tiles)
        {
            if(unit.tile.CheckSpecificDirection(t, dir))
            {
                retValue.Add(t);
            }
        }

        return retValue;
    }

    public void ChangeDirections(Tile t)
    {
        dir = unit.tile.GetDirections(t);
    }
}
