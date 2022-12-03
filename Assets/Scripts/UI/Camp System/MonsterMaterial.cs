using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public enum Rarity
{
    Common = 5,
    Special = 3
}

[CreateAssetMenu(menuName = "MonsterMaterial")]
public class MonsterMaterial: ScriptableObject
{
    public Sprite sprite;
    public Monster monster;
    public TypeOfMaterial material;
    public Rarity rarity;
}
