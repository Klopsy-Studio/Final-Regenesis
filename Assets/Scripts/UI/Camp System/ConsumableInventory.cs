using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InventorySystem/ConsumableInventory")]
public class ConsumableInventory : ScriptableObject
{
    public List<ConsumableSlot> consumableContainer = new List<ConsumableSlot>();
    public void AddConsumable(Consumables _consumable, int _amount)
    {
        bool hasConsumable = false;
        for (int i = 0; i < consumableContainer.Count; i++)
        {
            if (consumableContainer[i].consumable == _consumable)
            {
                consumableContainer[i].AddAmount(_amount);
                hasConsumable = true;
                break;
            }
        }

        if (!hasConsumable)
        {
            consumableContainer.Add(new ConsumableSlot(_consumable, _amount));
        }
    }

    public void TransferConsumables(ConsumableInventory targetInventory, int consumableID)
    {
        var inventorySlot = consumableContainer[consumableID];
        var inventorySlotAmount = inventorySlot.BackpackAmount();
        targetInventory.AddConsumable(inventorySlot.consumable, inventorySlotAmount);
    }

    //private void OnApplicationQuit()
    //{
    //   container.Clear();
    //}
}

[System.Serializable]
public class ConsumableSlot
{
    public Consumables consumable;
    public int amount;
    

    public int BackpackAmount()
    {
        int amountToTransfer;
        if (amount - consumable.maxBackPackAmount < 0)
        {
            amountToTransfer = amount;
            amount = 0;
        }
        else
        {
            amount -= consumable.maxBackPackAmount;
            amountToTransfer = consumable.maxBackPackAmount;
        }


        return amountToTransfer;
        //amount -= 3;
        //return 3;
    }

    public ConsumableSlot (Consumables _consumable, int _amount)
    {
        consumable = _consumable;
        amount = _amount;
    }

    public void AddAmount(int value)
    {

        amount += value;
        if(amount > 99)
        {
            amount = 99;
            Debug.Log("NO PUEDE SUPERARSE DE 99 stack");
        }
    }
}
