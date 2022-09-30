using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenNewTab : MonoBehaviour
{
    [SerializeField] GameObject tabToOpen;
    
    public void OpenTab()
    {
        if(tabToOpen != null)
        {
            tabToOpen.SetActive(true);
        }
        else
        {
            Debug.Log("There's no tab to open");
        }
    }
}
