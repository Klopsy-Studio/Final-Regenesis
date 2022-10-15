using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTreeTemplate : MonoBehaviour
{

    public WeaponInfoTemplate[] weaponInfoTemplateList;


    public void FillVariables(ForgeManager _forgeManager, PurchaseForge _purchaseForge)
    {
        for (int i = 0; i < weaponInfoTemplateList.Length; i++)
        {
            weaponInfoTemplateList[i].forgeManager = _forgeManager;
            weaponInfoTemplateList[i].purchaseForge = _purchaseForge;
        }
    }
}
