using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/MoveAction")]
public class MoveAction : Action
{
    Tile closestTile;
    public override void Act(MonsterController controller)
    {
        MoveMonster(controller);
        Debug.Log("MOVE ACTION");
    }

  
    void MoveMonster(MonsterController controller)
    {
        //Move To Closest Player Unit
        float closestDistance = 0f;
        Unit t = new Unit();

        //T is the closest unit to the enemy
        foreach (PlayerUnit p in controller.battleController.playerUnits)
        {
            if (Vector3.Distance(controller.currentEnemy.transform.position, p.transform.position) <= closestDistance || closestDistance == 0f)
            {
                t = p;
                closestDistance = Vector3.Distance(controller.currentEnemy.transform.position, p.transform.position);
            }
        }

        Movement range = controller.currentEnemy.GetComponent<Movement>();
        range.range = 3;

        //MovementRange range = GetRange<MovementRange>();

        //range.unit = owner.currentEnemy;
        //range.range = 3;
        //range.tile = owner.currentEnemy.tile;

        List<Tile>tiles = range.GetTilesInRangeForEnemy(controller.battleController.board, false);


        closestDistance = 0f;
        
        foreach (Tile tile in tiles)
        {
            if (Vector3.Distance(tile.transform.position, t.tile.transform.position) <= closestDistance || closestDistance == 0f && tile.content == null)
            {
                closestDistance = Vector3.Distance(tile.transform.position, t.tile.transform.position);
                closestTile = tile;
            }
        }

        Movement m = controller.currentEnemy.GetComponent<Movement>();

        List<Tile> test = new List<Tile>();
        test.Add(closestTile);

        controller.battleController.board.SelectMovementTiles(test);
        m.StartTraverse(closestTile);
        //StartCoroutine(m.Traverse(closestTile));

        //while (m.moving)
        //{
        //    //yield return null;
        //}
        controller.battleController.board.DeSelectDefaultTiles(test);
        //owner.ChangeState<Monster1CheckNextAction>();
    }
}
