using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] DropsContainer dropContainer;
    List<MonsterMaterial> monsterMaterials = new List<MonsterMaterial>();
    int ciclo = 0;
    public void DropMaterials()
    {
        while(monsterMaterials.Count < 3)
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
        foreach (var drop in dropContainer.container)
        {
            int testInt = 0;
            int rolls = (int)drop.monsterMaterial.rarity;
            Debug.Log("rolls " + rolls);
            for (int i = 0; i < rolls; i++)
            {
                var random = Random.value * 100;
                bool isDropSuccessful = random <= drop.dropProbabilty;
                testInt++;
                Debug.Log("valor aleatorio" + random + "testInt: " + testInt);
                if (isDropSuccessful)
                {
                    monsterMaterials.Add(drop.monsterMaterial);
                    //GameManager.instance.materialInventory.AddMonsterMaterial(drop.monsterMaterial, 1);
                }
            }

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
