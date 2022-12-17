using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] ShopItemContainer shopItemContainer;
    [SerializeField] GameObject slotPrefab;
    [SerializeField] Transform transformContent;

    private void Start()
    {
        CreateDisplay();
    }
    private void CreateDisplay()
    {
        for (int i = 0; i < shopItemContainer.shopItems.Length; i++)
        {
            var itemShop = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity, transformContent);
            itemShop.transform.GetChild(0).GetComponent<Image>().sprite = shopItemContainer.shopItems[i].item.iconSprite;
            itemShop.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(shopItemContainer.shopItems[i].name);
            itemShop.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(shopItemContainer.shopItems[i].pointCosts.ToString());


        }
    }


}
