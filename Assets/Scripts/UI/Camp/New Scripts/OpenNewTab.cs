using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenNewTab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject tabToOpen;
    //[SerializeField] GameObject gameObjectAnimator;
     public Animator animator;

    //private void Wake()
    //{
    //    animator = gameObjectAnimator.GetComponent<Animator>();
    //}
    public void OpenTab()
    {
     
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("AAAAAAA");
        animator.SetInteger("ToIdle", 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetInteger("ToIdle", 0);
    }

 
}
