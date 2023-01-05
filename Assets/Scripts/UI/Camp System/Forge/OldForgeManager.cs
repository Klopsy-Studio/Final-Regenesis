using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OldForgeManager : MonoBehaviour
{
    //Prueba
    [SerializeField] WeaponTreeTemplate[] weaponTreeTemplateList;
    [SerializeField] PurchaseForge purchaseForge;
    //------------

    public MonsterMaterial monsterMaterial1;
    public MonsterMaterial monsterMaterial2;
   

    [SerializeField] WeaponUpgradeSystem weaponUpgradeSystem;
    public int coins;
    public Text coinUI;
    [SerializeField] WeaponInfoTemplate[] weaponInfo;

    public WeaponInfoTemplate currentWeaponInfoSelected;

    public Text kitNameTxt;
    public Image weaponImage;
    public Text costTxt;
    public Text moveRangeTxt;
    public Text elementTxt;
    public Text powerTxt;
    public Text defenseTxt;
    public Text criticTxt;
    public Text elementEffectiveness;
    //public MaterialRequirement[] materialRequirement;
    
   

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < weaponTreeTemplateList.Length; i++)
        {
            foreach (var item in weaponTreeTemplateList)
            {
                item.FillVariables(this, purchaseForge);
            }
        }
       
        //for (int i = 0; i < weaponUpgradeSystem.allWeaponsTrees.Length; i++)
        //{
        //    Debug.Log("ee");
        //    for (int w = 0; w < weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade.Length; w++)
        //    {
        //        Debug.Log("RECORRIDO");
        //        weaponInfo[w].gameObject.SetActive(true);
        //    }
          
        //}

        for (int i = 0; i < weaponUpgradeSystem.allWeaponsTrees.Length; i++)
        {
            Debug.Log("ee");
            for (int w = 0; w < weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade.Length; w++)
            {
                weaponTreeTemplateList[i].weaponInfoTemplateList[w].gameObject.SetActive(true);
            }

        }

        coinUI.text = coins.ToString();
        LoadPanels();

        //CheckPurchaseable();
    }
    public void AddCoins()
    {
        coins += 150;
        coinUI.text = coins.ToString();
        //CheckPurchaseable();
        GameManager.instance.materialInventory.AddMonsterMaterial(monsterMaterial1, 30);
        GameManager.instance.materialInventory.AddMonsterMaterial(monsterMaterial2, 50);
    }

    public bool canPurchaseItem()
    {
       //Check if Enough Coins
        if (coins < currentWeaponInfoSelected.cost)
        {
           
            coinUI.text = coins.ToString();
            //CheckPurchaseable();
           
            return false;
        }

        //Check if enoguh materials and amount of materials
        for (int i = 0; i < currentWeaponInfoSelected.materialRequirement.Length; i++)
        {
            if (!currentWeaponInfoSelected.materialRequirement[i].DoIHaveEnoughMaterial(GameManager.instance.materialInventory))
            {
                return false;
            }

        }

        //Check if you have the required weapon
        if (currentWeaponInfoSelected.weaponUpgradeTree.weaponRequired != null)
        {

            
            if (!currentWeaponInfoSelected.weaponUpgradeTree.HasRequiredWeapon(GameManager.instance.equipmentInventory))
            {
                Debug.Log("NO TIENES EL ARMA REQUERIDA");
                return false;
            }
        }
        return true;
  
      
        
    }

    public void CloseForgeManager()
    {
        this.gameObject.SetActive(false);
    }

    public void SelectCurrentWeaponInfo(WeaponInfoTemplate value)
    {
        currentWeaponInfoSelected = value;

    }

    public void UpdateForgeUI()
    {
        coinUI.text = coins.ToString();
    }

    //private void CheckPurchaseable()
    //{


    //    for (int i = 0; i < weaponUpgradeSystem.allWeaponsTrees.Length; i++)
    //    {
    //        for (int w = 0; w < weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade.Length; w++)
    //        {
    //            if (coins >= weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].coins)//If I have enough money
    //            {
    //                myPurchaseButton[w].interactable = true;
    //            }
    //            else
    //            {
    //                Debug.Log("NO PURCHASABLE");
    //                myPurchaseButton[w].interactable = false;
    //            }
    //        }

    //    }
    //}


    public void LoadPanels()
    {
     
        for (int i = 0; i < weaponUpgradeSystem.allWeaponsTrees.Length; i++)
        {
            Debug.Log("LOAD PANEL");
            for (int w = 0; w < weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade.Length; w++)
            {
                weaponTreeTemplateList[i].weaponInfoTemplateList[w].weaponToPurchase = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].weapon;
                weaponTreeTemplateList[i].weaponInfoTemplateList[w].kitName = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].itemName;
                weaponTreeTemplateList[i].weaponInfoTemplateList[w].weaponImg = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].weapon.Sprite;
             
                weaponTreeTemplateList[i].weaponInfoTemplateList[w].moveRange = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].weapon.range.ToString();
                weaponTreeTemplateList[i].weaponInfoTemplateList[w].element = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].weapon.ElementPower.ToString();
                weaponTreeTemplateList[i].weaponInfoTemplateList[w].power = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].weapon.Power.ToString();
                weaponTreeTemplateList[i].weaponInfoTemplateList[w].defense = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].weapon.Defense.ToString();
                weaponTreeTemplateList[i].weaponInfoTemplateList[w].critic = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].weapon.CriticalPercentage.ToString();
                weaponTreeTemplateList[i].weaponInfoTemplateList[w].elementEffectiveness = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].weapon.WeaponAttackElement.ToString();
                weaponTreeTemplateList[i].weaponInfoTemplateList[w].materialRequirement = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].materialsRequired;
                weaponTreeTemplateList[i].weaponInfoTemplateList[w].weaponUpgradeTree = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w];


            }
        }
    }

  

}
