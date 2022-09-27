using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbilityState : BattleState
{
    List<Tile> tiles;
    List<Tile> selectTiles;
    public Abilities currentAbility;

    //PLACEHOLDER 
    bool attacking;
    public override void Enter()
    {
        base.Enter();
        owner.isTimeLineActive = false;

        Movement mover = owner.currentUnit.GetComponent<Movement>();
        currentAbility = owner.currentUnit.weapon.Abilities[owner.attackChosen];
        currentAbility.SetUnitTimelineVelocityAndActionCost(owner.currentUnit);

        tiles = PreviewAbility();
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
            if (owner.currentTile.content != null)
            {
                if (owner.currentTile.content.gameObject.GetComponent<EnemyUnit>() != null && tiles.Contains(owner.currentTile))
                {
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
                    SelectTile(e.info + t.pos);

                    if (tiles.Contains(owner.currentTile))
                    {
                        if (selectTiles != null)
                        {
                            board.DeSelectTiles(selectTiles);
                            selectTiles.Clear();
                        }
                        else
                        {
                            selectTiles = new List<Tile>();
                        }

                        switch (currentAbility.rangeType)
                        {
                            case TypeOfAbilityRange.Cone:

                                selectTiles.Add(owner.currentTile);
                                board.SelectAttackTiles(selectTiles);
                                break;
                            case TypeOfAbilityRange.Constant:
                                selectTiles.Add(owner.currentTile);
                                board.SelectAttackTiles(selectTiles);
                                break;
                            case TypeOfAbilityRange.Infinite:
                                selectTiles.Add(owner.currentTile);
                                board.SelectAttackTiles(selectTiles);
                                break;
                            case TypeOfAbilityRange.LineAbility:
                                selectTiles.Add(owner.currentTile);
                                board.SelectAttackTiles(selectTiles);
                                break;
                            case TypeOfAbilityRange.SelfAbility:
                                selectTiles.Add(owner.currentTile);
                                board.SelectAttackTiles(selectTiles);
                                break;
                            case TypeOfAbilityRange.SquareAbility:
                                selectTiles.Add(owner.currentTile);
                                board.SelectAttackTiles(selectTiles);
                                break;
                            case TypeOfAbilityRange.Side:
                                SideAbilityRange sideRange = GetRange<SideAbilityRange>();
                                sideRange.unit = owner.currentUnit;
                                sideRange.ChangeDirections(owner.currentTile);

                                selectTiles = sideRange.GetTilesInRange(board);
                                board.SelectAttackTiles(selectTiles);

                                break;
                            case TypeOfAbilityRange.Cross:
                                LineAbilityRange lineRange = GetRange<LineAbilityRange>();
                                lineRange.ChangeDirection(owner.currentUnit.tile.GetDirections(owner.currentTile));
                                lineRange.lineLength = currentAbility.range;
                                lineRange.unit = owner.currentUnit;

                                selectTiles = lineRange.GetTilesInRange(board);
                                board.SelectAttackTiles(selectTiles);
                                break;

                            case TypeOfAbilityRange.Normal:
                                selectTiles.Add(owner.currentTile);

                                board.SelectAttackTiles(selectTiles);
                                break;

                            default:
                                break;
                        }
                    }

                    else
                    {
                        if (selectTiles != null)
                        {
                            board.DeSelectTiles(selectTiles);
                            selectTiles.Clear();
                        }
                    }

                    if (owner.currentTile.content != null)
                    {
                        if (owner.currentTile.content.GetComponent<EnemyUnit>() != null)
                        {
                            //EnemyUnit target = owner.currentTile.content.GetComponent<EnemyUnit>();

                            //Unit status code, remove when new UI
                            //owner.unitStatusTargetUI.gameObject.SetActive(true);
                            //owner.unitStatusTargetUI.UpdateUnit(target);


                            //owner.unitStatusTargetUI.PreviewHealth((int)target.health.Value - currentAbility.damage);
                        }
                        //else
                        //{
                        //    if (owner.unitStatusTargetUI.gameObject.activeSelf)
                        //    {
                        //        owner.unitStatusTargetUI.gameObject.SetActive(false);
                        //    }
                        //}
                    }
                    //else
                    //{
                    //    if (owner.unitStatusTargetUI.gameObject.activeSelf)
                    //    {
                    //        owner.unitStatusTargetUI.gameObject.SetActive(false);
                    //    }
                    //}
                }

            }

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
                    if (t.content != null)
                    {
                        if (t.content.gameObject.GetComponent<Unit>() != null && selectTiles.Contains(owner.currentTile))
                        {
                            StartCoroutine(UseAbilitySequence(t.content.GetComponent<Unit>()));
                        }
                    }

                }
            }
            
        }
    }

    protected override void OnEscape(object sender, InfoEventArgs<KeyCode> e)
    {
        if (!attacking)
        {
            SelectTile(owner.currentUnit.currentPoint);
            owner.ChangeState<SelectAbilityState>();
        }
        
    }

    protected override void OnMouseCancelEvent(object sender, InfoEventArgs<KeyCode> e)
    {
        if (!attacking)
        {
            SelectTile(owner.currentUnit.currentPoint);
            owner.ChangeState<SelectAbilityState>();
        }
    }


    IEnumerator UseAbilitySequence(Unit target)
    {
        attacking = true;

        currentAbility.UseAbility(target);

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

        if (target == owner.currentUnit)
        {
            owner.currentUnit.Attack();
        }

        else
        {
            owner.currentUnit.Attack();
            target.React();       
        }

        while (ActionEffect.instance.play)
        {
            yield return null;
        }

        owner.currentUnit.actionDone = true;

        target.Default();
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


    public List<Tile> PreviewAbility()
    {
        switch (currentAbility.rangeType)
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
                lineRange.lineLength = currentAbility.range;
                lineRange.unit = owner.currentUnit;
                lineRange.ChangeDirection(owner.currentUnit.tile.GetDirections(owner.currentTile));

                return lineRange.GetTilesInRange(board);

            case TypeOfAbilityRange.SelfAbility:
                SelfAbilityRange selfRange = GetRange<SelfAbilityRange>();
                selfRange.unit = owner.currentUnit;
                return selfRange.GetTilesInRange(board);

                
            case TypeOfAbilityRange.SquareAbility:
                SquareAbilityRange squareRange = GetRange<SquareAbilityRange>();
                squareRange.unit = owner.currentUnit;

                return squareRange.GetTilesInRange(board);

            case TypeOfAbilityRange.Side:

                MovementRange sideRange = GetRange<MovementRange>();
                sideRange.unit = owner.currentUnit;
                sideRange.tile = owner.currentUnit.tile;
                sideRange.range = 1;
                return sideRange.GetTilesInRange(board);

            case TypeOfAbilityRange.Cross:
                CrossAbilityRange crossRange = GetRange<CrossAbilityRange>();
                crossRange.crossLength = 1;
                crossRange.unit = owner.currentUnit;

                return crossRange.GetTilesInRange(board);

            case TypeOfAbilityRange.Normal:
                MovementRange normalRange = GetRange<MovementRange>();
                normalRange.tile = owner.currentUnit.tile;
                normalRange.range = currentAbility.range;
                normalRange.unit = owner.currentUnit;
                normalRange.removeContent = false;

                return normalRange.GetTilesInRange(board);
            default:
                return null;
        }
    }
}
