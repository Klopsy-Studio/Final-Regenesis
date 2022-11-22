using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : WalkMovement
{ 
    bool canPush;

    public override void PushUnit(Directions pushDir, int pushStrength, Board board)
    {
        CrossAbilityRange crossRange = GetComponent<CrossAbilityRange>();
        crossRange.offset = pushStrength - 1;
        crossRange.crossLength = pushStrength;
        range = pushStrength;
        List<Tile> t = crossRange.GetTilesInRange(board);
        Tile desiredTile = null;

        foreach (Tile dirTile in t)
        {
            if (unit.tile.GetDirections(dirTile) == pushDir)
            {
                desiredTile = dirTile;
            }
        }
       

        if (desiredTile != null && desiredTile.content == null && desiredTile.CheckSurroundings(board) != null)
        {
            desiredTile.prev = unit.tile;
            StartCoroutine(Traverse(desiredTile, board));
        }
    }
}
