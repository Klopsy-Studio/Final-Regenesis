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
     
        if(currentItem.ConsumableType == ConsumableType.NormalConsumable)
        {
            StartCoroutine(Init());
        }
        else if (currentItem.ConsumableType == ConsumableType.TimelineConsumable)
        {
            isTimelineItem = true;
            if(currentItem is Bomb b)
            {
                //itemChosen.SetUnitTimelineVelocity(owner.currentUnit);
                range = GetRange<ItemRange>();
                range.range = b.range;
                range.tile = owner.currentUnit.tile;
                tiles = range.GetTilesInRange(board);

                board.SelectMovementTiles(tiles);

                foreach(Tile t in tiles)
                {
                    t.selected = false;
                }
                range.removeContent = false;
                owner.ghostImage.sprite = currentItem.sprite;

                
            }          
        }
       
    }

    IEnumerator Init()
    {
        owner.backpackInventory.UseConsumable(owner.itemChosen, targetUnit: owner.currentUnit);
        yield return null;
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
        if (!isTimelineItem) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            var a = hit.transform.gameObject;
            var t = a.GetComponent<Tile>();
            if (t != null)
            {
                SelectTile(e.info + t.pos);

                if (tiles.Contains(t))
                {
                    if (selectTiles != null)
                    {
                        board.DeSelectTiles(selectTiles);
                        selectTiles.Clear();
                    }
                    owner.ghostImage.gameObject.SetActive(true);
                    range.tile = owner.currentTile;
                    selectTiles = range.GetTilesInRange(board);
                    board.SelectAttackTiles(selectTiles);
                }

                else
                {
                    if (selectTiles != null)
                    {
                        board.DeSelectTiles(selectTiles);
                        selectTiles.Clear();
                    }

                    owner.ghostImage.gameObject.SetActive(false);


                }
            }

        }
    }

    protected override void OnMouseConfirm(object sender, InfoEventArgs<KeyCode> e)
    {

        if (!isTimelineItem) return;

        if (owner.currentTile.content == null && tiles.Contains(owner.currentTile))
        {
            owner.backpackInventory.UseConsumable(owner.itemChosen, tileSpawn: owner.currentTile, battleController: owner);
            itemUsed = true;
            owner.ChangeState<FinishPlayerUnitTurnState>();
        }
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

        if(selectTiles != null && !itemUsed)
        {
            board.DeSelectDefaultTiles(selectTiles);
            selectTiles = null;
        }

        tiles = null;

        owner.ghostImage.gameObject.SetActive(false);

    }
}
