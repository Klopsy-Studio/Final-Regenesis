using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MaterialPointsShopButton : MonoBehaviour, IPointerClickHandler
{
    public Image materialImage;
    public TextMeshProUGUI amountText;
    public int points;
    public MonsterMaterialSlot materialSlot;

    BuyItemPanel buyItemPanel;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Decrease();
        }   
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
            Debug.Log("Right click");
    }

    public void SetMaterial(MonsterMaterialSlot _materialSlot, BuyItemPanel _buyItemPanel)
    {
        materialImage.sprite = _materialSlot.material.sprite;
        amountText.SetText(_materialSlot.amount.ToString());
        materialSlot = _materialSlot;

        buyItemPanel = _buyItemPanel;
    }

    void Decrease()
    {
        materialSlot.amount -= 1;
        amountText.SetText(materialSlot.amount.ToString());
        buyItemPanel.UpdateCurrentPoints(points);
    }
}
