using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTab : MonoBehaviour
{
    [SerializeField] GameObject tab;
    [SerializeField] GameObject currentTab;

    public void OpenTab()
    {
        tab.gameObject.SetActive(true);
        currentTab.SetActive(false);
    }
}
