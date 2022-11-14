using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponInfoTemplate : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector] public ForgeManager forgeManager;
    [HideInInspector] public PurchaseForge purchaseForge;
    [HideInInspector] public Weapons weaponToPurchase;
    [HideInInspector] public string kitName;
    [HideInInspector] public Sprite weaponImg;
    [HideInInspector] public int cost;
    [HideInInspector] public WeaponUpgradeTree weaponUpgradeTree;
    [HideInInspector] public string moveRange;
    [HideInInspector] public string element;
    [HideInInspector] public string power;
    [HideInInspector] public string defense;
    [HideInInspector] public string critic;
    [HideInInspector] public string elementEffectiveness;
    [HideInInspector] public MaterialRequirement[] materialRequirement;
 

    public void OnPointerClick(PointerEventData eventData)
    {

        forgeManager.SelectCurrentWeaponInfo(this);
        purchaseForge.weaponToPurchase = weaponToPurchase;

        forgeManager.kitNameTxt.text = kitName;
        forgeManager.weaponImage.sprite = weaponImg;
        forgeManager.costTxt.text = cost.ToString();
        forgeManager.moveRangeTxt.text = moveRange;
        forgeManager.elementTxt.text = element;
        forgeManager.powerTxt.text = power;
        forgeManager.defenseTxt.text = defense;
        forgeManager.criticTxt.text = critic;
        //forgeManager.elementEffectiveness.text = elementEffectiveness;
        //forgeManager.materialRequirement = materialRequirement;


    }

}
