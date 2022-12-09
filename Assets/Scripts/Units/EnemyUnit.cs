using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{

    [Header("Monster Stats")]
    public float criticalPercentage;
    public float power;
    public float elementPower;
    [Header("Abilities")]
    public Abilities[] abilities;
    public Unit target;
    [SerializeField] WeaponElement monsterAttackElement;
    public WeaponElement MonsterAttackElement { get { return monsterAttackElement; } }

    [SerializeField] WeaponElement monsterDefenseElement;
    public WeaponElement MonsterDefenseElement { get { return monsterDefenseElement; } }

    //Enemy Stats
    float stunThreshold = 100;

    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite reactSprite;

    [SerializeField] MonsterController controller;
    List<Tile> monsterSpace;
    public float StunThreshold
    {
        get { return stunThreshold; }
        private set { stunThreshold = value; }
    }
    
    public void BeginAnimation()
    {
        controller.animPlaying = true;
    }

    public void EndAnimation()
    {
        controller.animPlaying = false;
    }

    protected override void Start()
    {
        base.Start();
        //timelineFill = Random.Range(0, 3);

        timelineFill = 30;
        timelineTypes = TimeLineTypes.EnemyUnit;
        health = maxHealth;
    }

    
    public override bool ReceiveDamageStun(float damage, float StunDMG)
    {
        StunThreshold -= StunDMG;
        if (StunThreshold <= 0)
        {
            timelineFill -= 30;
            StunThreshold = 100;

        }
        return base.ReceiveDamageStun(damage, StunDMG);     
    }

    public override void Damage()
    {
        base.Damage();
        sprite.sprite = reactSprite;
    }

    public override void Default()
    {
        base.Default();
        sprite.sprite = defaultSprite;
    }
 
  
    public void UpdateMonsterSpace(Board board)
    {
        if(monsterSpace != null)
        {
            foreach(Tile t in monsterSpace)
            {
                t.occupied = false;
                t.OnUnitLeave();
            }

            monsterSpace.Clear();
        }

        SquareAbilityRange monsterRange = GetComponent<SquareAbilityRange>();

        monsterSpace = monsterRange.GetTilesInRange(board);

        foreach (Tile t in monsterSpace)
        {
            t.occupied = true;
            t.OnUnitArriveMonster(this);
        }
    }

    public List<Tile> GiveMonsterSpace(Board board)
    {

        SquareAbilityRange monsterRange = GetComponent<SquareAbilityRange>();
        monsterRange.squareReach = 1;

        return monsterRange.GetTilesInRange(board);
    }


    public override void Die(BattleController battleController)
    {
        controller.monsterAnimations.SetBool("death", true);
        isDead = true;
        AudioManager.instance.Play("MonsterDeath");
    }

    public override bool UpdateTimeLine()
    {
        if (!stunned)
        {
            if (timelineFill >= timelineFull)
            {
                return true;
            }

            timelineFill += fTimelineVelocity * Time.deltaTime;
            return false;
        }

        else
        {
            timeStunned -= Time.deltaTime;

            if (timeStunned <= 0)
            {
                timelineVelocity = previousVelocity;
                SetCurrentVelocity();
                stunned = false;
                timeStunned = originalTimeStunned;
               
            }

            return false;
        }
    }
}
