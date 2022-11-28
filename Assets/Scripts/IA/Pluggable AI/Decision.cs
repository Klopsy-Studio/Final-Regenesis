using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decision : ScriptableObject
{
    public bool multipleStates;

    public List<Decision> conditionalStates;

    public List<MState> originalTrueState;
    public List<MState> trueState;
    public List<MState> originalFalseState;
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

    public void OnEnable()
    {
        ResetDecision();
    }


    public void ResetDecision()
    {
        trueState.Clear();
        foreach (MState state in originalTrueState)
        {
            trueState.Add(state);
        }

        falseState.Clear();

        foreach (MState state in originalFalseState)
        {
            falseState.Add(state);
        }
    }
    public void OnDisable()
    {
        
    }

}
