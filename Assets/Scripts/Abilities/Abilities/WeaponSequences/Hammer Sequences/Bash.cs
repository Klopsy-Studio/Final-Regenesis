using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bash : AbilitySequence
{
    [SerializeField] int bashStrenght;
    public override IEnumerator Sequence(GameObject target, BattleController controller)
    {
        user = controller.currentUnit;
        playing = true;
        user.SpendActionPoints(ability.actionCost);
        ActionEffect.instance.Play(ability.cameraSize, ability.effectDuration, ability.shakeIntensity, ability.shakeDuration);

        if(target.GetComponent<Unit>()!= null)
        {
            Unit u = target.GetComponent<Unit>();
            Attack(u);
            Movement m = u.GetComponent<Movement>();

            m.PushUnit(user.tile.GetDirections(u.tile), bashStrenght, controller.board);

            while (m.moving)
            {
                yield return null;
            }
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
