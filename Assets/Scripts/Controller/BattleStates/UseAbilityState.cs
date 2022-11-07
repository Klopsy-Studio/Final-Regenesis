using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbilityState : BattleState
{
    List<Tile> tiles;
    List<Tile> selectTiles = new List<Tile>();

    public Abilities currentAbility;

    bool onMonster;
    //PLACEHOLDER 
    bool attacking;
    public override void Enter()
    {
        base.Enter();
        CleanSelectTiles();
        owner.currentUnit.WeaponOut();
        owner.isTimeLineActive = false;
        owner.ActivateTileSelector();
        currentAbility = owner.currentUnit.weapon.Abilities[owner.attackChosen];

        owner.currentUnit.playerUI.PreviewActionCost(owner.currentUnit.weapon.Abilities[owner.attackChosen].actionCost);
        //tiles = PreviewAbility();

        tiles = PreviewAbility(currentAbility.rangeData);

        if(tiles == null)
        {
            Debug.Log("?");
        }

        board.SelectMovementTiles(tiles);
    }

    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        if (!attacking)
        {
            SelectTile(e.info + pos);
        }
        
    }

    protected override void OnFire(object sender, InfoEventArgs<KeyCode> e)
    {
        if (!attacking)
        {
            if (owner.currentTile.occupied)
            {
                if (owner.currentTile.content.gameObject.GetComponent<EnemyUnit>() != null && tiles.Contains(owner.currentTile))
                {
                    owner.currentUnit.playerUI.HideActionPoints();
                    StartCoroutine(UseAbilitySequence(owner.currentTile.content.gameObject.GetComponent<EnemyUnit>()));
                }
            }
        }

    }

    protected override void OnMouseSelectEvent(object sender, InfoEventArgs<Point> e)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            var a = hit.transform.gameObject;
            var t = a.GetComponent<Tile>();

            if (t != null)
            {
                if (!attacking)
                {
                    if (tiles.Contains(board.GetTile(e.info + t.pos)))
                    {
                        if(t.content != null)
                        {
                            if (t.content.GetComponent<EnemyUnit>())
                            {
                                if (!onMonster)
                                {
                                    SelectMonster(owner.enemyUnits[0].GetComponent<EnemyUnit>(), t);
                                }
                            }
                            else if (t.content.GetComponent<PlayerUnit>())
                            {
                                CleanSelectTiles();
                                SelectTile(e.info + t.pos);
                                owner.tileSelectionToggle.MakeTileSelectionSmall();
                                selectTiles.Add(t);
                                board.SelectAttackTiles(selectTiles);
                                onMonster = false;
                            }
                        }
                        else
                        {
                            if (t.occupied)
                            {
                                if (!onMonster)
                                {
                                    SelectMonster(owner.enemyUnits[0].GetComponent<EnemyUnit>(), t);
                                }
                            }

                            else
                            {
                                CleanSelectTiles();
                                SelectTile(e.info + t.pos);
                                owner.tileSelectionToggle.MakeTileSelectionSmall();
                                onMonster = false;
                            }
                        }
                    }
                   
                }

            }

        }

    }


    public void SelectMonster(EnemyUnit enemy, Tile t)
    {
        CleanSelectTiles();
        selectTiles = enemy.GiveMonsterSpace(board);
        board.SelectAttackTiles(selectTiles);
        SelectTile(enemy.currentPoint);
        owner.tileSelectionToggle.MakeTileSelectionBig();
        onMonster = true;
    }
    
    public void CleanSelectTiles()
    {
        if(selectTiles != null)
        {
            board.DeSelectTiles(selectTiles);
            selectTiles.Clear();
            selectTiles = new List<Tile>();
        }
    }
    protected override void OnMouseConfirm(object sender, InfoEventArgs<KeyCode> e)
    {
        
        if (!attacking && selectTiles !=null)
        {
            if(selectTiles != null)
            {
                foreach (Tile t in selectTiles)
                {
                    if (t.content != null && !attacking)
                    {
                        if (t.content.gameObject.GetComponent<Unit>() != null && selectTiles.Contains(owner.currentTile))
                        {
                            owner.currentUnit.playerUI.unitUI.gameObject.SetActive(false);
                            owner.currentUnit.playerUI.SpendActionPoints(owner.currentUnit.weapon.Abilities[owner.attackChosen].actionCost);
                            currentAbility.SetUnitTimelineVelocityAndActionCost(owner.currentUnit);

                            StartCoroutine(UseAbilitySequence(t.content.GetComponent<Unit>()));
                        }
                    }

                }
            }
            
        }
    }

    protected override void OnEscape(object sender, InfoEventArgs<KeyCode> e)
    {
        //if (!attacking)
        //{
        //    SelectTile(owner.currentUnit.currentPoint);
        //    owner.ChangeState<SelectAbilityState>();
        //}
        
    }

    protected override void OnMouseCancelEvent(object sender, InfoEventArgs<KeyCode> e)
    {
        if (!attacking)
        {
            owner.currentUnit.WeaponIn();
            owner.currentUnit.playerUI.ShowActionPoints();
            owner.DeactivateTileSelector();
            SelectTile(owner.currentUnit.currentPoint);
            owner.ChangeState<SelectAbilityState>();
        }
    }


    IEnumerator UseAbilitySequence(Unit target)
    {
        attacking = true;

        currentAbility.UseAbility(target, owner);
        if (target == owner.currentUnit)
        {
            owner.currentUnit.Default();
        }
        else
        {
            owner.currentUnit.Default();
            target.Default();
        }

        ActionEffect.instance.Play(currentAbility.cameraSize, currentAbility.effectDuration, currentAbility.shakeIntensity, currentAbility.shakeDuration);

        if(currentAbility.inAbilityEffects != null)
        {
            foreach(Effect e in currentAbility.inAbilityEffects)
            {
                switch (e.effectType)
                {
                    case TypeOfEffect.PushUnit:
                        e.PushUnit(target, owner.currentUnit.tile.GetDirections(target.tile), board);
                        break;
                    case TypeOfEffect.FallBack:
                        e.FallBack(owner.currentUnit, owner.currentUnit.tile.GetDirections(target.tile), board);
                        break;
                    case TypeOfEffect.AddStunValue:
                        e.AddStunValue(target, 5);
                        break;
                    default:
                        break;
                }
            }
        }
        if (target == owner.currentUnit)
        {
            AudioManager.instance.Play("HunterAttack");

            owner.currentUnit.Attack();
        }

        else
        {
            AudioManager.instance.Play("HunterAttack");
            owner.currentUnit.Attack();
            target.Damage();       
        }

        while (ActionEffect.instance.play)
        {
            yield return null;
        }

        owner.currentUnit.actionDone = true;

        target.Default();

        foreach (Effect e in currentAbility.postAbilityEffect)
        {
            switch (e.effectType)
            {
                case TypeOfEffect.PushUnit:
                    e.PushUnit(target, owner.currentUnit.tile.GetDirections(target.tile), board);
                    break;
                case TypeOfEffect.FallBack:
                    e.FallBack(owner.currentUnit, owner.currentUnit.tile.GetDirections(target.tile), board);
                    break;
                default:
                    break;
            }
        }
        owner.currentUnit.Default();

        yield return new WaitForSeconds(0.5f);

        if (target.isDead)
        {
            target.gameObject.SetActive(false);

            if(target.GetComponent<EnemyUnit>() != null)
            {
                owner.enemyUnits.Remove(target);

                if (owner.enemyUnits == null)
                {
                    owner.ChangeState<WinState>();
                }
            }

            else if(target.GetComponent<PlayerUnit>() != null)
            {
                owner.playerUnits.Remove(target);

                if(owner.playerUnits == null)
                {
                    //Change to lose state
                }
            }
        }

        else
        {
            yield return null;
            owner.ChangeState<SelectActionState>();
        }


    }
    public override void Exit()
    {
        base.Exit();

        if(tiles != null)
        {
            board.DeSelectDefaultTiles(tiles);
        }

        if(selectTiles != null)
        {
            board.DeSelectDefaultTiles(selectTiles);
        }

        attacking = false;
        tiles = null;
        Movement mover = owner.currentUnit.GetComponent<Movement>();
        mover.ResetRange();

        owner.attackChosen = 0;
    }

    public List<Tile> PreviewAbility(RangeData data)
    {
        switch (data.range)
        {
            case TypeOfAbilityRange.Cone:
                ConeAbilityRange rangeCone = GetRange<ConeAbilityRange>();
                rangeCone.unit = owner.currentUnit;
                return rangeCone.GetTilesInRange(board);

            case TypeOfAbilityRange.Constant:
                ConstantAbilityRange rangeConstant = GetRange<ConstantAbilityRange>();
                rangeConstant.unit = owner.currentUnit;

                return rangeConstant.GetTilesInRange(board);

            case TypeOfAbilityRange.Infinite:
                InfiniteAbilityRange infiniteRange = GetRange<InfiniteAbilityRange>();
                infiniteRange.unit = owner.currentUnit;
                return infiniteRange.GetTilesInRange(board);

            case TypeOfAbilityRange.LineAbility:
                LineAbilityRange lineRange = GetRange<LineAbilityRange>();
                lineRange.AssignVariables(data);
                lineRange.unit = owner.currentUnit;

                return lineRange.GetTilesInRange(board);

            case TypeOfAbilityRange.SelfAbility:
                SelfAbilityRange selfRange = GetRange<SelfAbilityRange>();
                selfRange.unit = owner.currentUnit;
                return selfRange.GetTilesInRange(board);
                
            case TypeOfAbilityRange.SquareAbility:
                SquareAbilityRange squareRange = GetRange<SquareAbilityRange>();
                squareRange.AssignVariables(data);
                squareRange.unit = owner.currentUnit;

                return squareRange.GetTilesInRange(board);

            case TypeOfAbilityRange.Side:

                MovementRange sideRange = GetRange<MovementRange>();
                sideRange.unit = owner.currentUnit;
                sideRange.AssignVariables(data);
                return sideRange.GetTilesInRange(board);

            case TypeOfAbilityRange.Cross:
                CrossAbilityRange crossRange = GetRange<CrossAbilityRange>();
                crossRange.AssignVariables(data);
                crossRange.unit = owner.currentUnit;

                return crossRange.GetTilesInRange(board);

            case TypeOfAbilityRange.Normal:
                MovementRange normalRange = GetRange<MovementRange>();
                normalRange.AssignVariables(data);
                normalRange.unit = owner.currentUnit;
                return normalRange.GetTilesInRange(board);
            default:
                return null;
        }
    }
}
