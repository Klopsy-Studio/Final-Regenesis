using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Random Attack")]
public class RandomAttackAction : Action
{
    public override void Act(MonsterController controller)
    {
        controller.CallCoroutine(Attack(controller, controller.ChooseRandomAttack()));
    }


    IEnumerator Attack(MonsterController controller, MonsterAbility ability)
    {
        List<Tile> tiles = ability.GetAttackTiles(controller);
        AudioManager.instance.Play("MonsterAttack");
        controller.battleController.board.SelectAttackTiles(tiles);

        if (controller.target.ReceiveDamage(ability.initialDamage))
        {
            AudioManager.instance.Play("HunterDeath");
        }
        
        controller.target.Damage();
        controller.target.DamageEffect();

        controller.monsterAnimations.SetBool(ability.attackTrigger, true);

        ActionEffect.instance.Play(3, 0.5f, 0.01f, 0.05f);

        while (ActionEffect.instance.play)
        {
            yield return null;
        }

        controller.monsterAnimations.SetBool(ability.attackTrigger, false);
        controller.monsterAnimations.SetBool("idle", true);

        controller.target.Default();
        controller.currentEnemy.Default();

        controller.validAbilities.Clear();
        OnExit(controller);
    }
}
