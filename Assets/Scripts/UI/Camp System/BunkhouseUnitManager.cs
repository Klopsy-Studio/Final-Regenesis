using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunkhouseUnitManager : MonoBehaviour
{

    public delegate void ClickAction();
    public static event ClickAction changeUnitWeaponID;

    public DisplayEquipmentBunkhouse openSelectWeaponPanel;
    public Text movimiento;
    public Text defensa;
    public Text elementEffectiveness;
    public Text poder;
    public Text critico;
    public Text elemento;


  
    public void FillUnitVariables(int id)
    {
        
       
        var unitProfile = GameManager.instance.unitProfilesList[id];
        openSelectWeaponPanel.SetUnitProfileID(id);
        if (changeUnitWeaponID != null) changeUnitWeaponID();
        movimiento.text = unitProfile.unitWeapon.originalRange.ToString();
        defensa.text = unitProfile.unitWeapon.Defense.ToString();
        //elementEffectiveness.text = unitProfile.unitWeapon.Elements_Effectiveness;
        poder.text = unitProfile.unitWeapon.Power.ToString();
        critico.text = unitProfile.unitWeapon.CriticalPercentage.ToString();
        elemento.text = unitProfile.unitWeapon.ElementPower.ToString();

    }

    public void OpenSelectWeapon()
    {
        if (openSelectWeaponPanel != null)
        {
          
            openSelectWeaponPanel.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("OpenSelectWeaponPanel es null");
        }
    }
}
