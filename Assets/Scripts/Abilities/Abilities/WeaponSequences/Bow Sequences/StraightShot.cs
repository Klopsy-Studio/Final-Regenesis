using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Weapon Sequences/Bow Sequences/Straight Shot")]
public class StraightShot : AbilitySequence
{
    public override IEnumerator Sequence(GameObject target, BattleController controller)
    {
        playing = true;
        yield return null; 

        ActionEffect.instance.Play(ability.cameraSize, ability.effectDuration, ability.shakeIntensity, ability.shakeDuration);

        if(target.GetComponent<Unit>()!= null)
        {
            Unit unitTarget = target.GetComponent<Unit>();

            int numberOfAttacks = 1;

            if (controller.bowExtraAttack)
            {
                numberOfAttacks++;
                user.animations.SetAnimation("extraAttack");
                controller.currentUnit.actionsPerTurn -= ability.actionCost+1;
                controller.currentUnit.playerUI.SpendActionPoints(ability.actionCost + 1);

            }
            else
            {
                controller.currentUnit.playerUI.SpendActionPoints(ability.actionCost);
                controller.currentUnit.actionsPerTurn -= ability.actionCost;
            }
            for (int i = 0; i < numberOfAttacks; i++)
            {
                Attack(unitTarget);
                yield return new WaitForSeconds(0.7f);
            }
        }

        if (target.GetComponent<BearObstacleScript>() != null)
        {
            BearObstacleScript obstacle = target.GetComponent<BearObstacleScript>();
            user.Attack();
            obstacle.GetDestroyed(controller.board);
        }

        while(ActionEffect.instance.play || ActionEffect.instance.recovery)
        {
            yield return null;
        }




        playing = false;

    }

    public void Attack(Unit unitTarget)
    {
        unitTarget.ReceiveDamage(ability.CalculateDmg(user, unitTarget));
        user.Attack();
    }
}
