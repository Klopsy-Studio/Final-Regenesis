using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionSelection : MonoBehaviour
{
  
    //Offset to set the image in the right place
    public float offset = 20f;
    
    
    //Reference to children in scene
    public RectTransform[] options;
    public RectTransform selector;

    [SerializeField] Color defaultColor;
    [SerializeField] Color disabledColor;
    [Space]

    [Header("Action Options")]
    [SerializeField] Text moveText;
    [SerializeField] Text abilityText;
    [SerializeField] Text itemText;
    [SerializeField] Text waitText;

    [Space]

    [SerializeField] Button buttonMove;
    [SerializeField] Button buttonAbility;
    [SerializeField] Button buttonItem;
    [SerializeField] Button buttonWait;
    [Space]

    [Header("Ability Options")]
    [SerializeField] Text ability1;
    [SerializeField] Text ability2;
    [SerializeField] Text ability3;
    [SerializeField] Text ability4;
    public int currentSelection = 0;

    [Space]

    [SerializeField] Button buttonSelectAbility1;
    [SerializeField] Button buttonSelectAbility2;
    [SerializeField] Button buttonSelectAbility3;
    [SerializeField] Button buttonSelectAbility4;

    [Space]

    [Header("Item Options")]
    [SerializeField] Text item1;
    [SerializeField] Text item2;
    [SerializeField] Text item3;
    [SerializeField] Text item4;

    [Space]
    [SerializeField] Button buttonSelectItem1;
    [SerializeField] Button buttonSelectItem2;
    [SerializeField] Button buttonSelectItem3;




    public bool onOption;
    private void Start()
    {
        //ACTION BUTTON
        if (buttonMove != null)
        {
            buttonMove.onClick.AddListener(() =>
            {
                Debug.Log("SELECT MOVE EVENT");
                UIController.instance.ClickConfirm(0);
            });
        }

        if(buttonAbility != null)
        {
            buttonAbility.onClick.AddListener(() =>
            {
                Debug.Log("SELECT ABILITY EVENT");
                UIController.instance.ClickConfirm(1);
            });
        }


        if (buttonItem != null)
        {
            buttonItem.onClick.AddListener(() =>
            {
                Debug.Log("SELECT ITEM EVENT");
                UIController.instance.ClickConfirm(2);
            });
        }

        if (buttonWait != null)
        {
            buttonWait.onClick.AddListener(() =>
            {
                UIController.instance.ClickConfirm(4);
            });
        }

        //ABILITIES BUTTON
        if (buttonSelectAbility1 != null)
        {
            buttonSelectAbility1.onClick.AddListener(() =>
            {
                UIController.instance.ClickConfirm(0);
                UIController.instance.ClickCancel(0);
            });
        }

        if (buttonSelectAbility2 != null)
        {
            buttonSelectAbility2.onClick.AddListener(() =>
            {
                UIController.instance.ClickConfirm(1);
                UIController.instance.ClickCancel(1);

            });

        }

        if (buttonSelectAbility3 != null)
        {
            buttonSelectAbility3.onClick.AddListener(() =>
            {
                UIController.instance.ClickConfirm(2);
                UIController.instance.ClickCancel(2);

            });

            
        }

        if (buttonSelectAbility4 != null)
        {
            buttonSelectAbility4.onClick.AddListener(() =>
            {
                UIController.instance.ClickConfirm(3);
                UIController.instance.ClickCancel(3);

            });
        }

        if (buttonSelectItem1 != null)
        {
            buttonSelectItem1.onClick.AddListener(() =>
            {
                UIController.instance.ClickConfirm(0);
            });
        }

        if (buttonSelectItem2 != null)
        {
            buttonSelectItem2.onClick.AddListener(() =>
            {
                UIController.instance.ClickConfirm(1);
            });
        }

        if (buttonSelectItem3 != null)
        {
            buttonSelectItem3.onClick.AddListener(() =>
            {
                UIController.instance.ClickConfirm(2);
            });
        }


    }

    public void MouseOverEnter(SelectorMovement s)
    {
        selector = UIController.instance.ReturnSelector();
        onOption = true;
        if(selector != null)
        {
            selector.position = new Vector3(selector.position.x, s.transform.position.y, selector.position.z);
        }
     
    }

    public void MouseOverExit(SelectorMovement s)
    {
        onOption = false;    
    }

    //Move selector forward/downwards
    public void MoveForward()
    {
        if(currentSelection < options.Length - 1)
        {
            currentSelection++;
        }
        else
        {
            currentSelection = 0;
        }

        MoveSelector(options[currentSelection].position.y);
    }
    
    //public void OnHover(int index)
    //{
    //    selector.position = new Vector3(selector.position.x, newPosition + offset, selector.position.z);
    //}

    public void MoveBackwards()
    {
        if(currentSelection > 0)
        {
            currentSelection--;
        }
        else
        {
            currentSelection = options.Length - 1;
        }

        MoveSelector(options[currentSelection].position.y);
    }

    public void ResetSelector()
    {
        currentSelection = 0;
        MoveSelector(options[currentSelection].position.y);
    }
    void MoveSelector(float newPosition)
    {
        selector.position = new Vector3(selector.position.x, newPosition +offset, selector.position.z);
    }


    void DisableOption(Text option)
    {
        option.color = disabledColor;
    }

    void EnableOption(Text option)
    {
        option.color = defaultColor;
    }

    public void DisableSelectOption(typeOfAction action)
    {
        switch (action)
        {
            case typeOfAction.Move:
                DisableOption(moveText);
                break;
            case typeOfAction.Ability:
                DisableOption(abilityText);
                break;
            case typeOfAction.Item:
                DisableOption(itemText);
                break;
            case typeOfAction.Wait:
                DisableOption(waitText);
                break;
            default:
                break;
        }
    }

    public void EnableSelectOption(typeOfAction action)
    {
        switch (action)
        {
            case typeOfAction.Move:
                EnableOption(moveText);
                break;
            case typeOfAction.Ability:
                EnableOption(abilityText);
                break;
            case typeOfAction.Item:
                EnableOption(itemText);
                break;
            case typeOfAction.Wait:
                EnableOption(waitText);
                break;
            default:
                break;
        }
    }


    public void EnableSelectAbilty(int ability)
    {
        switch (ability)
        {
            case 0:
                ability1.color = defaultColor;
                break;
            case 1:
                ability2.color = defaultColor;
                break;
        }
    }

    public void DisableSelectAbilty(int ability)
    {
        switch (ability)
        {
            case 0:
                ability1.color = disabledColor;
                break;
            case 1:
                ability2.color = disabledColor;
                break;
        }
    }
}
