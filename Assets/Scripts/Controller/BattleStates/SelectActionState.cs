using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typeOfAction
{
    Move,
    Ability,
    Item,
    Wait,
};
public class SelectActionState : BattleState
{
    public typeOfAction currentAction = typeOfAction.Move;
    public override void Enter()
    {
        base.Enter();

        owner.currentUnit.unitSprite.gameObject.GetComponent<Renderer>().material.SetFloat("_OutlineThickness", 1);
        owner.isTimeLineActive = false;
        owner.moveActionSelector = true;
        owner.actionSelectionUI.gameObject.SetActive(true);
        owner.currentUnit.GetComponent<Movement>().ResetRange();
      
        if (!owner.currentUnit.CanMove())
        {
            ActionSelectionUI.DisableSelectOption(typeOfAction.Move);
        }
        else
        {
            ActionSelectionUI.EnableSelectOption(typeOfAction.Move);
           
        }


        if (!owner.currentUnit.CanDoAbility())
        {
            ActionSelectionUI.DisableSelectOption(typeOfAction.Ability);
        }
        else
        {
            ActionSelectionUI.EnableSelectOption(typeOfAction.Ability);
        }
    }

    public override void Exit()
    {
        base.Exit();
        owner.currentUnit.unitSprite.gameObject.GetComponent<Renderer>().material.SetFloat("_OutlineThickness", 0);
        owner.moveActionSelector = false;
    }

    protected override void OnEscape(object sender, InfoEventArgs<KeyCode> e)
    {
        owner.currentUnit = null;
        ActionSelectionUI.ResetSelector();
        ActionSelectionUI.gameObject.SetActive(false);
        currentAction = typeOfAction.Move;
        owner.ChangeState<SelectUnitState>();
    }

    

   

    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        if(e.info.y >= 1)
        {
            ActionSelectionUI.MoveBackwards();

            switch (currentAction)
            {
                case typeOfAction.Move:
                    currentAction = typeOfAction.Wait;
                    break;
                case typeOfAction.Ability:
                    currentAction = typeOfAction.Move;
                    break;
                case typeOfAction.Item:
                    currentAction = typeOfAction.Ability;
                    break;
                case typeOfAction.Wait:
                    currentAction = typeOfAction.Item;
                    break;
                default:
                    break;
            }
        }

        if(e.info.y <= -1)
        {
            ActionSelectionUI.MoveForward();
            switch (currentAction)
            {
                case typeOfAction.Move:
                    currentAction = typeOfAction.Ability;
                    break;
                case typeOfAction.Ability:
                    currentAction = typeOfAction.Item;
                    break;
                case typeOfAction.Item:
                    currentAction = typeOfAction.Wait;
                    break;
                case typeOfAction.Wait:
                    currentAction = typeOfAction.Move;
                    break;
                default:
                    break;
            }
        }

        
    }


    protected override void OnSelectAction(object sender, InfoEventArgs<int> e)
    {
       
        switch (e.info)
        {
            case 0:
                if (owner.currentUnit.CanMove())
                {
                    Debug.Log("CASE 0");
                    ActionSelectionUI.gameObject.SetActive(false);
                    owner.ChangeState<MoveTargetState>();
                }

                //owner.currentUnit.GetComponent<Movement>().PushUnit(Directions.South, 3, board);
                break;

            case 1:
                if (owner.currentUnit.CanDoAbility())
                {
                    Debug.Log("CASE 1");
                    owner.ChangeState<SelectAbilityState>();
                }
                //OpenAbilityMenu
                break;

            case 2:
                Debug.Log("CASE 2");
                //right now it will change to SelectItemState. That state will select the potion item automatically. 
                //we should change that in the future
                owner.ChangeState<SelectItemState>();
                //OpenItemMenu
                break;

            case 3:
                //Recover stamina and end turn
                if (!owner.currentUnit.actionDone)
                {
                    currentAction = typeOfAction.Move;
                    owner.ChangeState<RestUnitState>();
                }
                break;
            case 4:
                //Skip turn

                owner.ChangeState<WaitUnitState>();
                break;

        }
    }
    protected override void OnFire(object sender, InfoEventArgs<KeyCode> e)
    {
        
        switch (currentAction)
        {
            case typeOfAction.Move:
                if (owner.currentUnit.CanMove())
                {
                    ActionSelectionUI.gameObject.SetActive(false);
                    owner.ChangeState<MoveTargetState>();
                }

                //owner.currentUnit.GetComponent<Movement>().PushUnit(Directions.South, 3, board);
                break;

            case typeOfAction.Ability:
                if (owner.currentUnit.CanDoAbility())
                {
                    owner.ChangeState<SelectAbilityState>();
                }
                //OpenAbilityMenu
                break;

            case typeOfAction.Item:
                //right now it will change to SelectItemState. That state will select the potion item automatically. 
                //we should change that in the future
                owner.ChangeState<SelectItemState>();
                //OpenItemMenu
                break;

            case typeOfAction.Wait:
                //Skip turn
                currentAction = typeOfAction.Move;
                owner.ChangeState<WaitUnitState>();
                break;

        }
    }

}
