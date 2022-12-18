using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnitUI : UnitUI
{
    bool updatingValue;
    [SerializeField] float speed;
    [Header("UI")]
    public Canvas unitUI;
    [SerializeField] GameObject actionPointsObject;
    [SerializeField] GameObject stunIndicator;
    [SerializeField] List<Image> actionPoints;

    [SerializeField] List<Image> originalActionPoints;
    [SerializeField] List<Image> usedActionPoints = new List<Image>();

    List<Image> previewActionPoints = new List<Image>();


    [Header("Sprites")]
    [SerializeField] Sprite regularActionPointsSprite;
    [SerializeField] Sprite previewActionPointsSprite;
    [SerializeField] Sprite spentActionPointsSprite;

    [SerializeField] Slider unitFury;
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

    public void ChangeFuryValue(int value)
    {

        StartCoroutine(SliderValueAnimation(unitFury, value));
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


    IEnumerator SliderValueAnimation(Slider s, int targetValue)
    {
        s.gameObject.SetActive(true);
        updatingValue = true;

        if (s.value >= targetValue)
        {
            while (s.value >= targetValue)
            {
                s.value -= Time.deltaTime * speed;
                yield return null;

                if (s.value <= 0)
                {
                    break;
                }
            }
        }
        else
        {
            while (s.value <= targetValue)
            {
                s.value += Time.deltaTime * speed;
                yield return null;

                if (s.value >= s.maxValue)
                {
                    break;
                }
            }
        }

        s.value = targetValue;

        yield return new WaitForSeconds(0.5f);
        s.gameObject.SetActive(false);
        updatingValue = false;
    }
}
