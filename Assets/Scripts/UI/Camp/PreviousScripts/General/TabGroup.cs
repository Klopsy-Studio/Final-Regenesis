using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    [SerializeField] public List<TabButton> tabButtons;
    [SerializeField] public TabButton selectedTabButton;
    [SerializeField] public Sprite tabIdle;
    [SerializeField] public Sprite tabHover;
    [SerializeField] public Sprite tabActive;
    [SerializeField] public List<GameObject> objectsToSwap;

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
            tabButtons = new List<TabButton>();

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTabButton == null || button != selectedTabButton)
            button.image.sprite = tabHover;

    }
    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }
    public void OnTabSelected(TabButton button)
    {
        selectedTabButton = button;
        ResetTabs();
        button.image.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
                objectsToSwap[i].SetActive(true);
            else
                objectsToSwap[i].SetActive(false);
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (selectedTabButton != null && button == selectedTabButton)
                continue;
            button.image.sprite = tabIdle;
        }
    }
}