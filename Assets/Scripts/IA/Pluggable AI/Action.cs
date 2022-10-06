using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : ScriptableObject
{
    protected virtual void Act(MonsterController controller)
    {

    }
    protected virtual void OnExit(MonsterController controller)
    {
        controller.currentState.CheckTransitions(controller);
        controller.currentState.UpdateState(controller);
    }
}
