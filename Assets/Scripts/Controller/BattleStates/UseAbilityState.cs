using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbilityState : BattleState
{
    List<Tile> tiles;
    List<Tile> targetTiles = new List<Tile>();
    List<Tile> selectTiles = new List<Tile>();

    public Abilities currentAbility;

    bool onMonster;
    //PLACEHOLDER 
    bool attacking;
    bool selected;

    bool test;
    public override void Enter()
    {
        base.Enter();
        CleanSelectTiles();
        owner.currentUnit.WeaponOut();
        owner.isTimeLineActive = false;
        owner.ActivateTileSelector();
        currentAbility = owner.currentUnit.weapon.Abilities[owner.attackChosen];

        owner.currentUnit.playerUI.PreviewActionCost(currentAbility.actionCost);
        //tiles = PreviewAbility();
        tiles = new List<Tile>();

        foreach(RangeData r in currentAbility.abilityRange)
        {
            List<Tile> dirtyTiles = PreviewAbility(r);
            foreach(Tile t in dirtyTiles)
            {
                if (!tiles.Contains(t))
                {
                    tiles.Add(t);
                }
            }
        }

        board.SelectAbilityTiles(tiles);

        switch (owner.currentUnit.weapon.EquipmentType)
        {
            case KitType.Hammer:
                break;
            case KitType.Bow:
                owner.bowExtraAttackObject.SetActive(true);
                break;
            case KitType.Gunblade:
                break;
            default:
                break;
        }

        foreach(AbilityTargetType target in currentAbility.elementsToTarget)
        {
            switch (target)
            {
                case AbilityTargetType.Enemies:
                    foreach(Tile t in tiles)
                    {
                        if (t.occupied)
                        {
                            targetTiles.Add(t);
                        }
                    }
                    break;
                case AbilityTargetType.Allies:
                    foreach (Tile t in tiles)
                    {
                        if(t.content != null)
                        {
                            if (t.content.GetComponent<PlayerUnit>() != null)
                            {
                                targetTiles.Add(t);
                            }
                        }
                    }
                    break;
                case AbilityTargetType.Obstacles:
                    foreach (Tile t in tiles)
                    {
                        if(t.content != null)
                        {
                            if (t.content.GetComponent<BearObstacleScript>() != null)
                            {
                                targetTiles.Add(t);
                            }
                        }
                    }
                    break;
                case AbilityTargetType.Self:
                    targetTiles.Add(owner.currentUnit.tile);
                    break;
                default:
                    break;
            }
        }

        owner.targets.gameObject.SetActive(true);

        if(targetTiles != null || targetTiles.Count > 0)
        {
            owner.targets.CreateTargets(targetTiles);
        }
        
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
                    //StartCoroutine(UseAbilitySequence(owner.currentTile.content.gameObject.GetComponent<EnemyUnit>()));
                }
            }
        }

    }

    public void ExtraAttackBow()
    {
        if(currentAbility.actionCost+1<= owner.currentUnit.actionsPerTurn)
        {
            owner.SetBowExtraAttack();

            if (owner.bowExtraAttack)
            {
                owner.currentUnit.playerUI.PreviewActionCost(currentAbility.actionCost + 1);
            }
            else
            {
                owner.currentUnit.playerUI.ShowActionPoints();
                owner.currentUnit.playerUI.PreviewActionCost(currentAbility.actionCost);
            }
        }
    }
    //protected override void OnMouseSelectEvent(object sender, InfoEventArgs<Point> e)
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit, 100))
    //    {
    //        var a = hit.transform.gameObject;
    //        var t = a.GetComponent<Tile>();

    //        if (t != null)
    //        {
    //            if (!attacking)
    //            {
    //                if(targetTiles != null)
    //                {
    //                    if (targetTiles.Contains(board.GetTile(e.info + t.pos)))
    //                    {
    //                        if (t.content != null)
    //                        {
    //                            if (t.content.GetComponent<EnemyUnit>())
    //                            {
    //                                if (!onMonster)
    //                                {
    //                                    SelectMonster(owner.enemyUnits[0].GetComponent<EnemyUnit>(), t);
    //                                }
    //                            }
    //                            else if (t.content.GetComponent<PlayerUnit>())
    //                            {
    //                                CleanSelectTiles();
    //                                SelectTile(e.info + t.pos);
    //                                owner.tileSelectionToggle.MakeTileSelectionSmall();
    //                                selectTiles.Add(t);
    //                                board.SelectAttackTiles(selectTiles);
    //                                onMonster = false;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            if (t.occupied)
    //                            {
    //                                if (!onMonster)
    //                                {
    //                                    SelectMonster(owner.enemyUnits[0].GetComponent<EnemyUnit>(), t);
    //                                }
    //                            }

    //                            else
    //                            {
    //                                CleanSelectTiles();
    //                                SelectTile(e.info + t.pos);
    //                                owner.tileSelectionToggle.MakeTileSelectionSmall();
    //                                onMonster = false;
    //                            }
    //                        }
    //                    }
    //                } 
    //            }
    //        }
    //    }
    //}


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
        if (!test)
        {
            test = true;
            return;
        }
        if (!attacking)
        {
            if(owner.targets.selectedTarget != null)
            {
                owner.currentUnit.playerUI.HideActionPoints();

                GameObject objectTarget = owner.targets.selectedTarget.targetAssigned;

                board.DeSelectTiles(tiles);
                owner.bowExtraAttackObject.SetActive(false);
                StartCoroutine(UseAbilitySequence(objectTarget));
                owner.ResetUnits();
                owner.targets.gameObject.SetActive(false);
                owner.targets.stopSelection = true;
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
            owner.currentUnit.animations.SetIdle();
            owner.currentUnit.playerUI.ShowActionPoints();
            owner.DeactivateTileSelector();
            SelectTile(owner.currentUnit.currentPoint);
            owner.ChangeState<SelectAbilityState>();
        }
    }

    IEnumerator UseAbilitySequence(GameObject target)
    {
        attacking = true;

        StartCoroutine(currentAbility.sequence.Sequence(target, owner));

        while (currentAbility.sequence.playing)
        {
            yield return null;
        }

        if (!owner.endTurnInstantly)
        {
            owner.currentUnit.animations.SetIdle();
            owner.ChangeState<SelectActionState>();
        }

        else
        {
            owner.endTurnInstantly = false;
            owner.ChangeState<FinishPlayerUnitTurnState>();
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

        targetTiles.Clear();
        owner.targets.ClearTargets();
        owner.ResetUnits();
        owner.targets.gameObject.SetActive(false);
        owner.targets.stopSelection = false;

        //For the first click inside the state
        test = false;

        //Bow variables
        owner.bowExtraAttackObject.SetActive(false);
        owner.bowExtraAttack = false;
        owner.ResetBowExtraAttack();
    }

    public List<Tile> PreviewAbility(RangeData data)
    {
        List<Tile> t = new List<Tile>();
        AbilityRange range = data.GetOrCreateRange(data.range,owner.currentUnit.gameObject);
        range.unit = owner.currentUnit;

        t = range.GetTilesInRange(board);
        return t;

    }
   
}
