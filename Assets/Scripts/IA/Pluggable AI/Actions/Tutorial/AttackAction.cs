using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action
{
    public override void Act(MonsterController controller)
    {
        controller.CallCoroutine(Attack(controller, controller.ChooseAttack()));
    }


    IEnumerator Attack(MonsterController controller, MonsterAbility ability)
    {
        List<Tile> tiles = ability.GetAttackTiles(controller);

        controller.battleController.board.SelectAttackTiles(tiles);

        controller.target.ReceiveDamage(40);
        controller.target.React();
        controller.target.DamageEffect();

        controller.currentEnemy.React();

        ActionEffect.instance.Play(3, 0.5f, 0.01f, 0.05f);

        while (ActionEffect.instance.play)
        {
            yield return null;
        }

        controller.target.Default();
        controller.currentEnemy.Default();

        OnExit(controller);
    }
}
