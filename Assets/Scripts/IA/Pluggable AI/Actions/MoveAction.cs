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

    private void OnEnable()
    {
        isCalled = false;
    }

    IEnumerator MoveMonster(MonsterController controller)
    {
        yield return new WaitForSeconds(1);
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
        range.range = 5;

        //MovementRange range = GetRange<MovementRange>();

        //range.unit = owner.currentEnemy;
        //range.range = 3;
        //range.tile = owner.currentEnemy.tile;

        List<Tile> unfilteredTiles = range.GetTilesInRangeForEnemy(controller.battleController.board, false);
        List<Tile> tiles = new List<Tile>();
        foreach(Tile validTile in unfilteredTiles)
        {
            if (validTile.CheckSurroundings(controller.battleController.board) != null)
            {
                tiles.Add(validTile);
            }
        }

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
      
        //controller.battleController.board.SelectMovementTiles(test);
        

        controller.monsterAnimations.SetBool("hide", true);
        AudioManager.instance.Play("MonsterMovement");
        yield return new WaitForSeconds(1f);

        controller.monsterAnimations.SetBool("hide", false);

        controller.battleController.SelectTile(closestTile.pos);
        controller.battleController.tileSelectionToggle.MakeTileSelectionBig();
        controller.CallCoroutine(m.SimpleTraverse(closestTile));

        controller.monsterAnimations.SetBool("appear", true);
        AudioManager.instance.Play("MonsterMovement");

        yield return new WaitForSeconds(1f);

        controller.monsterAnimations.SetBool("appear", false);
        controller.monsterAnimations.SetBool("idle", true);
        controller.monsterAnimations.SetBool("idle", false);


        yield return new WaitForSeconds(0.2f);
        controller.battleController.board.DeSelectDefaultTiles(test);
        controller.currentEnemy.UpdateMonsterSpace(controller.battleController.board);


        controller.currentEnemy.currentPoint = closestTile.pos;
        controller.currentEnemy.actionDone = true;
        OnExit(controller);
    }

    protected override void OnExit(MonsterController controller)
    {
        isCalled = false;
        base.OnExit(controller);
    }
}
