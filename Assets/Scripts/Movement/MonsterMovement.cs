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

        SideAbilityRange sideRange = this.GetComponent<SideAbilityRange>();

        sideRange.sideDir = pushDir;
        sideRange.sideLength = 1;
        sideRange.sideReach = pushStrength;

        List<Tile> dirT = sideRange.GetTilesInRange(board);

        if(dirT != null)
        {
            foreach (Tile e in dirT)
            {
                if (e.content != null || e.CheckSurroundings(board) != null)
                {
                    canPush = false;
                    return;
                }
            }
        }
        else
        {
            canPush = false;
            return;
        }
        

        canPush = true;

        if (desiredTile != null && desiredTile.content == null && canPush)
        {
            StartCoroutine(Traverse(desiredTile, board));
        }
    }
}
