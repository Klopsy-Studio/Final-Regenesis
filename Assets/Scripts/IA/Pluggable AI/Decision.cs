using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decision : ScriptableObject
{
    public MState trueState;
    
    public abstract bool Decide(MonsterController controller);

}
