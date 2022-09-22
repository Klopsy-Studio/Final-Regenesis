using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/New Unit Profile")]
public class UnitProfile : ScriptableObject
{
    public string unitName;

    [Header("Sprites")]
    public Sprite unitSprite;
    public Sprite unitPortrait;
    public Sprite unitReaction;
    public Sprite unitAction;
    public Sprite unitTimelineIcon;

    [Header("Equipment")]
    public Weapons unitWeapon;
}
