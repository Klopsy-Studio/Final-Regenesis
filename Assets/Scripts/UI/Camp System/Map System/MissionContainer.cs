using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Security;

//[CreateAssetMenu(menuName = "Mission/Mission Container UI")]
public class MissionContainer : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] MissionInfoPanel missionInfoPanel;
    [SerializeField] Text nameText; 
    public LevelData levelData;
    string missionName;
    MapManager mapManager;

    //public MissionContainer[] UnlockableMissions;

    bool isNew;
   

    [SerializeField] string zone;
    [SerializeField] string hazard;
    [SerializeField] string otherCreature;
    [SerializeField] string money;
    [SerializeField] string items;
    public void FillVariables(MapManager _mapManager)
    {
        missionName = levelData.missionName;
        nameText.text = missionName;
        mapManager = _mapManager;
        missionInfoPanel = mapManager.missionInfoPanel;
        mapManager.allMissionsDictionary.Add(levelData, this );
        mapManager.allMisionsList.Add(this);

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        missionInfoPanel.gameObject.SetActive(true);
        mapManager.acceptMissionButton.missionInfoPanel = missionInfoPanel;
        mapManager.acceptMissionButton.mission = levelData;
    }

    public void AcceptMission()
    {
        GameManager.instance.currentMission = levelData;
    }
}
