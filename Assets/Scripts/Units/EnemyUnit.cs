using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{

    [Header("Range Variables")]
    public int aggroRange;
    public int closeRange;
    public int longRange;
    public int randomRange;

    [Header("Abilities")]
    public Abilities[] abilities;
    public Unit target;
    [SerializeField] E_Effectiveness elements_Effectiveness;
    public E_Effectiveness Elements_Effectiveness { get { return elements_Effectiveness; } }

    //Enemy Stats
    float stunThreshold = 100;

    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite reactSprite;
    public float StunThreshold
    {
        get { return stunThreshold; }
        private set { stunThreshold = value; }
    }
    

    protected override void Start()
    {
        base.Start();
        //timelineFill = Random.Range(0, 3);

        timelineFill = 90;
        timelineTypes = TimeLineTypes.EnemyUnit;
        health.baseValue = maxHealth.baseValue;

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

    public override void React()
    {
        base.React();
        sprite.sprite = reactSprite;
    }

    public override void Default()
    {
        base.Default();
        sprite.sprite = defaultSprite;
    }

}
