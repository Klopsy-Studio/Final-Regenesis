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

   /* [HideInInspector]*/ public ShopItemTemplate shopItemSelected;
    public SetShopItemInfoPanelText itemPanelInfo;
   

    public BuyItemPanel buyItemPanel;
    

    private void Start()
    {
    
        itemPanelInfo.GO.SetActive(false);
        buyItemPanel.GO.SetActive(false);
        CreateDisplay();
    }
    private void CreateDisplay()
    {
        for (int i = 0; i < shopItemContainer.shopItems.Length; i++)
        {
            var itemShop = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity, transformContent);
            var itemInfo = shopItemContainer.shopItems[i];
            itemShop.GetComponent<ShopItemTemplate>().SetItemInfo(itemInfo, this);
           

        }
    }

    public void IncreaseAmount() //UnityButton function calls this method
    {
        itemPanelInfo.IncreaseAmount();
    }

    public void DecreaseAmount()//UnityButton function calls this method
    {
        itemPanelInfo.DecreaseAmount();
    }

    public void OpenBuyItemPanel()//UnityButton function calls this method
    {
        if (itemPanelInfo.itemAmount <= 0) return;
        buyItemPanel.GO.SetActive(true);
        buyItemPanel.SetBuyPanelInfo(itemPanelInfo);
    }
}

[System.Serializable]
public class SetShopItemInfoPanelText
{
    public GameObject GO;

    [field:SerializeField] public TextMeshProUGUI ItemName { get; private set;}
    [field:SerializeField] public Image ItemImage { get; private set;}
    [SerializeField] TextMeshProUGUI itemAmountTxT;
    [SerializeField] TextMeshProUGUI itemCostTxT;

    public int itemAmount;
    public int itemCost;

    public ShopItemInfo itemInfo;

    public void SetItemInfo(ShopItemTemplate _shopItem)
    {
        ItemName.SetText(_shopItem.name);
        ItemImage.sprite = _shopItem.item.consumable.itemSprite;
        ItemImage.SetNativeSize();
        itemInfo = _shopItem.item;

        ResetPanelInfo();
    }

    public void IncreaseAmount()
    {
        itemAmount++;
        itemCost = itemAmount * itemInfo.pointCosts;
        itemAmountTxT.SetText("AMOUNT: " + itemAmount);
        itemCostTxT.SetText("TOTAL PRICE: " + itemCost + "p");
    }

    public void DecreaseAmount()
    {
        if (itemAmount <= 0) return;
        itemAmount--;
        itemCost = itemAmount * itemInfo.pointCosts;
        itemAmountTxT.SetText("AMOUNT: " + itemAmount);
        itemCostTxT.SetText("TOTAL PRICE: " + itemCost + "p");
    }

    void ResetPanelInfo()
    {
        itemAmount = 0;
        itemCost = 0;
        itemAmountTxT.SetText("AMOUNT: " + itemAmount);
        itemCostTxT.SetText("TOTAL PRICE: " + itemCost + "p");
    }

   
}

[System.Serializable]
public class BuyItemPanel
{
    public GameObject GO;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemAmount;
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemTotalCostTxT;
    [SerializeField] TextMeshProUGUI currentPointsTxT;
    int itemTotalCost;
    int currentPoints;

    public void SetBuyPanelInfo(SetShopItemInfoPanelText _itemInfoPanel)
    {
       
        itemName.SetText(_itemInfoPanel.ItemName.text);
        itemImage.sprite = _itemInfoPanel.ItemImage.sprite;
        itemTotalCost = _itemInfoPanel.itemCost;
        itemTotalCostTxT.SetText(itemTotalCost.ToString());
        itemAmount.SetText("x "+_itemInfoPanel.itemAmount.ToString());

        currentPoints = 0;
        currentPointsTxT.SetText(currentPoints.ToString());


    }

}
