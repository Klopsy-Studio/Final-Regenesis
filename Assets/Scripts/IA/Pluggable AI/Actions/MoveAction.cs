using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/MoveAction")]
public class MoveAction : Action
{
    public bool isCalled = false;
    Movement m;
    public override void Act(MonsterController controller)
    {
        if (!isCalled)
        {
            controller.CallCoroutine(MoveMonster(controller));
        }
    }


    IEnumerator MoveMonster(MonsterController controller)
    {
        isCalled = true;
        //Move To Closest Player Unit
        Tile closestTile = null;
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

        m = controller.currentEnemy.GetComponent<Movement>();

        List<Tile> test = new List<Tile>();
        test.Add(closestTile);
      
        controller.battleController.board.SelectMovementTiles(test);
        controller.CallCoroutine(m.Traverse(closestTile, controller.battleController.board));

        //StartCoroutine(m.Traverse(closestTile));

        while (m.moving)
        {
            yield return null;
        }

        controller.battleController.board.DeSelectDefaultTiles(test);
        //owner.ChangeState<Monster1CheckNextAction>();
        //m.isTraverseCalled = false;

      
        Debug.Log("ISTRAVERSECALLED, " + m.isTraverseCalled);

        controller.currentEnemy.currentPoint = closestTile.pos;
        controller.currentEnemy.actionDone = true;
        OnExit(controller);
    }

    protected override void OnExit(MonsterController controller)
    {
        isCalled = false;
        base.OnExit(controller);
        //m.isTraverseCalled = false;
    }
}
