using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionInfoPanel : MonoBehaviour
{
    //PARA EL BOTON BUSCAR INFO DE DELEGATES;
    public Text missionName;
    public Text zone;
    public Text hazard;
    public Text otherCreature;
    public Text money;
    public Text item;

    public Button button;

    public void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }
}
