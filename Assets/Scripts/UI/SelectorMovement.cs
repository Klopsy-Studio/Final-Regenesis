using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectorMovement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] OptionSelection optionSelection;

    [SerializeField] int selection;

    public void OnPointerEnter(PointerEventData eventData)
    {
        optionSelection.MouseOverEnter(this);
        optionSelection.currentSelection = selection;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        optionSelection.MouseOverExit(this);
    }
}
