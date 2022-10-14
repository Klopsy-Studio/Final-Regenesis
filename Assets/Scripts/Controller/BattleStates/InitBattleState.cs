using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        board.Load(levelData);
        SpawnUnits();
        yield return null;
        //owner.ChangeState<StartPlayerTurnState>();
        SelectTile(new Point(1, 5));
        owner.ChangeState<TimeLineState>();
    }

    void SpawnUnits()
    {
        System.Type[] components = new System.Type[] { typeof(WalkMovement) };

        for (int i = 0; i < levelData.playerSpawnPoints.Count; i++)
        {
            GameObject instance = Instantiate(owner.heroPrefab) as GameObject;

            AssignUnitData(levelData.unitsInLevel[i], instance.GetComponent<PlayerUnit>());

            instance.GetComponent<PlayerUnit>().profile = levelData.unitsInLevel[i];
            Point p = levelData.playerSpawnPoints.ToArray()[i];

            Unit unit = instance.GetComponent<Unit>();
            unit.Place(board.GetTile(p));
            unit.Match();

            Movement m = instance.AddComponent(components[0]) as Movement;
            m.jumpHeight = 1;


            unitsInGame.Add(unit);

            owner.playerUnits.Add(unit);

            
            //if (unit.UnitType == UnitType.PlayerUnit)
            //{
               
            //}

            //if(unit.UnitType == UnitType.EnemyUnit)
            //{
            //    owner.enemyUnits.Add(unit);
            //}
        }

        owner.unitStatusUI.SpawnUnitStatus(playerUnits);

        for (int i = 0; i < levelData.enemySpawnPoints.Count; i++)
        {
            if (levelData.enemyInLevel == null) break;
            GameObject instance = Instantiate(levelData.enemyInLevel) as GameObject;
            Point p = levelData.enemySpawnPoints.ToArray()[i];

            Unit unit = instance.GetComponent<Unit>();
            unit.Place(board.GetTile(p));
            unit.Match();

            Movement m = instance.AddComponent(components[i]) as Movement;
            m.range = 10;
            m.jumpHeight = 1;

            unitsInGame.Add(unit);

            owner.enemyUnits.Add(unit);
            //if (unit.UnitType == UnitType.PlayerUnit)
            //{
            //    owner.playerUnits.Add(unit);
            //}

            //if (unit.UnitType == UnitType.EnemyUnit)
            //{
            //    owner.enemyUnits.Add(unit);
            //}
        }

       
        foreach (var unit in unitsInGame)
        {
            owner.timelineElements.Add(unit);
        }
        owner.timelineElements.Add(owner.environmentEvent);
    }

    public void AssignUnitData(UnitProfile data, PlayerUnit unit)
    {
        unit.unitPortrait = data.unitPortrait;
        unit.fullUnitPortrait = data.unitFullPortrait;
        unit.weapon = data.unitWeapon;
        unit.unitName = data.unitName;

        unit.unitSprite.sprite = data.unitSprite;
        unit.timelineIcon = data.unitTimelineIcon;

    }
}
