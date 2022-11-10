using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Specific Attack")]
public class SpecificAttackAction : Action
{
    public override void Act(MonsterController controller)
    {
        Debug.Log("Acting");

        controller.CallCoroutine(Attack(controller, controller.ChooseSpecificAttack()));
    }


    IEnumerator Attack(MonsterController controller, MonsterAbility ability)
    {
        Directions dir = controller.currentEnemy.tile.GetDirections(controller.target[Random.Range(0, controller.target.Count)].tile);
        List<Tile> tiles = ability.ShowAttackRange(dir, controller);

        controller.target.Clear();
        foreach(Tile t in tiles)
        {
            if(t.content != null)
            {
                if(t.content.GetComponent<PlayerUnit>() != null)
                {
                    controller.target.Add(t.content.GetComponent<PlayerUnit>());
                }
            }
        }
        AudioManager.instance.Play("MonsterAttack");

        controller.battleController.board.SelectAttackTiles(tiles);

        foreach(PlayerUnit u in controller.target)
        {
            ability.UseAbility(u, controller.currentEnemy, controller.battleController);
            u.Damage();
            u.DamageEffect();
        }
        


        controller.monsterAnimations.SetBool(ability.attackTrigger, true);
        controller.monsterAnimations.SetBool("idle", false);
        ActionEffect.instance.Play(3, 0.5f, 0.01f, 0.05f);

        if(ability.inAbilityEffects != null)
        {
            foreach(Effect e in ability.inAbilityEffects)
            {
                foreach(PlayerUnit u in controller.target)
                {
                    switch (e.effectType)
                    {
                        case TypeOfEffect.PushUnit:
                            e.PushUnit(u, dir, controller.battleController.board);
                            break;
                        default:
                            break;
                    }
                }
                
            }
        }
        while (ActionEffect.instance.play)
        {
            yield return null;
        }
        controller.monsterAnimations.SetBool(ability.attackTrigger, false);
        controller.monsterAnimations.SetBool("idle", true);

        if(ability.postAbilityEffect != null)
        {
            foreach (Effect e in ability.postAbilityEffect)
            {

                switch (e.effectType)
                {
                    case TypeOfEffect.FallBack:
                        e.FallBack(controller.currentEnemy, dir, controller.battleController.board);
                        break;
                    default:
                        break;
                }
            }
        }
        
        while (ActionEffect.instance.recovery)
        {
            yield return null;
        }

        

        controller.battleController.board.DeSelectDefaultTiles(tiles);

        foreach(PlayerUnit u in controller.target)
        {
            u.Default();
        }

        controller.validAttack = null;
        OnExit(controller);
    }
}

