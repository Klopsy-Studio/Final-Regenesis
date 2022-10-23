using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1Attack : Monster1State
{
    List<Tile> tiles = new List<Tile>();
    public override void Enter()
    {
        stateName = "Attack State";
        base.Enter();
        
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    { 
        switch (attackToUse)
        {
            case 0:
                StartCoroutine(Attack1());
                break;
            case 1:
                StartCoroutine(Attack2());
                break;
            case 2:
                StartCoroutine(Attack3());
                break;
            default:
                break;
        }
        yield return null;
    }
    IEnumerator Attack1()
    {
        tiles.Add(owner.currentTarget.tile);
        battleController.board.SelectAttackTiles(tiles);

        //Use attack
        currentTarget.ReceiveDamage(50);

        owner.currentEnemy.Damage();
        currentTarget.Damage();
        currentTarget.DamageEffect();

        ActionEffect.instance.Play(3, 0.5f, 0.01f, 0.05f);

        while (ActionEffect.instance.play)
        {
            yield return null;
        }

        owner.currentEnemy.Default();
        currentTarget.Default();

        yield return null;
        owner.ChangeState<Monster1CheckNextAction>();

    }

    IEnumerator Attack2()
    {
        SideAbilityRange range = GetRange<SideAbilityRange>();
        //range.dir = currentEnemy.dir;
        tiles = range.GetTilesInRange(battleController.board);
        battleController.board.SelectAttackTiles(tiles);

        yield return new WaitForSeconds(0.5f);
        //Use Attack
        foreach (PlayerUnit target in owner.targets)
        {
            target.ReceiveDamage(40);
            target.Damage();
            target.DamageEffect();
        }

        owner.currentEnemy.Damage();

        

        ActionEffect.instance.Play(3, 0.5f, 0.01f, 0.05f);

        owner.currentEnemy.Default();
        foreach (PlayerUnit target in owner.targets)
        {
            target.Default();
        }

        Movement m = currentEnemy.GetComponent<Movement>();

        switch (currentEnemy.dir)
        {
            case Directions.North:
                m.PushUnit(Directions.South, 1, battleController.board);
                break;
            case Directions.East:
                m.PushUnit(Directions.West, 1, battleController.board);
                break;
            case Directions.South:
                m.PushUnit(Directions.North, 1, battleController.board);
                break;
            case Directions.West:
                m.PushUnit(Directions.East, 1, battleController.board);
                break;
            default:
                break;
        }

        while (m.moving)
        {
            yield return null;
        }


        yield return null;
        owner.ChangeState<Monster1CheckNextAction>();
    }

    IEnumerator Attack3()
    {
        Debug.Log("Attack 3");

        LineAbilityRange range = GetRange<LineAbilityRange>();
        range.unit = owner.currentEnemy;

        range.lineLength = 2;
        range.ChangeDirection(currentEnemy.tile.GetDirections(currentTarget.tile));


        tiles = range.GetTilesInRange(battleController.board);

        battleController.board.SelectAttackTiles(tiles);

        yield return new WaitForSeconds(0.5f);

        currentTarget.ReceiveDamage(50);

        owner.currentEnemy.Damage();
        currentTarget.Damage();
        currentTarget.DamageEffect();

        ActionEffect.instance.Play(3, 0.5f, 0.01f, 0.05f);

        while (ActionEffect.instance.play)
        {
            yield return null;
        }

        owner.currentEnemy.Default();
        currentTarget.Default();



        switch (range.lineDir)
        {
            case Directions.North:
                if(battleController.board.GetTile(currentTarget.tile.pos + new Point(0, 1)) != null && battleController.board.GetTile(currentTarget.tile.pos + new Point(0, 1)).content == null)
                {
                    owner.tileToPlaceObstacle = battleController.board.GetTile(currentTarget.tile.pos + new Point(0, 1));
                }
                break;
            case Directions.East:
                if (battleController.board.GetTile(currentTarget.tile.pos + new Point(1, 0)) != null && battleController.board.GetTile(currentTarget.tile.pos + new Point(1, 0)).content == null)
                {
                    owner.tileToPlaceObstacle = battleController.board.GetTile(currentTarget.tile.pos + new Point(1, 0));
                }
                break;
            case Directions.South:
                if (battleController.board.GetTile(currentTarget.tile.pos + new Point(0, -1)) != null && battleController.board.GetTile(currentTarget.tile.pos + new Point(0, -1)).content == null)
                {
                    owner.tileToPlaceObstacle = battleController.board.GetTile(currentTarget.tile.pos + new Point(0, -1));
                }
                break;
            case Directions.West:
                if (battleController.board.GetTile(currentTarget.tile.pos + new Point(-1, 0)) != null && battleController.board.GetTile(currentTarget.tile.pos + new Point(-1, 0)).content == null)
                {
                    owner.tileToPlaceObstacle = battleController.board.GetTile(currentTarget.tile.pos + new Point(-1, 0));
                }
                break;
            default:
                break;
        }

        owner.ChangeState<Monster1SpecialAbility>();
        yield return null;

    }

    public override void Exit()
    {
        base.Exit();
        battleController.board.DeSelectTiles(tiles);
        owner.currentTarget = null;
        owner.lastAction = EnemyActions.Attack;
        owner.currentEnemy.actionDone = true;
    }
}
