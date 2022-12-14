using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Ability Sequences")]

public class AbilitySequence : ScriptableObject
{
    public Abilities ability;
    public PlayerUnit user;
    public Weapons weapon;
    public bool playing;


    [Header("Bow Variables")]
    public bool extraAttack;
    public virtual IEnumerator Sequence(GameObject target, BattleController controller)
    {
        playing = true;
        yield return null;
        playing = false;
    }
    
    public virtual IEnumerator Sequence(GameObject target, BattleController controller, bool extraAttack)
    {
        playing = true;
        yield return null;
        playing = false;
    }

    private void OnEnable()
    {
        extraAttack = false;
        playing = false;
    }
}
