using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineState : BattleState
{
    [SerializeField] PlayerUnit selectedUnit;
    public override void Enter()
    {
        base.Enter();
        owner.turnStatusUI.Disappear();
    }

    private void Update()
    {

        if (owner.isTimeLineActive && !owner.timelineUI.CheckMouse())
        {
            if(selectedUnit != null)
            {
                selectedUnit.status.ChangeToSmall();
                selectedUnit = null;
            }
            //Timeline del evento real
            //owner.realTimeEvent.UpdateTimeLine();

            foreach (var t in owner.timelineElements)
            {
                if (t == null) { return; }
                //if (owner.IsInMenu()) continue;
                
                bool isTimeline = t.UpdateTimeLine();
                if (isTimeline)
                {
                    if (t is PlayerUnit p)
                    {
                        owner.currentUnit = p;
                        owner.currentUnit.playerUI.ResetActionPoints();
                        owner.ChangeState<SelectUnitState>();
                        break;
                    }

                    if (t is EnemyUnit e)
                    {

                        owner.currentEnemyUnit = e;
                        SelectTile(owner.currentEnemyUnit.tile.pos);
                        //owner.currentEnemyController = e.GetComponent<EnemyController>();
                        //owner.currentEnemyController.battleController = owner;

                        owner.monsterController = e.GetComponent<MonsterController>();
                        owner.monsterController.battleController = owner;
                        owner.ChangeState<StartEnemyTurnState>();
                        break;
                    }

                    if (t is RealTimeEvents)
                    {
                        owner.ChangeState<EventActiveState>();
                        break;
                    }

                    if (t is ItemElements w)
                    {
                        owner.currentItem = w;
                        owner.turnStatusUI.EventTurn();
                        SelectTile(owner.currentItem.tile.pos);
                        owner.ChangeState<ItemActiveState>();
                        break;
                    }
                    

                }
               
            }
        }

        else
        {
            if (owner.timelineUI.selectedIcon != null)
            {
                if (owner.timelineUI.selectedIcon.element.GetComponent<Unit>() != null)
                {
                    if (owner.timelineUI.selectedIcon.element.GetComponent<PlayerUnit>() != null)
                    {
                        selectedUnit = owner.timelineUI.selectedIcon.element.GetComponent<PlayerUnit>();
                        selectedUnit.status.ChangeToBig();
                    }
                    SelectTile(owner.timelineUI.selectedIcon.element.GetComponent<Unit>().tile.pos);
                }

                owner.timelineUI.selectedIcon.Grow();
            }
        }

    }

    protected override void OnMouseSelectEvent(object sender, InfoEventArgs<Point> e)
    {
        
    }


}
