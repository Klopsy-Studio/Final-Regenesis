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
    [SerializeField] GameObject missionInfoPanel;
    [SerializeField] Text nameText; 
    public LevelData levelData;
    string missionName;
    MapManager mapManager;

    bool isNew;
    bool isCompleted;

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
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        missionInfoPanel.SetActive(true);
        GameManager.instance.currentMission = levelData;
    }

    public void AcceptMission()
    {
        GameManager.instance.currentMission = levelData;
    }
}
