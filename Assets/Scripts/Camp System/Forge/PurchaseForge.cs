using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PurchaseForge : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] ForgeManager forgeManager;
    [HideInInspector] public Weapons weaponToPurchase;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(weaponToPurchase == null)
        {
            Debug.Log("WEAPON TO PURCHASE IS NULL");
        }
        GameManager.instance.equipmentInventory.AddItem(weaponToPurchase);
     
    }
}
