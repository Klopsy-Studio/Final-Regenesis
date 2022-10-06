using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public bool isUpdatingState = false;
   
    public MState currentState;

    public MState startState;

    public BattleController battleController;
    public EnemyUnit currentEnemy;
    public Unit currentTarget;
    public List<PlayerUnit> targets;
    public EnemyActions lastAction = EnemyActions.None;
    public Abilities[] abilities;
    public int attactkToUse;
    // Update is called once per frame
    //void Update()
    //{
    //    currentState.UpdateState(this);
    //}


    public void StartMonster()
    {
        currentState.UpdateState(this);
        
    }



    protected void OnDrawGizmos()
    {
        if (currentState != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(transform.position, 2);
        }
    }

    public void TransitionToState(MState nextState)
    {
        if (nextState != currentState)
        {
            currentState = nextState;
        }
    }



    public virtual T GetRange<T>() where T : AbilityRange
    {
        T target = GetComponent<T>();
        if (target == null)
        {
            target = gameObject.AddComponent<T>();
        }

        return target;
    }

    public void CallCoroutine (IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
        Debug.Log("Call");
    }
}
