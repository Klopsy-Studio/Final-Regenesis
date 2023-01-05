using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class WeaponInfoTemplate : MonoBehaviour, IPointerClickHandler
{
    public int WeaponDamage { get; private set; }
    public int WeaponRange { get; private set; }
    ForgeManager forgeManager;
    public TextMeshProUGUI weaponName;
    WeaponUpgrade weaponUpgrade;

    public void OnPointerClick(PointerEventData eventData)
    {

        forgeManager.weaponPanelInfo.UpdatePanelInfo(this);
        forgeManager.UpdateMaterialRequiredPanel(weaponUpgrade);
        foreach (var material in weaponUpgrade.materialsRequired)
        {
            Debug.Log("El material requerido es " + material.monsterMaterial.materialName);
        }

    }



    public void SetWeaponInfo(WeaponUpgrade _weaponUpgrade, ForgeManager _forgeManager)
    {
        weaponUpgrade = _weaponUpgrade;
        var weapon = _weaponUpgrade.weapon;
        weaponName.SetText(_weaponUpgrade.itemName);
        forgeManager = _forgeManager;
        WeaponDamage = weapon.Power;
        WeaponRange = weapon.range;
    }


    //DEPRECATED BORRAR
    [HideInInspector] public OldForgeManager oldForgeManager;
    [HideInInspector] public PurchaseForge purchaseForge;
    [HideInInspector] public Weapons weaponToPurchase;
    [HideInInspector] public string kitName;
    [HideInInspector] public Sprite weaponImg;
    [HideInInspector] public int cost;
    [HideInInspector] public WeaponUpgrade weaponUpgradeTree;
    [HideInInspector] public string moveRange;
    [HideInInspector] public string element;
    [HideInInspector] public string power;
    [HideInInspector] public string defense;
    [HideInInspector] public string critic;
    [HideInInspector] public string elementEffectiveness;
    [HideInInspector] public MaterialRequirement[] materialRequirement;


    //public void OnPointerClick(PointerEventData eventData)
    //{

    //    forgeManager.SelectCurrentWeaponInfo(this);
    //    purchaseForge.weaponToPurchase = weaponToPurchase;

    //    forgeManager.kitNameTxt.text = kitName;
    //    forgeManager.weaponImage.sprite = weaponImg;
    //    forgeManager.costTxt.text = cost.ToString();
    //    forgeManager.moveRangeTxt.text = moveRange;
    //    forgeManager.elementTxt.text = element;
    //    forgeManager.powerTxt.text = power;
    //    forgeManager.defenseTxt.text = defense;
    //    forgeManager.criticTxt.text = critic;
    //    //forgeManager.elementEffectiveness.text = elementEffectiveness;
    //    //forgeManager.materialRequirement = materialRequirement;


    //}

}
