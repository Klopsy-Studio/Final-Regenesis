using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMapContainers : MonoBehaviour
{
    [SerializeField] MissionList missionList;
    [SerializeField] GameObject missionPrefab;
    [HideInInspector] public MapManager mapManager;
    public List<MissionContainer> missionContainerList = new List<MissionContainer>();
 
    public void CreateMissionContainers()
    {
        for (int i = 0; i < missionList.missions.Count; i++)
        {
            var mission = missionList.missions[i];

            var obj = Instantiate(missionPrefab, Vector3.zero, Quaternion.identity, transform);
            var missionContainer = obj.GetComponent<MissionContainer>();
            missionContainer.levelData = mission;
            missionContainer.FillVariables(mapManager);
         
            missionContainerList.Add(missionContainer);
        }
    }
}
