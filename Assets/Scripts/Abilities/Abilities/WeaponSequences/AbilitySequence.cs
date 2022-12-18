using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Ability Sequences")]

public class AbilitySequence : ScriptableObject
{
    [HideInInspector] public Abilities ability;
    [HideInInspector] public PlayerUnit user;
    [HideInInspector] public Weapons weapon;
    [HideInInspector] public bool playing;


    [Header("Bow Variables")]
    [HideInInspector] public bool extraAttack;
    public virtual IEnumerator Sequence(GameObject target, BattleController controller)
    {
        playing = true;
        yield return null;
        playing = false;
    }
    
    public virtual IEnumerator Sequence(GameObject target, BattleController controller, bool extraAttack)
    {
        playing = true;
        yield return null;
        playing = false;
    }

    private void OnEnable()
    {
        extraAttack = false;
        playing = false;
    }


    public void Attack(Unit unitTarget)
    {
        unitTarget.ReceiveDamage(ability.CalculateDmg(user, unitTarget));
        user.Attack();
    }


    public int DefaultBowAttack(BattleController controller)
    {
        int numberOfAttacks = 1;

        if (controller.bowExtraAttack)
        {
            numberOfAttacks++;
            user.animations.SetAnimation("extraAttack");
            controller.currentUnit.SpendActionPoints(ability.actionCost + 1);
        }
        else
        {
            controller.currentUnit.SpendActionPoints(ability.actionCost);
        }

        return numberOfAttacks;
    }

    public void HammerFurySequence(int pushFury, Unit target, BattleController controller, Directions dir)
    {
        target.GetComponent<Movement>().PushUnit(dir, pushFury, controller.board);
        //Just a value to trigger it
        target.ApplyStunValue(100);
    }

    public void IncreaseFury(int fury)
    {
        user.hammerFuryAmount += fury;
        user.playerUI.ChangeFuryValue(user.hammerFuryAmount);
        if(user.hammerFuryAmount >= user.hammerFuryMax)
        {
            user.hammerFuryAmount = user.hammerFuryMax;
        }
    }
    public void ResetFury()
    {
        user.hammerFuryAmount = 0;
        user.playerUI.ChangeFuryValue(user.hammerFuryAmount);
    }

    public bool CheckFury()
    {
        if(user.hammerFuryAmount >= user.hammerFuryMax)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
