using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectorMovement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Color hoverColor;
    [SerializeField] Color normalColor;


    [SerializeField] OptionSelection optionSelection;
    [SerializeField] Text textButton;

    [SerializeField] int selection;

    public AbilityDescription abilityDescription;

    public bool canBeSelected;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (canBeSelected)
        {
            if(abilityDescription!= null)
            {
                abilityDescription.gameObject.SetActive(true);
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
            }

            optionSelection.MouseOverExit(this);

            textButton.color = normalColor;
        }
    }


    public void ChangeToDefault()
    {
        textButton.color = normalColor;

    }

}
