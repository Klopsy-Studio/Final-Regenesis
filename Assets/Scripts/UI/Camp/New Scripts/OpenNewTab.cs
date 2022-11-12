using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenNewTab : MonoBehaviour
{
    [SerializeField] GameObject tabToOpen;

    private void Start()
    {
        Debug.Log("HOLA");
    }
    public void OpenTab()
    {
        Debug.Log("pipi");
        if(tabToOpen != null)
        {
            Debug.Log("GOLPEA");
            tabToOpen.SetActive(true);
        }
        else
        {
            Debug.Log("There's no tab to open");
        }
    }
}
