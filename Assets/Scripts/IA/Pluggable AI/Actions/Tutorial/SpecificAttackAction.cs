using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Specific Attack")]
public class SpecificAttackAction : Action
{
    public override void Act(MonsterController controller)
    {
        controller.CallCoroutine(Attack(controller, controller.ChooseSpecificAttack()));
    }


    IEnumerator Attack(MonsterController controller, MonsterAbility ability)
    {
        List<Tile> tiles = ability.GetAttackTiles(controller);

        controller.battleController.board.SelectAttackTiles(tiles);

        controller.target.ReceiveDamage(40);
        controller.target.Damage();
        controller.target.DamageEffect();

        controller.currentEnemy.Damage();

        ActionEffect.instance.Play(3, 0.5f, 0.01f, 0.05f);

        while (ActionEffect.instance.play)
        {
            yield return null;
        }

        controller.target.Default();
        controller.currentEnemy.Default();

        controller.validAttack = null;
        OnExit(controller);
    }
}

