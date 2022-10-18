using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "InventorySystem/materialInventory")]
public class MaterialInventory : ScriptableObject
{
    public List<MonsterMaterialSlot> materialContainer = new List<MonsterMaterialSlot>();
    public void AddMonsterMaterial(MonsterMaterial _material, int _amount)
    {
        bool hasMaterial = false;
        for (int i = 0; i < materialContainer.Count; i++)
        {
            if(materialContainer[i].material == _material)
            {
                materialContainer[i].AddAmount(_amount);
                hasMaterial = true;
                break;
            }
        }
        if (!hasMaterial)
        {
            materialContainer.Add(new MonsterMaterialSlot(_material, _amount));
        }
    }
}

[System.Serializable]
public class MonsterMaterialSlot
{
    public MonsterMaterial material;
    public int amount;
    public MonsterMaterialSlot (MonsterMaterial _material, int _amount)
    {
        material = _material;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
