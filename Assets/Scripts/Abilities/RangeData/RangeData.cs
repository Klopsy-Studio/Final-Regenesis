using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RangeData 
{
    [SerializeField] public TypeOfAbilityRange range;
    [SerializeField] public Directions lineDir;
    [SerializeField] public int lineLength;
    [SerializeField] public Directions sideDir;
    [SerializeField] public int sideReach;
    [SerializeField] public int sideLength;
    [SerializeField] public int crossLength;
    [SerializeField] public int crossOffset;
    [SerializeField] public int squareReach;
    [SerializeField] public Directions alternateSideDir;
    [SerializeField] public int alternateSideReach;
    [SerializeField] public int alternateSideLength;
    [SerializeField] public int movementRange;
    [SerializeField] public bool removeOrigin;
    [SerializeField] public int itemRange;
    [SerializeField] public bool itemRemoveContent;


    public AbilityRange GetOrCreateRange(TypeOfAbilityRange rangeType, GameObject receiver)
    {
        AbilityRange rangeClass;

        switch (rangeType)
        {
            case TypeOfAbilityRange.Cone:

                if (receiver.GetComponent<ConeAbilityRange>() != null)
                {
                    rangeClass = receiver.GetComponent<ConeAbilityRange>();
                }
                else
                {
                    rangeClass = receiver.AddComponent<ConeAbilityRange>();
                }
                break;

            case TypeOfAbilityRange.Constant:
                if (receiver.GetComponent<ConstantAbilityRange>() != null)
                {
                    rangeClass = receiver.GetComponent<ConstantAbilityRange>();
                }
                else
                {
                    rangeClass = receiver.AddComponent<ConstantAbilityRange>();
                }
                break;

            case TypeOfAbilityRange.Infinite:
                if (receiver.GetComponent<InfiniteAbilityRange>() != null)
                {
                    rangeClass = receiver.GetComponent<InfiniteAbilityRange>();
                }
                else
                {
                    rangeClass = receiver.AddComponent<InfiniteAbilityRange>();
                }
                break;

            case TypeOfAbilityRange.LineAbility:
                if (receiver.GetComponent<LineAbilityRange>() != null)
                {
                    rangeClass = receiver.GetComponent<LineAbilityRange>();
                }
                else
                {
                    rangeClass = receiver.AddComponent<LineAbilityRange>();
                }
                break;
            case TypeOfAbilityRange.SelfAbility:
                if (receiver.GetComponent<SelfAbilityRange>() != null)
                {
                    rangeClass = receiver.GetComponent<SelfAbilityRange>();
                }
                else
                {
                    rangeClass = receiver.AddComponent<SelfAbilityRange>();
                }
                break;
            case TypeOfAbilityRange.SquareAbility:
                if (receiver.GetComponent<SquareAbilityRange>() != null)
                {
                    rangeClass = receiver.GetComponent<SquareAbilityRange>();
                }
                else
                {
                    rangeClass = receiver.AddComponent<SquareAbilityRange>();
                }
                break;
            case TypeOfAbilityRange.Side:
                if (receiver.GetComponent<SideAbilityRange>() != null)
                {
                    rangeClass = receiver.GetComponent<SideAbilityRange>();
                }
                else
                {
                    rangeClass = receiver.AddComponent<SideAbilityRange>();
                }
                break;
            case TypeOfAbilityRange.AlternateSide:
                if (receiver.GetComponent<AlternateSideRange>() != null)
                {
                    rangeClass = receiver.GetComponent<AlternateSideRange>();
                }
                else
                {
                    rangeClass = receiver.AddComponent<AlternateSideRange>();
                }
                break;
            case TypeOfAbilityRange.Cross:
                if (receiver.GetComponent<CrossAbilityRange>() != null)
                {
                    rangeClass = receiver.GetComponent<CrossAbilityRange>();
                }
                else
                {
                    rangeClass = receiver.AddComponent<CrossAbilityRange>();
                }
                break;
            case TypeOfAbilityRange.Normal:
                if (receiver.GetComponent<MovementRange>() != null)
                {
                    rangeClass = receiver.GetComponent<MovementRange>();
                }
                else
                {
                    rangeClass = receiver.AddComponent<MovementRange>();
                }
                break;
            case TypeOfAbilityRange.Item:
                if (receiver.GetComponent<ItemRange>() != null)
                {
                    rangeClass = receiver.GetComponent<ItemRange>();
                }
                else
                {
                    rangeClass = receiver.AddComponent<ItemRange>();
                }
                break;
            default:
                rangeClass = null;
                break;
        }

        if(rangeClass != null)
        {
            rangeClass.AssignVariables(this);
            return rangeClass;
        }
        else
        {
            return null;
        }
        
    }


    //public AbilityRange AssignRangeClass<T>(GameObject receiver)
    //{
    //    AbilityRange rangeClass;
    //    if (receiver.GetComponent<T>() != null)
    //    {
    //        rangeClass = receiver.GetComponent<T>() as AbilityRange;
    //    }
    //    else
    //    {
    //        rangeClass = receiver.AddComponent<T>();
    //    }
    //    rangeClass.AssignVariables(this);
    //    return rangeClass;
    //}
}


