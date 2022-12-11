using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Target : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject targetAssigned;
    public Point targetPosition;
    public AbilityTargetType targetType;
    public BattleController controller;
    public Text targetDisplay;

    public AbilityTargets owner;


    public void OnPointerEnter(PointerEventData eventData)
    {
        owner.selectedTarget = this;
        controller.SelectTile(targetPosition);

        switch (targetType)
        {
            case AbilityTargetType.Enemies:
                controller.tileSelectionToggle.MakeTileSelectionBig();
                break;
            case AbilityTargetType.Allies:
                controller.tileSelectionToggle.MakeTileSelectionSmall();
                break;
            case AbilityTargetType.Obstacles:
                controller.tileSelectionToggle.MakeTileSelectionSmall();
                break;
            default:
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        owner.selectedTarget = null;

        controller.SelectTile(controller.currentUnit.tile.pos);
    }
    public void SetTarget()
    {
        switch (targetType)
        {
            case AbilityTargetType.Enemies:
                Unit u = targetAssigned.GetComponent<Unit>();
                targetDisplay.text = u.unitName;
                targetPosition = u.currentPoint;
                break;
            case AbilityTargetType.Allies:
                Unit e = targetAssigned.GetComponent<Unit>();
                targetDisplay.text = e.unitName;
                targetPosition = e.currentPoint;
                break;
            case AbilityTargetType.Obstacles:
                targetPosition = targetAssigned.GetComponent<BearObstacleScript>().pos;
                targetDisplay.text = "Monster Obstacle";
                break;
            default:
                break;
        }
    }
}
