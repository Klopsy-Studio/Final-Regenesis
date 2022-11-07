using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] ZoneButton[] zoneButtons;
    [SerializeField] GameObject[] mapLists;
    [SerializeField] DisplayMapContainers[] displayMapContainerList;
    public MissionInfoPanel missionInfoPanel;
    public AcceptMissionButton acceptMissionButton;
    private void Start()
    {
        missionInfoPanel.gameObject.SetActive(false);
        foreach (var button in zoneButtons)
        {
            button.FillVariables(this);
        }

        foreach (var displayMapContainerList in displayMapContainerList)
        {
            displayMapContainerList.mapManager = this;
            displayMapContainerList.CreateMissionContainers();
        }
    }
    public void OpenMapPanelList(int mapSelectorId)
    {
        
        foreach (var map in mapLists)
        {
            map.SetActive(false);
        }

        mapLists[mapSelectorId].SetActive(true);
    }


   
}
