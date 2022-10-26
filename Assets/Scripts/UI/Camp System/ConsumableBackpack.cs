using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InventorySystem/ConsumableBackpack")]
public class ConsumableBackpack : ConsumableInventory
{
    public override void AddConsumable(Consumables _consumable, int _amount)
    {
        bool hasConsumable = false;
        for (int i = 0; i < consumableContainer.Count; i++)
        {
            if (consumableContainer[i].consumable == _consumable)
            {
                if(consumableContainer[i].amount < _consumable.maxBackPackAmount)
                {
                    var addAmount = _consumable.maxBackPackAmount - consumableContainer[i].amount;

                    consumableContainer[i].AddAmount(addAmount);
                    hasConsumable = true;
                    break;
                }
              
            }
        }

        if (!hasConsumable)
        {
            consumableContainer.Add(new ConsumableSlot(_consumable, _amount));
        }
    }

    public void TransferConsumablesToInventory(ConsumableInventory targetInventory, int consumableID, DisplayConsumableBackpack displayConsumableBackpack)
    {
        var inventorySlot = consumableContainer[consumableID];

        displayConsumableBackpack.consumableDisplayed.Remove(inventorySlot);
        displayConsumableBackpack.slotPrefabList[consumableID].gameObject.SetActive(false);
        displayConsumableBackpack.slotPrefabList.RemoveAt(consumableID);
        consumableContainer.Remove(inventorySlot);

       

        targetInventory.AddConsumable(inventorySlot.consumable, inventorySlot.amount);
        //displayConsumableBackpack.UpdateDisplay();
    }
}
