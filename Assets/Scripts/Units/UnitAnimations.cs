using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimations : MonoBehaviour
{
    [SerializeField] Animator unitAnimator;
    

    public void SetAnimation(string animationToCall)
    {
        unitAnimator.SetTrigger(animationToCall);
    }

    public void SetIdle()
    {
        SetAnimation("idle");
    }

    public void SetCombatIdle()
    {
        SetAnimation("combatIdle");
    }

    public void SetNearDeath()
    {
        SetAnimation("nearDeath");
    }

    public void SetDeath()
    {
        SetAnimation("death");
    }


    public void SetAttackHammer()
    {
        SetAnimation("attackHammer");
    }

    public void SetAttackSlingshot()
    {
        SetAnimation("attackSlingshot");
    }

    public void SetPush()
    {
        SetAnimation("push");
    }

    public void SetDamage()
    {
        SetAnimation("damage");
    }
}
