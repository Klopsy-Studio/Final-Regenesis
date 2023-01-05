using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForgeManager : MonoBehaviour
{
    [SerializeField] WeaponUpgradeSystem weaponUpgradeSystem;
    [SerializeField] Transform transformContent;
    [SerializeField] GameObject treePrefab;
    [SerializeField] GameObject weaponSlotPrefab;

    public WeaponPanelInfo weaponPanelInfo;

    [SerializeField] MaterialRequiredSlot[] materialsRequiredSlot;

    private void Start()
    {
        CreateDisplay();
    }
    void CreateDisplay()
    {
        //The first FOR is used to create each weapon tree
        for (int i = 0; i < weaponUpgradeSystem.allWeaponsTrees.Length; i++)
        {
            var weaponTree = Instantiate(treePrefab, Vector3.zero, Quaternion.identity, transformContent);
            var weaponTreeTemplate = weaponTree.GetComponent<WeaponTreeTemplate>();
            weaponTreeTemplate.treeNameText.SetText(weaponUpgradeSystem.allWeaponsTrees[i].TreeName);

            //The seconf FOR is used to create the weapon slot inside of the weapon tree
            for (int w = 0; w < weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade.Length; w++)
            {
                var weaponSlot = Instantiate(weaponSlotPrefab, Vector3.zero, Quaternion.identity, weaponTreeTemplate.contentTransform);
                //weaponSlot.GetComponent<WeaponInfoTemplate>().weaponName.SetText(weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w].itemName);
                weaponSlot.GetComponent<WeaponInfoTemplate>().SetWeaponInfo(weaponUpgradeSystem.allWeaponsTrees[i].weaponUpgrade[w], this);
            }
        }
    }

    public void UpdateMaterialRequiredPanel(WeaponUpgrade _weaponUpgrade)
    {

        for (int i = 0; i < _weaponUpgrade.materialsRequired.Length; i++)
        {
            var materialRequired = _weaponUpgrade.materialsRequired[i];
            //materialsRequiredSlot[i].sprite = materialRequired.monsterMaterial.sprite;
            materialsRequiredSlot[i].SetUpMaterialRequiredSlot(materialRequired);
        }
    }


}

[System.Serializable]
public class WeaponPanelInfo
{
    public TextMeshProUGUI weaponDamage;
    public TextMeshProUGUI weaponRange;

    public void UpdatePanelInfo(WeaponInfoTemplate _weaponInfoTemplate)
    {
        weaponDamage.SetText(_weaponInfoTemplate.WeaponDamage.ToString());
        weaponRange.SetText(_weaponInfoTemplate.WeaponRange.ToString());
    }
}
