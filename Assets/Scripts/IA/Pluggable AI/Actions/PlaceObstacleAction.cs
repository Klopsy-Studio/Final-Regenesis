using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Place Obstacle")]

public class PlaceObstacleAction : Action
{
    [SerializeField] MonsterAbility obstacleRange;
    public override void Act(MonsterController controller)
    {
        controller.CallCoroutine(PlaceObstacle(controller));
    }

    IEnumerator PlaceObstacle(MonsterController controller)
    {
        if(controller.obstaclesInGame.Count >= controller.obstacleLimit)
        {
            AudioManager.instance.Play("MonsterObstacle");

            foreach(Tile t in controller.obstaclesInGame)
            {
                BearObstacleScript o = t.content.GetComponent<BearObstacleScript>();
                List<Tile> tiles = o.Explode(t.pos, controller.battleController.board);

                controller.battleController.SelectTile(t.pos);
                ActionEffect.instance.Play(3, 0.5f, 0.01f, 0.05f);

                while (ActionEffect.instance.play)
                {
                    yield return null;
                }

                while (ActionEffect.instance.recovery)
                {
                    yield return null;
                }

                controller.battleController.board.DeSelectTiles(tiles);

                o.gameObject.SetActive(false);

            }

            OnExit(controller);

        }
        else
        {
            List<Tile> rangeTiles = obstacleRange.GetAttackTiles(controller);

            List<Tile> validTiles = new List<Tile>();
            Tile tileToPlaceObstacle = new Tile();

            foreach (Tile t in rangeTiles)
            {
                if (t.content == null)
                {
                    validTiles.Add(t);
                }
            }

            if (validTiles != null)
            {
                tileToPlaceObstacle = validTiles[Random.Range(0, validTiles.Count - 1)];
            }

            else
            {
                OnExit(controller);
                yield break;
            }

            List<Tile> singleTile = new List<Tile>();
            singleTile.Add(tileToPlaceObstacle);
            controller.battleController.board.SelectAttackTiles(singleTile);

            GameObject obstacle = Instantiate(controller.obstacle, new Vector3(tileToPlaceObstacle.pos.x, 1, tileToPlaceObstacle.pos.y), controller.obstacle.transform.rotation);
            controller.obstaclesInGame.Add(tileToPlaceObstacle);
            tileToPlaceObstacle.content = obstacle;
            obstacle.transform.parent = null;

            controller.currentEnemy.Damage();

            ActionEffect.instance.Play(3, 0.5f, 0.01f, 0.05f);

            while (ActionEffect.instance.play)
            {
                yield return null;
            }

            controller.battleController.board.DeSelectTiles(singleTile);
            controller.currentEnemy.Default();

            OnExit(controller);
        }
        
    }
}
