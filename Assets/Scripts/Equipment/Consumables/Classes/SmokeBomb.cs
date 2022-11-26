using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Consumables/New Smoke Bomb")]

public class SmokeBomb : Consumables
{
    [SerializeField] SmokeBombTimeline smokeBomb;
    public override bool ApplyConsumable(Tile t, BattleController battleController)
    {
        Instantiate(smokeBomb);
        smokeBomb.range.tile = t;
        smokeBomb.ApplyEffect(battleController.board);
        return true;
    }

    public override bool ApplyConsumable(Unit unit)
    {
        return true;
    }

}
