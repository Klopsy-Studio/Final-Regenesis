using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumableType
{
    NormalConsumable,
    TimelineConsumable,
    TargetConsumable,
}
public abstract class Consumables : ScriptableObject
{
    
    [SerializeField] protected string itemName;
    [SerializeField] public Sprite sprite;
    [SerializeField] private ConsumableType consumableType;
    public RangeData itemRange;
    public RangeData effectRange;
    public int maxBackPackAmount;
    public string description;
    public ConsumableType ConsumableType { get { return consumableType; } }

    public string ItemName { get { return itemName; } }
    public abstract bool ApplyConsumable(Unit unit);
    public abstract bool ApplyConsumable(Tile t, BattleController battleController);

    public virtual bool CanUseItem()
    {
        return true;
    }

}
