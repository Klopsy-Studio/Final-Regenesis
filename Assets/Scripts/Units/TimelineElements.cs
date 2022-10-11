using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeLineTypes
{
    Null,
    PlayerUnit,
    EnemyUnit,
    Events,
    Items,
}
public enum TimelineVelocity
{
    None,
    Quick,
    Normal,
    Slow,
    VerySlow,

}
public abstract class TimelineElements : MonoBehaviour
{
    protected int actionsPerTurn;
    public virtual int ActionsPerTurn
    {
        get { return actionsPerTurn; }
        set { actionsPerTurn = value; }
    }


    protected TimeLineTypes timelineTypes;
    public TimeLineTypes TimelineTypes { get { return timelineTypes; } }
    [Header("Timelines variables")]
    protected TimelineVelocity timelineVelocity = TimelineVelocity.Normal;
    public virtual TimelineVelocity TimelineVelocity
    {
        get { return timelineVelocity; }
        set { timelineVelocity = value; }
    }

    public float fTimelineVelocity;
    public float timelineFill;
    protected const float timelineFull = 100;
    public bool isTimelineActive;

    public Sprite timelineIcon;
    public float GetActionBarPosition()
    {
        return Mathf.Clamp01(timelineFill / timelineFull);
    }

    public abstract bool UpdateTimeLine();
}
