using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/New Unit Profile")]
public class UnitProfile : ScriptableObject
{
    [Header("Placeholder")]
    public Color unitColor;

    public string unitName;

    [Header("UI Sprites")]
    public Sprite unitPortrait;
    public Sprite unitFullPortrait;
    public Sprite unitTimelineIcon;
    [Space]
    [Header("Unit Sprites")]
    public Sprite unitIdle;
    public Sprite unitIdleCombat;
    public Sprite unitHeavyWeapon;
    public Sprite unitLightWeapon;
    public Sprite unitDamageReaction;
    public Sprite unitTakeOutWeapon;
    public Sprite unitPush; 


    [Header("Equipment")]
    public Weapons unitWeapon;
}
