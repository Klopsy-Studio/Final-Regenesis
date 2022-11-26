using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemState : BattleState
{
    public List<Tile> tiles;
    public List<Tile> selectTiles;
    Consumables currentItem;

    ItemRange range;
    
    bool isTimelineItem = false;

    bool itemUsed;
    public override void Enter()
    {
        base.Enter();
        itemUsed = false;
        owner.isTimeLineActive = false;
        owner.actionSelectionUI.gameObject.SetActive(false);

        currentItem = owner.backpackInventory.consumableContainer[owner.itemChosen].consumable;

        owner.currentUnit.playerUI.PreviewActionCost(2);

        if(currentItem.ConsumableType == ConsumableType.NormalConsumable)
        {
            StartCoroutine(Init());
        }
        else if (currentItem.ConsumableType == ConsumableType.TimelineConsumable)
        {
            isTimelineItem = true;

            tiles = GetRangeOnItems(currentItem.itemRange);
            board.SelectMovementTiles(tiles);
            owner.ghostImage.sprite = currentItem.sprite;
        }
       
    }

    public List<Tile> GetRangeOnItems(RangeData data)
    {
        switch (data.range)
        {
            case TypeOfAbilityRange.Cone:
                return null;
            case TypeOfAbilityRange.Constant:
                return null;
            case TypeOfAbilityRange.Infinite:
                return null;
            case TypeOfAbilityRange.LineAbility:
                LineAbilityRange line = GetRange<LineAbilityRange>();
                line.AssignVariables(data);
                return line.GetTilesInRange(board);
            case TypeOfAbilityRange.SelfAbility:
                return null;
            case TypeOfAbilityRange.SquareAbility:
                SquareAbilityRange square = GetRange<SquareAbilityRange>();
                square.AssignVariables(data);
                return square.GetTilesInRangeWithoutUnit(board, owner.currentTile.pos);
            case TypeOfAbilityRange.Side:
                SideAbilityRange side = GetRange<SideAbilityRange>();
                side.AssignVariables(data);
                return side.GetTilesInRange(board);
            case TypeOfAbilityRange.AlternateSide:
                AlternateSideRange altSide = GetRange<AlternateSideRange>();
                altSide.AssignVariables(data);
                return altSide.GetTilesInRange(board);
            case TypeOfAbilityRange.Cross:
                CrossAbilityRange cross = GetRange<CrossAbilityRange>();
                cross.AssignVariables(data);
                return cross.GetTilesInRange(board);
            case TypeOfAbilityRange.Normal:
                return null;
            case TypeOfAbilityRange.Item:
                ItemRange item = GetRange<ItemRange>();
                item.AssignVariables(data);
                item.tile = owner.currentTile;
                return item.GetTilesInRange(board);
            default:
                return null;
        }
    }
    IEnumerator Init()
    {
        owner.backpackInventory.UseConsumable(owner.itemChosen, targetUnit: owner.currentUnit);
        owner.currentUnit.playerUI.gameObject.SetActive(false);

        ActionEffect.instance.Play(3, 0.5f, 0.01f, 0.05f);
        
        while(ActionEffect.instance.play || ActionEffect.instance.recovery)
        {
            yield return null;
        }

        owner.currentUnit.playerUI.SpendActionPoints(2);
        owner.currentUnit.ActionsPerTurn -= 2;

        owner.ChangeState<FinishPlayerUnitTurnState>();
    }

    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        if (!isTimelineItem) return;
        SelectTile(e.info + pos);
    }

    protected override void OnFire(object sender, InfoEventArgs<KeyCode> e)
    {
        if (!isTimelineItem) return;

        if (owner.currentTile.content == null)
        {
            owner.backpackInventory.UseConsumable(owner.itemChosen, tileSpawn: owner.currentTile);
            itemUsed = true;
            owner.ChangeState<FinishPlayerUnitTurnState>();
        }
    }

    protected override void OnMouseSelectEvent(object sender, InfoEventArgs<Point> e)
    {
        if (!isTimelineItem || itemUsed) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            var a = hit.transform.gameObject;
            var t = a.GetComponent<Tile>();
            if (t != null)
            {
                if (tiles.Contains(t))
                {
                    SelectTile(e.info + t.pos);
                    owner.ActivateTileSelector();
                    if (selectTiles != null)
                    {
                        board.DeSelectTiles(selectTiles);
                        selectTiles.Clear();
                    }
                    owner.ghostImage.gameObject.SetActive(true);
                    selectTiles = GetRangeOnItems(currentItem.effectRange);
                    board.SelectAttackTiles(selectTiles);
                }

                else
                {
                    if (selectTiles != null)
                    {
                        board.DeSelectTiles(selectTiles);
                        selectTiles.Clear();
                    }

                    owner.DeactivateTileSelector();
                }
            }

        }
    }

    protected override void OnMouseConfirm(object sender, InfoEventArgs<KeyCode> e)
    {
        if (!isTimelineItem || itemUsed) return;

        if (owner.currentTile.content == null && tiles.Contains(owner.currentTile))
        {
            StartCoroutine(UseItemSpace());
        }
    }
    IEnumerator UseItemSpace()
    {
        itemUsed = true;

        owner.backpackInventory.UseConsumable(owner.itemChosen, tileSpawn: owner.currentTile, battleController: owner);
        //Unit item pose
        ActionEffect.instance.Play(3, 0.5f, 0.01f, 0.05f);

        while (ActionEffect.instance.play || ActionEffect.instance.recovery)
        {
            yield return null;
        }
        owner.currentUnit.playerUI.SpendActionPoints(2);
        owner.currentUnit.ActionsPerTurn -= 2;

        owner.ChangeState<SelectActionState>();
    }
    protected override void OnEscape(object sender, InfoEventArgs<KeyCode> e)
    {
        if (!isTimelineItem) return;
        SelectTile(owner.currentUnit.currentPoint);
        owner.ChangeState<SelectItemState>();

    }
    protected override void OnMouseCancelEvent(object sender, InfoEventArgs<KeyCode> e)
    {
        if (!isTimelineItem) return;
        SelectTile(owner.currentUnit.currentPoint);
        owner.ChangeState<SelectItemState>();

    }


    public override void Exit()
    {
        base.Exit();
        owner.itemChosen = 0;

        if (tiles != null)
        {
            List<Tile> trash = new List<Tile>(tiles);

            foreach (Tile t in trash)
            {
                if (t.selected)
                {
                    tiles.Remove(t);
                }
            }

            board.DeSelectDefaultTiles(tiles);
        }

        if(selectTiles != null)
        {
            board.DeSelectDefaultTiles(selectTiles);
            selectTiles = null;
        }

        tiles = null;

        
        owner.ghostImage.gameObject.SetActive(false);

    }
}
