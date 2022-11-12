using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitDeath : TimelineElements
{
    public PlayerUnit unit;

    
    public void Death(BattleController controller)
    {
        unit.Die(controller);
        controller.timelineElements.Remove(this);
        gameObject.SetActive(false);
    }

    public override bool UpdateTimeLine()
    {
        if (timelineFill >= timelineFull)
        {
            return true;
        }

        timelineFill += fTimelineVelocity * Time.deltaTime;

        return false;
    }
}
