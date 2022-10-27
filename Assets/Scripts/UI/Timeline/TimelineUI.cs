using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineUI : MonoBehaviour
{
    [SerializeField] BattleController battleController;
    [SerializeField] GameObject iconPrefab;
    [SerializeField] RectTransform content;
    [SerializeField] RectTransform removedChildren;
    float barSize;


    [Header("Icon prefabs")]
    [SerializeField] Sprite playerFrame;
    [SerializeField] Sprite enemyFrame;
    [SerializeField] Sprite eventFrame;
    [SerializeField] Sprite eventIcon;
    [SerializeField] Sprite itemFrame;
    [SerializeField] Sprite itemIcon;


    int offset;

    public TimelineIconUI selectedIcon;

    //The bar size. Dependant on size delta. Only works for a static scale object as delta isn't mesured the same way with different anchors.
    private void Start()
    {
        barSize = content.sizeDelta.x;
    }

    //Not Ideal. Would be better to avoid GetComponent entirely. Simplest solution for a 45 minutes project
    private void Update()
    {
        BalanceAmountOf(iconPrefab, content, battleController.timelineElements.Count);
        TimelineIconUI temp;

        for (int i = 0; i < battleController.timelineElements.Count; i++)
        {
            temp = content.GetChild(i).GetComponent<TimelineIconUI>();
            temp.element = battleController.timelineElements[i];

            if (battleController.timelineElements[i].TimelineTypes == TimeLineTypes.PlayerUnit)
            {
                temp.image.sprite = playerFrame;
                temp.element.GetComponent<PlayerUnit>().timelineIconUI = temp;
                temp.icon.sprite = battleController.timelineElements[i].timelineIcon;
                offset = 45;
            }
            else if (battleController.timelineElements[i].TimelineTypes == TimeLineTypes.EnemyUnit)
            {
                temp.image.sprite = enemyFrame;

                temp.icon.sprite = battleController.timelineElements[i].timelineIcon;
                offset = -45;

            }
            else if (battleController.timelineElements[i].TimelineTypes == TimeLineTypes.Events)
            {
                temp.image.sprite = eventFrame;
                temp.icon.sprite = eventIcon;

                offset = 0;
            }
            else if (battleController.timelineElements[i].TimelineTypes == TimeLineTypes.Items)
            {
                temp.image.sprite = itemFrame;
                temp.icon.sprite = itemIcon;

                offset = 45;
            }

            temp.icon.sprite = battleController.timelineElements[i].timelineIcon;
            temp.image.SetNativeSize();
            temp.icon.SetNativeSize();

            temp.rectTransform.anchoredPosition = new Vector2(-barSize / 2 + battleController.timelineElements[i].GetActionBarPosition() * barSize, offset);
        }
    }


    //Avoid creating or destroying more than necessary
    private bool BalanceAmountOf(GameObject prefab, Transform content, int amount)
    {
        if (content.childCount > amount)
        {
            int amountToRemove = content.childCount - amount;
            for (int i = 0; i < amountToRemove; i++)
            {
                var a = content.GetChild(battleController.itemIndexToRemove);
                a.parent = removedChildren;
                a.gameObject.SetActive(false);
                //Destroy(content.GetChild(0));
            }
            return true;
        }

        if (content.childCount < amount)
        {
            int amountToAdd = amount - content.childCount;
            for (int i = 0; i < amountToAdd; i++)
            {
                Instantiate(prefab, content);
            }
            return true;
        }

        //foreach (var item in battleController.timelineElements)
        //{
        //    Debug.Log("NOMBRE DEL ITEM " + item.name);
        //}
        return false;
    }


    public bool CheckMouse()
    {
        for (int i = 0; i < battleController.timelineElements.Count; i++)
        {
            TimelineIconUI temp = content.GetChild(i).GetComponent<TimelineIconUI>();

            if (temp.mouseOver)
            {
                selectedIcon = temp;
                return true;
            }
        }

        selectedIcon = null;
        return false;
    }
}
