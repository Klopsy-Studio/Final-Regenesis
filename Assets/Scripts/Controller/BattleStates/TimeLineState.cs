using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLineState : BattleState
{
    [SerializeField] PlayerUnit selectedUnit;

    TimelineElements currentElement;

    float timer = 2f;
    bool timerCheck;
    public override void Enter()
    {
        base.Enter();
        if(currentElement != null)
        {
            if (currentElement.elementEnabled)
            {
                owner.timelineUI.ShowTimelineIcon(currentElement);
                currentElement = null;
            }
        }
        owner.timelineUI.HideIconActing();
        owner.turnStatusUI.DeactivateTurn();
        owner.isTimeLineActive = true;
    }

    private void Update()
    {
        if (owner.isTimeLineActive && !owner.timelineUI.CheckMouse())
        {

            if (selectedUnit != null)
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
                    AudioManager.instance.Play("TurnTransition");

                    currentElement = t;
                    owner.timelineUI.ShowIconActing(t);
                    owner.timelineUI.HideTimelineIcon(t);
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
                        owner.turnStatusUI.ActivateTurn("Item");
                        SelectTile(owner.currentItem.tile.pos);
                        owner.ChangeState<ItemActiveState>();
                        break;
                    }

                    if(t is PlayerUnitDeath r)
                    {
                        SelectTile(r.unit.currentPoint);
                        owner.currentUnit = r.unit;
                        owner.ChangeState<PlayerUnitDeathState>();
                        break;
                    }

                    if(t is MonsterEvent m)
                    {
                        SelectTile(m.controller.currentEnemy.tile.pos);
                        owner.currentMonsterEvent = m;
                        owner.ChangeState<MonsterEventState>();
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

                if(owner.timelineUI.selectedIcon.element.GetComponent<PlayerUnitDeath>()!= null)
                {
                    selectedUnit = owner.timelineUI.selectedIcon.element.GetComponent<PlayerUnitDeath>().unit;
                    selectedUnit.status.ChangeToBig();

                    SelectTile(selectedUnit.currentPoint);

                }

                owner.timelineUI.selectedIcon.Grow();
            }
        }

    }

    protected override void OnMouseSelectEvent(object sender, InfoEventArgs<Point> e)
    {
        
    }


}
