using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponTreeTemplate : MonoBehaviour
{
    public TextMeshProUGUI treeNameText;
    public Transform contentTransform;
    public WeaponInfoTemplate[] weaponInfoTemplateList;


    public void FillVariables(OldForgeManager _forgeManager, PurchaseForge _purchaseForge)
    {
        for (int i = 0; i < weaponInfoTemplateList.Length; i++)
        {
            weaponInfoTemplateList[i].oldForgeManager = _forgeManager;
            weaponInfoTemplateList[i].purchaseForge = _purchaseForge;
        }
    }
}
