using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : WalkMovement
{ 
    bool canPush;

    public override void PushUnit(Directions pushDir, int pushStrength, Board board)
    {
        range = pushStrength;
        List<Tile> t = GetTilesInRange(board, true);
        Tile desiredTile = null;
        foreach (Tile dirTile in t)
        {
            if (dirTile.GetDirections(unit.tile) == pushDir)
            {
                desiredTile = dirTile;
            }
        }

        SideAbilityRange sideRange = this.GetComponent<SideAbilityRange>();

        sideRange.sideDir = pushDir;

        List<Tile> dirT = sideRange.GetTilesInRange(board);

        foreach(Tile e in dirT)
        {
            if(e.content!= null)
            {
                canPush = false;
                return;
            }
        }

        canPush = true;

        if (desiredTile != null && desiredTile.content == null && canPush)
        {
            StartCoroutine(Traverse(desiredTile, board));
        }
    }
}
