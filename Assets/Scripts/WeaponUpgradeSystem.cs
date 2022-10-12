using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponUpgrade")]
public class WeaponUpgradeSystem : ScriptableObject
{

   public AllWeaponsTrees[] allWeaponsTrees;
  
}

[System.Serializable]
public class AllWeaponsTrees
{
   public WeaponUpgradeTree[] weaponUpgrade;

  
}

[System.Serializable]
public class WeaponUpgradeTree
{
    public string itemName;
    public Weapons weapon;
    public int cost;
    public Weapons weaponRequired;
    public MaterialRequirement[] materialsRequired;
  

}

public enum Monster
{
    Monster1,
    Monster2,
    Monster3
}

public enum TypeOfMaterial
{
    Scale,
    Fang,
    Skull
}

[System.Serializable]
public class MaterialRequirement
{
    public Monster monster;
    public TypeOfMaterial material;
    public int numberOfMaterial;
   
}
