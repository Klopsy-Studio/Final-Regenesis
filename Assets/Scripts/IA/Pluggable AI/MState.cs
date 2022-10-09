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

    [SerializeField] bool isDecisionRandom;
    

    public void UpdateState(MonsterController controller)
    {
        Debug.Log("State Updated");
        actions.Act(controller);
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
        foreach(Decision d in transition.decision)
        {
            d.CheckConditionalStates(controller);
        }
        if (!isDecisionRandom)
        {
            if(transition.decision.Length > 1)
            {
                for (int i = 0; i < transition.decision.Length; i++)
                {
                    bool decisionSucceeded = transition.decision[i].Decide(controller);

                    if (decisionSucceeded)
                    {
                        if (!transition.decision[i].multipleStates)
                        {
                            controller.TransitionToState(transition.decision[i].trueState[0]);
                        }

                        else
                        {
                            int random = Random.Range(0, transition.decision[i].trueState.Count);
                            controller.TransitionToState(transition.decision[i].trueState[random]);
                        }
                    }
                }
            }

            else
            {
                if (transition.decision[0].Decide(controller))
                {
                    if (!transition.decision[0].multipleStates)
                    {
                        controller.TransitionToState(transition.decision[0].trueState[0]);
                    }
                    else
                    {
                        int random = Random.Range(0, transition.decision[0].trueState.Count);
                        controller.TransitionToState(transition.decision[0].trueState[random]);
                    }
                }
                else
                {
                    if (!transition.decision[0].multipleStates)
                    {
                        controller.TransitionToState(transition.decision[0].falseState[0]);
                    }
                    else
                    {
                        int random = Random.Range(0, transition.decision[0].falseState.Count);
                        controller.TransitionToState(transition.decision[0].falseState[random]);
                    }
                }
            }
        }

        else
        {
            //This pack of code is random in a way that chooses randomly between all the decisions, not random in a random state way

            //List<Decision> validDecisions = new List<Decision>();

            //for (int i = 0; i < transition.decision.Length; i++)
            //{
            //    bool decisionSucceeded = transition.decision[i].Decide(controller);

            //    if (decisionSucceeded)
            //    {
            //        validDecisions.Add(transition.decision[i]);
            //    }
            //}

            
            //int randomNumber = Random.Range(0, validDecisions.Count);
            //controller.TransitionToState(transition.decision[randomNumber].trueState);


            //Version with actually random transitions to different States
            //Only use one true and false state with this version if possible
            if(transition.decision.Length > 1)
            {
                int random = Random.Range(0, 2);

                if(random == 0)
                {
                    controller.TransitionToState(transition.decision[0].trueState[0]);
                }
                else if(random == 1)
                {
                    controller.TransitionToState(transition.decision[1].trueState[0]);
                }
            }
        }
    }
}
