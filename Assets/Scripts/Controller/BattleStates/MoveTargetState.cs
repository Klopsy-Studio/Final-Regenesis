using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetState : BattleState
{
    public List<Tile> tiles;

    Tile originPoint;
    int staminaPreview;
    MovementRange range;
    public override void Enter()
    {
        base.Enter();
        owner.ActivateTileSelector();
        owner.tileSelectionToggle.MakeTileSelectionSmall();

        owner.isTimeLineActive = false;

        //range = GetRange<MovementRange>();

        //range.range = owner.currentUnit.weapon.range;
        //range.unit = owner.currentUnit;
        //range.tile = owner.currentUnit.tile;
        //range.removeContent = true;
        //tiles = range.GetTilesInRange(board);

        Movement m = owner.currentUnit.GetComponent<Movement>();

        m.range = owner.currentUnit.weapon.range;
        tiles = m.GetTilesInRange(board, true);
        owner.currentUnit.playerUI.PreviewActionCost(2);
        board.SelectMovementTiles(tiles);
        tiles.Add(owner.currentTile);
        originPoint = owner.currentTile;
        owner.ghostImage.sprite = owner.currentUnit.unitSprite.sprite;

    }

    public override void Exit()
    {
        base.Exit();
        owner.ghostImage.gameObject.SetActive(false);
        board.DeSelectDefaultTiles(tiles);
        tiles = null;
    }

    protected override void OnEscape(object sender, InfoEventArgs<KeyCode> e)
    {
        SelectTile(owner.currentUnit.currentPoint);
        owner.ChangeState<SelectActionState>();
    }

    protected override void OnMouseCancelEvent(object sender, InfoEventArgs<KeyCode> e)
    {
        SelectTile(owner.currentUnit.currentPoint);
        owner.DeactivateTileSelector();
        owner.ChangeState<SelectActionState>();
    }
    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        if(CanReachTile(e.info+pos, tiles))
        {
            SelectTile(e.info + pos);


            //Distance preview, remove next time.
            //if (tiles.Contains(owner.currentTile))
            //{
            //    if (prevDistance <= owner.currentTile.distance)
            //    {
            //        staminaPreview -= owner.currentUnit.weapon.StaminaCost;
            //        owner.unitStatusUI.PreviewStaminaCost(staminaPreview);
            //    }
            //    else
            //    {
            //        staminaPreview += owner.currentUnit.weapon.StaminaCost;
            //        owner.unitStatusUI.PreviewStaminaCost(staminaPreview);
            //    }

            //    prevDistance = owner.currentTile.distance;
            //}
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
                SelectTile(e.info + t.pos);

                if (tiles.Contains(t))
                {
                    owner.ghostImage.gameObject.SetActive(true);
                }
                else
                {
                    owner.ghostImage.gameObject.SetActive(false);
                }
            }
        }

    }

    protected override void OnMouseConfirm(object sender, InfoEventArgs<KeyCode> e)
    {
        if (tiles.Contains(owner.currentTile) && owner.currentTile != originPoint)
        {
            owner.currentUnit.playerUI.SpendActionPoints(2);
            owner.ghostImage.gameObject.SetActive(false);
            owner.currentUnit.didNotMove = false;
            owner.currentUnit.ActionsPerTurn -= 1;
            owner.currentUnit.actionDone = true;
            owner.ChangeState<MoveSequenceState>();
        }
    }

    protected override void OnFire(object sender, InfoEventArgs<KeyCode> e)
    {
        if (tiles.Contains(owner.currentTile) && owner.currentTile != originPoint)
        {
            owner.currentUnit.playerUI.SpendActionPoints(2);

            owner.currentUnit.didNotMove = false;
            owner.currentUnit.ActionsPerTurn -= 1;
            owner.currentUnit.actionDone = true;
            owner.ChangeState<MoveSequenceState>();
        }
    }
}