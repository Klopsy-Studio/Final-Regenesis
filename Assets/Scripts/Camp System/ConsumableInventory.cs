using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InventorySystem/ConsumableInventory")]
public class ConsumableInventory : ScriptableObject
{
    public List<ConsumableSlot> container = new List<ConsumableSlot>();
    public void AddItem(Consumables _consumable, int _amount)
    {
        bool hasConsumable = false;
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].consumable == _consumable)
            {
                container[i].AddAmount(_amount);
                hasConsumable = true;
                break;
            }
        }

        if (!hasConsumable)
        {
            container.Add(new ConsumableSlot(_consumable, _amount));
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
