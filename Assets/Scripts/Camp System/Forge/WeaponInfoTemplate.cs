using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponInfoTemplate : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] ForgeManager forgeManager;
    [HideInInspector] public string kitName;
    [HideInInspector] public string cost;
    [HideInInspector] public string moveRange;
    [HideInInspector] public string element;
    [HideInInspector] public string power;
    [HideInInspector] public string defense;
    [HideInInspector] public string critic;
    [HideInInspector] public string elementalDefense;
 

    public void OnPointerClick(PointerEventData eventData)
    {
        forgeManager.kitNameTxt.text = kitName;
        forgeManager.costTxt.text = cost;
        forgeManager.moveRangeTxt.text = moveRange;
        forgeManager.elementTxt.text = element;
        forgeManager.powerTxt.text = power;
        forgeManager.defenseTxt.text = defense;
        forgeManager.criticTxt.text = critic;
        forgeManager.elementalDefenseTxt.text = elementalDefense;


    }

}
