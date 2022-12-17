using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStrike : AbilitySequence
{
    [SerializeField] int furyAmount;

    public override IEnumerator Sequence(GameObject target, BattleController controller)
    {
        user = controller.currentUnit;
        playing = true;
        user.SpendActionPoints(ability.actionCost);

        ActionEffect.instance.Play(ability.cameraSize, ability.effectDuration, ability.shakeIntensity, ability.shakeDuration);
        if (target.GetComponent<Unit>() != null)
        {
            Attack(target.GetComponent<Unit>());
        }

        if(target.GetComponent<BearObstacleScript>() != null)
        {
            user.Attack();
            target.GetComponent<BearObstacleScript>().GetDestroyed(controller.board);
        }

        while (ActionEffect.instance.CheckActionEffectState())
        {
            yield return null;
        }

        playing = false;
    }
}
