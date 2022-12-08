using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSystem : MonoBehaviour
{
   
    [SerializeField] DropsContainer dropContainer;
    public List<MonsterMaterial> monsterMaterials = new List<MonsterMaterial>();
    public DroppedMaterials dropMaterials = new DroppedMaterials();
    int ciclo = 0;


    public void DropMaterials()
    {
        Debug.Assert(dropContainer != null, "Drop container is null", gameObject);
       
        if(dropContainer.containerList.Count == 0)
        {
            Debug.Assert(dropContainer.containerList.Count > 0, "drop container list is void. Add info", gameObject);
            return;
        }
        monsterMaterials.Clear();

        while (monsterMaterials.Count < 3)
        {
            CalculaNumberOfMatDrops();
            ciclo++;
            Debug.Log("numero de ciclos: " + ciclo);
        }

        AddToMaterialInventory();
    }

    void CalculaNumberOfMatDrops()
    {
       
     
        monsterMaterials.Clear();
        foreach (var drop in dropContainer.containerList)
        {
            int testInt = 0;
            int rolls = (int)drop.monsterMaterial.rarity;
            Debug.Log("rolls " + rolls);
            for (int i = 0; i < rolls; i++)
            {
                var random =  Random.value * 100;
                bool isDropSuccessful = random <= drop.dropProbabilty;
                testInt++;
                Debug.Log("valor random " + random);
                //Debug.Log("valor aleatorio" + random + "testInt: " + testInt);
                if (isDropSuccessful)
                {
                    monsterMaterials.Add(drop.monsterMaterial);
                    //GameManager.instance.materialInventory.AddMonsterMaterial(drop.monsterMaterial, 1);
                }
            }



        }

        foreach (var materials in monsterMaterials)
        {
            dropMaterials.AddMonsterMaterial(materials, 1);
        }

        
    }

    void AddToMaterialInventory()
    {
        foreach (var material in monsterMaterials)
        {
            GameManager.instance.materialInventory.AddMonsterMaterial(material, 1);
        }

      
    }

    
}

public class DroppedMaterials
{
    public List<MonsterMaterialSlot> materialContainer = new List<MonsterMaterialSlot>();
    public void AddMonsterMaterial(MonsterMaterial _material, int _amount)
    {
        bool hasMaterial = false;
        for (int i = 0; i < materialContainer.Count; i++)
        {
            if (materialContainer[i].material == _material)
            {
                materialContainer[i].AddAmount(_amount);
                hasMaterial = true;
                break;
            }
        }
        if (!hasMaterial)
        {
            materialContainer.Add(new MonsterMaterialSlot(_material, _amount));
        }
    }
}

