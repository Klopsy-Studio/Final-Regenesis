using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "InventorySystem/ConsumableInventory")]
public class MaterialInventory : ScriptableObject
{
   public List<>
}

public class MaterialRequirementSlot
{
    public MaterialRequirement material;
    public int amount;
    public MaterialRequirementSlot (MaterialRequirement _material, int _amount)
    {
        material = _material;
        amount = _amount;
    }
}
