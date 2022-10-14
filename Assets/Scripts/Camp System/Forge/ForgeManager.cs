using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ForgeManager : MonoBehaviour
{
   
    

    [SerializeField] WeaponUpgradeSystem weaponUpgradeSystem;
    public int coins;
    public Text coinUI;
    [SerializeField] WeaponInfoTemplate[] weaponInfo;
  

    public Text kitNameTxt;
    public Text costTxt;
    public Text moveRangeTxt;
    public Text elementTxt;
    public Text powerTxt;
    public Text defenseTxt;
    public Text criticTxt;
    public Text elementalDefenseTxt;
   

    // Start is called before the first frame update
    void Start()
    {
       
        for (int i = 0; i < weaponUpgradeSystem.allWeaponsTrees.Length; i++)
        {
            for (int w = 0; w < weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade.Length; w++)
            {
                weaponInfo[w].gameObject.SetActive(true);
            }
          
        }

        coinUI.text = coins.ToString();
        LoadPanels();

        //CheckPurchaseable();
    }
    public void AddCoins()
    {
        coins += 10;
        coinUI.text = coins.ToString();
        //CheckPurchaseable();
    }

    //public void PurchaseItem(int itemIndex)
    //{
    //    if (coins >= shopItems[itemIndex].baseCost)
    //    {
    //        coins = coins - shopItems[itemIndex].baseCost;
    //        coinUI.text = coins.ToString();
    //        CheckPurchaseable();
    //        //UNLOCK ITEM;
    //    }
    //}

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
    public void Purchase(Weapons weapon)
    {
        GameManager.instance.equipmentInventory.AddItem(weapon);
    }

    public void LoadPanels()
    {
     
        for (int i = 0; i < weaponUpgradeSystem.allWeaponsTrees.Length; i++)
        {
          
            for (int w = 0; w < weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade.Length; w++)
            {
                //weaponPanel[w].costTxt.text = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].coins.ToString();
                weaponInfo[w].weapon = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].weapon;
                weaponInfo[w].kitName = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].itemName;
                weaponInfo[w].cost = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].cost.ToString();
                weaponInfo[w].moveRange = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].weapon.range.ToString();
                weaponInfo[w].element = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].weapon.ElementPower.ToString();
                weaponInfo[w].power = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].weapon.Power.ToString();
                weaponInfo[w].defense = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].weapon.Defense.ToString();
                weaponInfo[w].critic = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].weapon.CriticalPercentage.ToString();
                weaponInfo[w].elementalDefense = weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].weapon.Elements_Effectiveness.ToString();
            }
        }
    }

    public void fillKitName(Text txt)
    {
        kitNameTxt.text = txt.text;
    }

}
