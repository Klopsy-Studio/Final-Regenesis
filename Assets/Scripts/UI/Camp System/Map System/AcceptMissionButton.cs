using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AcceptMissionButton : MonoBehaviour, IPointerClickHandler
{

    [HideInInspector] public LevelData mission;
    [HideInInspector] public MissionInfoPanel missionInfoPanel;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(mission != null)
        {
            GameManager.instance.currentMission = mission;
            missionInfoPanel.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Accept Mission is null");
        }
       
    }
}
