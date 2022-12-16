using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnitUI : UnitUI
{
    [Header("UI")]
    public Canvas unitUI;
    [SerializeField] GameObject actionPointsObject;
    [SerializeField] GameObject stunIndicator;
    [SerializeField] List<Image> actionPoints;

    [SerializeField] List<Image> originalActionPoints;
    [SerializeField] List<Image> usedActionPoints = new List<Image>();

    List<Image> previewActionPoints = new List<Image>();


    [SerializeField] GameObject mark;

    [Header("Sprites")]
    [SerializeField] Sprite regularActionPointsSprite;
    [SerializeField] Sprite previewActionPointsSprite;
    [SerializeField] Sprite spentActionPointsSprite;

    int index;

    private void Start()
    {
        foreach(Image i in actionPoints)
        {
            originalActionPoints.Add(i);
        }
    }

    public void HideActionPoints()
    {
        actionPointsObject.SetActive(false);
    }

    public void EnableMark()
    {
        mark.gameObject.SetActive(true);
    }

    public void DisableMark()
    {
        mark.gameObject.SetActive(false);
    }
    public void ShowActionPoints()
    {
        previewActionPoints.Clear();
        actionPointsObject.SetActive(true);

        if(actionPoints != null)
        {
            foreach (Image i in actionPoints)
            {
                i.sprite = regularActionPointsSprite;
            }
        }
        

        if(usedActionPoints != null)
        {
            foreach (Image i in usedActionPoints)
            {
                i.sprite = spentActionPointsSprite;
            }
        }
        

    }

    public void EnableStun()
    {
        stunIndicator.SetActive(true);
    }

    public void DisableStun()
    {
        stunIndicator.SetActive(false);
    }
    public void PreviewActionCost(int actionCost)
    {
        index = actionPoints.Count - 1;
        for (int i = actionCost; i >0; i--)
        {
            actionPoints[index].sprite = previewActionPointsSprite;
            previewActionPoints.Add(actionPoints[index]);
            index--;
        }
    }

    public void SpendActionPoints(int actionCost)
    {
        index = actionPoints.Count - 1;
        for (int i = actionCost; i >0; i--)
        {
            actionPoints.RemoveAt(index);
            index--;
        }

        foreach(Image i in previewActionPoints)
        {
            usedActionPoints.Add(i);
        }
    }
    public void ResetActionPoints()
    {
        actionPoints.Clear();
        usedActionPoints.Clear();
        previewActionPoints.Clear();

        foreach(Image i in originalActionPoints)
        {
            actionPoints.Add(i);
        }
    }

 
}
