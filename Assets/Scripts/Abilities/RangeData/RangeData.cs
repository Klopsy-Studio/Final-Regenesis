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
    [SerializeField] public int squareReach;
    [SerializeField] public Directions alternateSideDir;
    [SerializeField] public int alternateSideReach;
    [SerializeField] public int alternateSideLength;
    [SerializeField] public int movementRange;
}

