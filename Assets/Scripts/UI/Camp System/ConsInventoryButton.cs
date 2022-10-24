using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConsInventoryButton : MonoBehaviour, IPointerClickHandler
{
    public ConsumableInventory inventory;
   [HideInInspector] public int consumableID;
  
    public void FillVariables(ConsumableInventory _inventory, int i)
    {
        inventory = _inventory;
        consumableID = i;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //var inventorySlot = inventory.consumableContainer[consumableID];
        //GameManager.instance.consumableBackpack.AddConsumable(inventorySlot.consumable, inventorySlot.amount);

        var backpackInventory = GameManager.instance.consumableBackpack;
        inventory.TransferConsumables(backpackInventory, consumableID);

    }
}
