using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Place Obstacle")]

public class PlaceObstacleAction : Action
{
    public override void Act(MonsterController controller)
    {
        controller.CallCoroutine(PlaceObstacle(controller));
    }

    IEnumerator PlaceObstacle(MonsterController controller)
    {
        //Currently for only 1 tile monster
        //Change square reach to 2 to change this
        SquareAbilityRange squareRange = controller.GetRange<SquareAbilityRange>();
        squareRange.squareReach = 1;

        List<Tile> rangeTiles = squareRange.GetTilesInRange(controller.battleController.board);
        List<Tile> validTiles = new List<Tile>();
        Tile tileToPlaceObstacle = new Tile();

        foreach(Tile t in rangeTiles)
        {
            if(t.content == null)
            {
                validTiles.Add(t);
            }
        }

        if(validTiles != null)
        {
            tileToPlaceObstacle = validTiles[Random.Range(0, validTiles.Count-1)];
        }

        GameObject obstacle = Instantiate(controller.obstacle, new Vector3(tileToPlaceObstacle.pos.x, 1, tileToPlaceObstacle.pos.y), controller.obstacle.transform.rotation);
        tileToPlaceObstacle.content = obstacle;
        obstacle.transform.parent = null;

        controller.currentEnemy.React();

        ActionEffect.instance.Play(3, 0.5f, 0.01f, 0.05f);

        while (ActionEffect.instance.play)
        {
            yield return null;
        }

        controller.currentEnemy.Default();

        OnExit(controller);
    }
}
