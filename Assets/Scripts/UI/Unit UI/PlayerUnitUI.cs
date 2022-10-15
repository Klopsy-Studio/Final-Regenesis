using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnitUI : MonoBehaviour
{
    public Canvas unitUI;
    [SerializeField] List<Image> actionPoints;

    [SerializeField] List<Image> originalActionPoints;
    [SerializeField] List<Image> usedActionPoints = new List<Image>();

    List<Image> previewActionPoints = new List<Image>();


    int index;

    private void Start()
    {
        foreach(Image i in actionPoints)
        {
            originalActionPoints.Add(i);
        }
    }


    public void ShowActionPoints()
    {
        previewActionPoints.Clear();
        unitUI.gameObject.SetActive(true);

        if(actionPoints != null)
        {
            foreach (Image i in actionPoints)
            {
                i.color = Color.white;
            }
        }
        

        if(usedActionPoints != null)
        {
            foreach (Image i in usedActionPoints)
            {
                i.color = Color.red;
            }
        }
        

    }

    public void PreviewActionCost(int actionCost)
    {
        index = actionPoints.Count - 1;
        for (int i = actionCost; i >0; i--)
        {
            actionPoints[index].color = Color.red;
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
        usedActionPoints = previewActionPoints;
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
