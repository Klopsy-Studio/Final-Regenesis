using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decision : ScriptableObject
{
    public bool multipleStates;

    public List<Decision> conditionalStates;
    public List<MState> trueState;
    public List<MState> falseState;
    
    public abstract bool Decide(MonsterController controller);

    public void CheckConditionalStates(MonsterController controller)
    {
        if(conditionalStates != null)
        {
            foreach(Decision d in conditionalStates)
            {
                if (d.Decide(controller))
                {
                    trueState.Add(d.trueState[0]);
                }
            }
        }
    }

}
