using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "InventorySystem/EquipmentInventory")]
public class EquipmentInventory : ScriptableObject
{
    public List<EquipmentSlot> container = new List<EquipmentSlot>();
    public void AddItem(Equipment _equipment)
    {
        bool hasEquipment = false;
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].equipment == _equipment)
            {
                
                hasEquipment = true;
                container.Add(new EquipmentSlot(_equipment));
                break;
            }
        }

        if (!hasEquipment)
        {
            container.Add(new EquipmentSlot(_equipment));
        }
    }

    //private void OnApplicationQuit()
    //{
    //   container.Clear();
    //}
}

[System.Serializable]
public class EquipmentSlot
{
    public Equipment equipment;
    
    public EquipmentSlot(Equipment _equipment)
    {
        equipment = _equipment;
    }

    //public void AddAmount(int value)
    //{
    //    amount += value;
    //}
}

