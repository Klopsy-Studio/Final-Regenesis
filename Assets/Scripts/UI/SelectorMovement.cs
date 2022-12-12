using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum TabType
{
    Ability, Item, Action
};
public class SelectorMovement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Color hoverColor;
    [SerializeField] Color normalColor;


    [SerializeField] OptionSelection optionSelection;
    [SerializeField] Text textButton;

    [SerializeField] int selection;

    public AbilityDescription abilityDescription;
    public Abilities assignedAbility;
    public BattleController controller;

    public bool canBeSelected;

    List<Tile> abilityPreviewTiles = new List<Tile>();

    bool monsterSelected;

    List<SpriteRenderer> targets = new List<SpriteRenderer>();

    [SerializeField] TabType typeOfOption;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (canBeSelected)
        {
            if(abilityDescription!= null)
            {
                abilityDescription.gameObject.SetActive(true);

                switch (typeOfOption)
                {
                    case TabType.Ability:
                        controller.board.SelectAbilityTiles(abilityPreviewTiles);

                        controller.currentUnit.playerUI.PreviewActionCost(assignedAbility.actionCost);


                        if (targets != null)
                        {
                            if (targets.Count > 0)
                            {
                                foreach (SpriteRenderer target in targets)
                                {
                                    target.color = new Color(target.color.r, target.color.g, target.color.b, target.color.a + 0.5f);
                                }
                            }
                        }
                        break;
                    case TabType.Item:
                        //Yet to be implemented 
                        break;
                    case TabType.Action:
                        //Yet to be implemented
                        break;
                    default:
                        break;
                }
                
            }
            optionSelection.MouseOverEnter(this);
            optionSelection.currentSelection = selection;

            textButton.color = hoverColor;
        }
    }
  
    public void OnPointerExit(PointerEventData eventData)
    {
        if (canBeSelected)
        {
            if(abilityDescription != null)
            {

                abilityDescription.gameObject.SetActive(false);

                switch (typeOfOption)
                {
                    case TabType.Ability:

                        if (abilityPreviewTiles != null)
                        {
                            controller.board.DeSelectTiles(abilityPreviewTiles);
                        }

                        controller.currentUnit.playerUI.ShowActionPoints();

                        if (targets != null)
                        {
                            if (targets.Count > 0)
                            {
                                foreach (SpriteRenderer target in targets)
                                {
                                    target.color = new Color(target.color.r, target.color.g, target.color.b, target.color.a - 0.5f);
                                }
                            }
                        }
                        break;
                    case TabType.Item:
                        //Yet to be implemented 
                        break;
                    case TabType.Action:
                        //Yet to be implemented
                        break;
                    default:
                        break;
                }
              
            }

            optionSelection.MouseOverExit(this);

            textButton.color = normalColor;
        }
    }


    public void ChangeToDefault()
    {
        textButton.color = normalColor;
    }

    public void ClearOption()
    {
        if(targets != null)
        {
            targets.Clear();
        }
        if(abilityPreviewTiles != null)
        {
            controller.board.DeSelectDefaultTiles(abilityPreviewTiles);
            abilityPreviewTiles.Clear();
        }

        if(abilityDescription != null)
        {
            abilityDescription.gameObject.SetActive(false);
        }
    }
    public void GetPreviewRange()
    {
        AbilityRange range = assignedAbility.rangeData.GetOrCreateRange(assignedAbility.rangeData.range, controller.currentUnit.gameObject);
        range.unit = controller.currentUnit;

        abilityPreviewTiles = range.GetTilesInRange(controller.board);
    }
    public void GetTargets()
    {
        foreach (AbilityTargetType a in assignedAbility.elementsToTarget)
        {
            switch (a)
            {
                case AbilityTargetType.Enemies:
                    if (!monsterSelected)
                    {
                        foreach (Tile t in abilityPreviewTiles)
                        {
                            if (t.occupied)
                            {
                                targets.Add(controller.enemyUnits[0].unitSprite);
                                monsterSelected = true;
                            }
                        }
                    }
                    break;
                case AbilityTargetType.Allies:

                    foreach (Tile t in abilityPreviewTiles)
                    {
                        if (t.content != null)
                        {
                            if (t.content.GetComponent<PlayerUnit>() != null)
                            {
                                targets.Add(t.content.GetComponent<PlayerUnit>().unitSprite);
                            }
                        }
                    }
                    break;
                case AbilityTargetType.Obstacles:
                    foreach (Tile t in abilityPreviewTiles)
                    {
                        if (t.content != null)
                        {
                            if (t.content.GetComponent<BearObstacleScript>() != null)
                            {
                                targets.Add(t.content.GetComponent<SpriteRenderer>());
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

}
