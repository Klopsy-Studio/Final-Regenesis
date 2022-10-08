using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponUpgrade")]
public class WeaponUpgradeSystem : ScriptableObject
{

    [SerializeField] WeaponShop[] weaponTree1;
  
}

[System.Serializable]
public class WeaponShop
{
    public string itemName;
    public Weapons weapon;
    public int coins;
    public int material1;
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
    public Weapons weaponRequired;
}
