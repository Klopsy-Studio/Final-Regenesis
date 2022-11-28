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

    [SerializeField] Sprite upSupport;
    [SerializeField] Sprite downSupport;

    [SerializeField] Sprite eventSupport;

    public int offset;


    public TimelineIconUI selectedIcon;


    public Image currentActorFrame;
    public Image currentActorIcon;
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
                temp.element.iconTimeline = temp;
                temp.icon.sprite = battleController.timelineElements[i].timelineIcon;

                temp.downSupport.GetComponent<Image>().enabled = true;

                temp.downSupport.sprite = upSupport;
                offset = 70;

                temp.velocityText.gameObject.SetActive(true);
                var a = (int)temp.element.TimelineVelocity;
                temp.velocityText.SetText(a.ToString());
            }
            else if (battleController.timelineElements[i].TimelineTypes == TimeLineTypes.EnemyUnit)
            {
                temp.element.iconTimeline = temp;

                temp.image.sprite = enemyFrame;

                temp.icon.sprite = battleController.timelineElements[i].timelineIcon;

                temp.upSupport.GetComponent<Image>().enabled = true;
                temp.upSupport.sprite = downSupport;

                offset = -70;

            }
            else if (battleController.timelineElements[i].TimelineTypes == TimeLineTypes.Events)
            {
                temp.element.GetComponent<TimelineElements>().iconTimeline = temp;

                temp.image.sprite = eventFrame;
                temp.icon.sprite = eventIcon;
                //temp.upSupport.GetComponent<Image>().enabled = true;

                //temp.upSupport.sprite = eventSupport;


                offset = 0;
            }
            else if (battleController.timelineElements[i].TimelineTypes == TimeLineTypes.Items)
            {
                temp.element.GetComponent<TimelineElements>().iconTimeline = temp;

                temp.image.sprite = itemFrame;
                temp.icon.sprite = itemIcon;
                //temp.upSupport.GetComponent<Image>().enabled = true;

                //temp.upSupport.sprite = eventSupport;


                offset = 0;
            }

            else if(battleController.timelineElements[i].TimelineTypes == TimeLineTypes.PlayerDeath)
            {
                temp.element.GetComponent<TimelineElements>().iconTimeline = temp;

                temp.image.sprite = itemFrame;
                
                //temp.upSupport.GetComponent<Image>().enabled = true;

                //temp.upSupport.sprite = eventSupport;


                offset = 0;
            }

            temp.icon.sprite = battleController.timelineElements[i].timelineIcon;
            temp.image.SetNativeSize();
            //temp.icon.SetNativeSize();

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


    public void ShowIconActing(TimelineElements element)
    {
        currentActorFrame.enabled = true;
        currentActorIcon.enabled = true;

        switch (element.TimelineTypes)
        {
            case TimeLineTypes.Null:
                break;
            case TimeLineTypes.PlayerUnit:
                currentActorFrame.sprite = playerFrame;
                break;
            case TimeLineTypes.EnemyUnit:
                currentActorFrame.sprite = enemyFrame;
                break;
            case TimeLineTypes.Events:
                currentActorFrame.sprite = eventFrame;
                break;
            case TimeLineTypes.Items:
                currentActorFrame.sprite = eventFrame;
                break;
            case TimeLineTypes.PlayerDeath:
                currentActorFrame.sprite = itemFrame;
                break;
            default:
                break;
        }

        currentActorIcon.sprite = element.timelineIcon;
    }

    public void HideIconActing()
    {
        currentActorFrame.enabled = false;
        currentActorIcon.enabled = false;
    }
    public void HideTimelineIcon(TimelineElements element)
    {
        element.iconTimeline.gameObject.SetActive(false);
    }

    public void ShowTimelineIcon(TimelineElements element)
    {
        element.iconTimeline.gameObject.SetActive(true);
    }
}
