using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PluggableAI/m_state")]
public class MState : ScriptableObject
{
  
    public Color sceneGizmoColor = Color.grey;
    public Transition transition;
    public Action actions;
    //public Action[] actions;
    

    public void UpdateState(MonsterController controller)
    {
        Debug.Log("STATE AAA");
        actions.Act(controller);
        //DoActions(controller);
    }

    //private void DoActions(TestMonsterController controller)
    //{
    //    for (int i = 0; i < actions.Length; i++)
    //    {
    //        actions[i].Act(controller);
    //    }
    //}

    public void CheckTransitions(MonsterController controller)
    {
        for (int i = 0; i < transition.decision.Length; i++)
        {
            bool decisionSucceeded = transition.decision[i].Decide(controller);
            if (decisionSucceeded)
            {
                Debug.Log("SUCCEEDD");
                controller.TransitionToState(transition.decision[i].trueState);
            }
            
        }
    }
}
