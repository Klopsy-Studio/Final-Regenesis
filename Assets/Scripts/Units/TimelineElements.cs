using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

public enum TimeLineTypes
{
    Null,
    PlayerUnit,
    EnemyUnit,
    Events,
    Items,
    PlayerDeath
}
public enum TimelineVelocity
{
    //None,
    //Quick,
    //VeryQuick,
    //Normal,
    //Slow,
    //VerySlow,


    VerySlow,
    Slow,
    Normal,
    Quick,
    VeryQuick,
    TurboFast,
}
public abstract class TimelineElements : MonoBehaviour
{
    public int actionsPerTurn;
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
        set { timelineVelocity = value;}
    }


    public void SetTimelineVelocityText()
    {
        Debug.Log("b");
        if (iconTimeline.velocityText == null)
        {
            Debug.Log("a");
        }
        
        Debug.Log("entra");
    }
    
    

    public float fTimelineVelocity;
    public float timelineFill;
    protected const float timelineFull = 100;
    public bool isTimelineActive;

    public Sprite timelineIcon;

    public TimelineIconUI iconTimeline;


    public float GetActionBarPosition()
    {
        return Mathf.Clamp01(timelineFill / timelineFull);
    }

    public abstract bool UpdateTimeLine();
}
