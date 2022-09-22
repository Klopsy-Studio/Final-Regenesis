using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTimeline : ItemElements
{
    public ItemRange itemRange;
    List<Tile> tiles;


    BattleController battleController;

    [SerializeField] Animator bombAnimator;

    private void Start()
    {

    }
    public void Init(BattleController bController, Tile t)
    {
        battleController = bController;
        bController.timelineElements.Add(this);
        timelineTypes = TimeLineTypes.Items;
        fTimelineVelocity = 70;
        itemRange.removeContent = false;
        itemRange.tile = t;
        tile = t;
        currentPoint = t.pos;
        tiles = itemRange.GetTilesInRange(battleController.board);

        battleController.board.SelectAttackTiles(tiles);
    }


    public override bool UpdateTimeLine()
    {
      
        if (timelineFill >= timelineFull)
        {
            return true;
        }
        timelineFill += fTimelineVelocity * Time.deltaTime;
        return false;
    }

    public override IEnumerator ApplyEffect()
    {

        yield return new WaitForSeconds(1);
        timelineFill = 0;

        foreach (var t in tiles)
        {
            if (t.content == null) continue;
            if(t.content.TryGetComponent(out Unit unit))
            { 
                unit.ReceiveDamage(30);
                unit.React();
                unit.DamageEffect();
            }
        }
        bombAnimator.SetTrigger("explode");

        ActionEffect.instance.Play(3, 0.5f, 0.01f, 0.05f);

        while (ActionEffect.instance.play)
        {
            yield return null;
        }

        foreach (var t in tiles)
        {
            if (t.content == null) continue;
            if (t.content.TryGetComponent(out Unit unit))
            {
                unit.Default();
            }
        }

        battleController.board.DeSelectDefaultTiles(tiles);
        battleController.itemIndexToRemove = battleController.timelineElements.IndexOf(this);
        battleController.timelineElements.Remove(this);
        itemRange.tile.content = null; 
        gameObject.SetActive(false);


    }

    

    public void Place(Tile target)
    {
        // Make sure old tile location is not still pointing to this unit
        if (tile != null && tile.content == gameObject)
            tile.content = null;

        // Link unit and tile references
        tile = target;

        if (target != null)
            target.content = gameObject;
    }

    public void Match()
    {
        transform.localPosition = tile.center;
     
        currentPoint = tile.pos;
    }
}
