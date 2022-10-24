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

[CreateAssetMenu(menuName = "MonsterMaterial")]
public class MonsterMaterial: ScriptableObject
{
    public Sprite sprite;
    public Monster monster;
    public TypeOfMaterial material;
}
