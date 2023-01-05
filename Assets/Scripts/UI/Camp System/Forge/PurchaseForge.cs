using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PurchaseForge : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] OldForgeManager forgeManager;
    [HideInInspector] public Weapons weaponToPurchase;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(weaponToPurchase == null)
        {
            Debug.Log("WEAPON TO PURCHASE IS NULL");
        }

        if (forgeManager.canPurchaseItem())
        {
            Debug.Log("AAAAAAAAA");
            forgeManager.coins -= forgeManager.currentWeaponInfoSelected.cost;

            for (int i = 0; i < forgeManager.currentWeaponInfoSelected.materialRequirement.Length; i++)
            {
                forgeManager.currentWeaponInfoSelected.materialRequirement[i].ReduceMaterial(GameManager.instance.materialInventory);
             
            }

            forgeManager.UpdateForgeUI();
            GameManager.instance.equipmentInventory.AddItem(weaponToPurchase);
            forgeManager.currentWeaponInfoSelected.weaponUpgradeTree.QuitRequiredWeapon(GameManager.instance.equipmentInventory);
        }

        
     
     
    }
}
