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
    public ConsumableSlot (Consumables _consumable, int _amount)
    {
        consumable = _consumable;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
