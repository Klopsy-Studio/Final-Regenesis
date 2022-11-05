using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoneButton : MonoBehaviour, IPointerClickHandler
{
    public int mapID;
    MapManager mapManager;

    public void FillVariables(MapManager _mapManager)
    {
        mapManager = _mapManager;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
       
        mapManager.OpenMapPanelList(mapID);
    }
}
