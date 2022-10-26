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
        List<Tile> tiles = ability.ShowAttackRange(controller.currentEnemy.tile.GetDirections(controller.target.tile), controller);
        AudioManager.instance.Play("MonsterAttack");
        controller.battleController.board.SelectAttackTiles(tiles);


        ability.UseAbility(controller.target, controller.currentEnemy, controller.battleController);

        
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

        while (ActionEffect.instance.recovery)
        {
            yield return null;
        }

        
        controller.battleController.board.DeSelectDefaultTiles(tiles);
        controller.target.Default();

        controller.validAbilities.Clear();
        OnExit(controller);
    }
}
