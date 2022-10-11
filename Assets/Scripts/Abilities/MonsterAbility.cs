using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Ability/New Monster Ability")]
public class MonsterAbility : ScriptableObject
{
    public EffectType typeOfAbility;

    [Header("Attack Range")]
    [SerializeField] List<RangeData> attackRange;

    [Header("Damage")]
    //Variables relacionado con da�o
    public float initialDamage;
    float finalDamage;

    [Range(0.1f, 1f)]
    [SerializeField] public float abilityModifier; //CAMBIAR ESTA VARIABLE A PUBLICA Y HACER QUE SEA UN SLIDE ENTRE 0 A 1 

    [Header("Heal")]
    //Si la habilidad es de curaci�n, se utilizan estas variables
    [SerializeField]  public float initialHeal;
    float finalHeal;

    [Header("Buff")]
    //Si la habilidad es un bufo, se usar� esto
    [SerializeField]  public float initialBuff;

    [Header("Debuff")]
    //Si la habilidad es de debuffo, se usar� esto
    [SerializeField] public float initialDebuff;


    public bool CheckIfAttackIsValid(MonsterController monster)
    {
        List<Tile> attackTiles = new List<Tile>();

        foreach (RangeData r in attackRange)
        {
            switch (r.range)
            {
                case TypeOfAbilityRange.LineAbility:
                    LineAbilityRange lineRange = monster.GetRange<LineAbilityRange>();
                    lineRange.AssignVariables(r.lineDir, r.lineLength);

                    monster.battleController.board.SelectAttackTiles(lineRange.GetTilesInRange(monster.battleController.board));
                    if (CheckForUnits(lineRange.GetTilesInRange(monster.battleController.board)))
                    {
                        return true;
                    }
                    break;
                case TypeOfAbilityRange.Side:
                    SideAbilityRange sideRange = monster.GetRange<SideAbilityRange>();
                    sideRange.AssignVariables(r.sideDir, r.sideReach, r.sideLength);

                    //monster.battleController.board.SelectAttackTiles(sideRange.GetTilesInRange(monster.battleController.board));
                    if (CheckForUnits(sideRange.GetTilesInRange(monster.battleController.board)))
                    {
                        return true;
                    }
                    break;
                case TypeOfAbilityRange.Cross:

                    CrossAbilityRange crossRange = monster.GetRange<CrossAbilityRange>();
                    crossRange.AssignVariables(r.crossLength);
                    if (CheckForUnits(crossRange.GetTilesInRange(monster.battleController.board)))
                    {
                        return true;
                    }
                    break;
                default:
                    break;
            }
        }

        return false;
    }

    public List<Tile> GetAttackTiles(MonsterController monster)
    {
        List<Tile> retValue = new List<Tile>();
        foreach (RangeData r in attackRange)
        {
            switch (r.range)
            {
                case TypeOfAbilityRange.LineAbility:
                    LineAbilityRange lineRange = monster.GetRange<LineAbilityRange>();
                    lineRange.AssignVariables(r.lineDir, r.lineLength);
                    List<Tile> lineTiles = lineRange.GetTilesInRange(monster.battleController.board);
                    AddTilesToList(retValue, lineTiles);
                    break;
                case TypeOfAbilityRange.Side:
                    SideAbilityRange sideRange = monster.GetRange<SideAbilityRange>();
                    sideRange.AssignVariables(r.sideDir, r.sideReach, r.sideLength);
                    List<Tile> sideTiles = sideRange.GetTilesInRange(monster.battleController.board);
                    AddTilesToList(retValue, sideTiles);
                    break;

                case TypeOfAbilityRange.Cross:
                    CrossAbilityRange crossRange = monster.GetRange<CrossAbilityRange>();
                    crossRange.AssignVariables(r.crossLength);
                    List<Tile> crossTiles = crossRange.GetTilesInRange(monster.battleController.board);
                    AddTilesToList(retValue, crossTiles);
                    break;
                default:
                    break;
            }
        }

        return retValue;
    }
    public bool CheckForUnits(List<Tile> tileList)
    {
        foreach(Tile t in tileList)
        {
            if(t.content != null)
            {
                if(t.content.GetComponent<PlayerUnit>() != null)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public List<PlayerUnit> ReturnPossibleTargets(MonsterController monster)
    {
        List<Tile> tiles = GetAttackTiles(monster);
        List<PlayerUnit> units = new List<PlayerUnit>();

        foreach(Tile t in tiles)
        {
            if(t.content != null)
            {
                if(t.content.GetComponent<PlayerUnit>() != null)
                {
                    units.Add(t.content.GetComponent<PlayerUnit>());
                }
            }
        }

        return units;
    }
    public void AddTilesToList(List<Tile> tileHolder, List<Tile> newTiles)
    {
        foreach (Tile t in newTiles)
        {
            if (!tileHolder.Contains(t))
            {
                tileHolder.Add(t);
            }
        }
    }
}
