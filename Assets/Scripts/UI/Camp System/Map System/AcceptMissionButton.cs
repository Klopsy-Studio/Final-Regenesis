using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
            GameManager.instance.sceneToLoad = "Battle";
            SceneManager.LoadScene("LoadingScreen");
        }
        else
        {
            Debug.Log("Accept Mission is null");
        }
       
    }
}
