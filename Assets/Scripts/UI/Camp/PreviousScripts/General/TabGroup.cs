using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour //THIS SCRIPT HAS BEEN MODIFIED. IT DOES NOT HAVE ITS ORIGINAL PURPOSE
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

      
        if(index == 0)
        {
            for (int i = 0; i < objectsToSwap.Count; i++)
            {
                objectsToSwap[i].SetActive(false);
                
            }
            objectsToSwap[0].SetActive(true);
        }
        else if(index >0)
        {
            for (int i = 0; i < objectsToSwap.Count; i++)
            {
                objectsToSwap[i].SetActive(false);

            }
            objectsToSwap[1].SetActive(true);
        }
        //for (int i = 0; i < objectsToSwap.Count; i++)
        //{
            
        //    if (i == index)
        //        objectsToSwap[i].SetActive(true);
        //    else
        //        objectsToSwap[i].SetActive(false);
        //}
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