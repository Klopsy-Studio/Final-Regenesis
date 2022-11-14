using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEvent : RealTimeEvents
{
    [SerializeField] GameObject windEffect;
    List<Unit> units;

    public Directions direction;
    protected override void Start()
    {
        base.Start();
        windEffect.SetActive(false);
    }

    public override void ApplyEffect()
    {
        direction = Directions.North;


        windEffect.SetActive(true);
        timelineFill = 10;
        units = battleController.unitsInGame;
        AudioManager.instance.Play("WindEvent");
        foreach (var unit in units)
        {
            if (unit.isInAction) { continue; }
            Movement mover = unit.GetComponent<Movement>();
            mover.PushUnit(direction, 1, Board);
        }

        fTimelineVelocity = 10;

        //switch (direction)
        //{
        //    case Directions.North:
        //        direction = Directions.East;
        //        break;
        //    case Directions.East:
        //        direction = Directions.South;
        //        break;
        //    case Directions.South:
        //        direction = Directions.West;
        //        break;
        //    case Directions.West:
        //        direction = Directions.North;
        //        break;
        //    default:
        //        break;
        //}
        Invoke("DeactivateWindEffect", 1);
    }

    void DeactivateWindEffect()
    {
        windEffect.SetActive(false);
    }

   
}
