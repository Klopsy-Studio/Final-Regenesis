using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEvent : RealTimeEvents
{
    [SerializeField] GameObject windEffect;
    List<Unit> units;

    protected override void Start()
    {
        base.Start();
        windEffect.SetActive(false);
    }

    public override void ApplyEffect()
    {
        windEffect.SetActive(true);
        timelineFill = 10;
        units = battleController.unitsInGame;
        foreach (var unit in units)
        {
            if (unit.isInAction) { continue; }
            Movement mover = unit.GetComponent<Movement>();
            mover.PushUnit(Directions.North, 1, Board);
        }
        fTimelineVelocity = 10;
        Invoke("DeactivateWindEffect", 1);
    }

    void DeactivateWindEffect()
    {
        windEffect.SetActive(false);
    }

   
}
