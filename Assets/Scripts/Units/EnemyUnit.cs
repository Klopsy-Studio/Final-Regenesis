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
    [SerializeField] WeaponElement elements_Effectiveness;
    public WeaponElement monsterElement { get { return elements_Effectiveness; } }

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

        timelineFill = 70;
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
            }

            monsterSpace.Clear();
        }

        //Place(board.GetTile(currentPoint));

        SquareAbilityRange monsterRange = GetComponent<SquareAbilityRange>();

        monsterSpace = monsterRange.GetTilesInRange(board);

        foreach (Tile t in monsterSpace)
        {
            t.occupied = true;
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
        AudioManager.instance.Play("MonsterDeath");
        battleController.ChangeState<WinState>();
    }
}
