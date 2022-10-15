using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "InventorySystem/EquipmentInventory")]
public class EquipmentInventory : ScriptableObject
{
    public List<WeaponSlot> container = new List<WeaponSlot>();
    public void AddItem(weapon _equipment)
    {
        bool hasEquipment = false;
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].weapon == _equipment)
            {
                
                hasEquipment = true;
                container.Add(new WeaponSlot(_equipment));
                break;
            }
        }

        if (!hasEquipment)
        {
            container.Add(new WeaponSlot(_equipment));
        }
    }

    //private void OnApplicationQuit()
    //{
    //   container.Clear();
    //}
}

[System.Serializable]
public class WeaponSlot
{
    public weapon weapon;
    
    public WeaponSlot(weapon _weapon)
    {
        weapon = _weapon;
    }

    //public void AddAmount(int value)
    //{
    //    amount += value;
    //}

}

