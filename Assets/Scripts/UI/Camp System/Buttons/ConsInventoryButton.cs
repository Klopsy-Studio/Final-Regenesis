using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConsInventoryButton : UIButtons
{
    public DisplayConsumableInventory displayconsumableInventory;
    public ConsumableInventory inventory;
    public int consumableID;

    public void FillVariables(ConsumableInventory _inventory, int i, DisplayConsumableInventory _displayconsumableInventory)
    {
        inventory = _inventory;
        consumableID = i;
        displayconsumableInventory = _displayconsumableInventory;
        
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        //var inventorySlot = inventory.consumableContainer[consumableID];
        //GameManager.instance.consumableBackpack.AddConsumable(inventorySlot.consumable, inventorySlot.amount);

        var backpackInventory = GameManager.instance.consumableBackpack;
        inventory.TransferConsumablesToBackPack(backpackInventory, consumableID, displayconsumableInventory);
        
    }
}
