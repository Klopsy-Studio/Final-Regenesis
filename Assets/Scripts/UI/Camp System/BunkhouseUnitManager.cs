using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BunkhouseUnitManager : MonoBehaviour
{

    public delegate void ClickAction();
    public static event ClickAction changeUnitWeaponID;

    public DisplayEquipmentBunkhouse openSelectWeaponPanel;



    public SetWeaponInfoText[] weaponsInfo;

    [Header("Unit Panel Info")]
    public Image weaponIMG;
    public Text movimiento;
    public Text defensa;
    public Text defElemental;
    public Text poder;
    public Text critico;
    public Text ataqElemental;

    private void Start()
    {
        UpdateDefaultWeaponPanel();
    }
    void UpdateDefaultWeaponPanel()
    {



        for (int i = 0; i < GameManager.instance.unitProfilesList.Length; i++)
        {
            weaponsInfo[i].SetWeaponText(GameManager.instance.unitProfilesList[i].unitWeapon);
        }
    }
  
    public void FillUnitVariables(int id)
    {
        
       
        var unitProfile = GameManager.instance.unitProfilesList[id];
      
        openSelectWeaponPanel.SetUnitProfileID(id);
        if (changeUnitWeaponID != null) changeUnitWeaponID();
        weaponIMG.sprite = unitProfile.unitWeapon.Sprite;
        movimiento.text = unitProfile.unitWeapon.originalRange.ToString();
        defensa.text = unitProfile.unitWeapon.Defense.ToString();
        defElemental.text = unitProfile.unitWeapon.WeaponDefenseElement.ToString();
        poder.text = unitProfile.unitWeapon.Power.ToString();
        critico.text = unitProfile.unitWeapon.CriticalPercentage.ToString();
        ataqElemental.text = unitProfile.unitWeapon.ElementPower.ToString();

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

[System.Serializable]
public class SetWeaponInfoText
{
    public string unitName;
    public Image weaponIMG;
    public TextMeshProUGUI range;
    public TextMeshProUGUI def;
    public TextMeshProUGUI eleDef;
    public TextMeshProUGUI power;
    public TextMeshProUGUI crit;
    public TextMeshProUGUI elePower;
    public void SetWeaponText(Weapons weapon)
    {
        weaponIMG.sprite = weapon.Sprite;
        range.SetText(weapon.originalRange.ToString());
        def.SetText(weapon.Defense.ToString());
        eleDef.SetText(weapon.WeaponDefenseElement.ToString());
        power.SetText(weapon.Power.ToString());
        crit.SetText(weapon.CriticalPercentage.ToString());
        elePower.SetText(weapon.WeaponDefenseElement.ToString());
    }
}
