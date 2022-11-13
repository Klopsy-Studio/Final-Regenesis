using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TimelineIconUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform rectTransform;
    public Image image;

    public Image icon;

    public Image upSupport;
    public Image downSupport;

    public bool mouseOver;

    public Animator iconAnimations;

    public TimelineElements element;

    public GameObject stunnedIndicator;
    public void EnableStun()
    {
        stunnedIndicator.SetActive(true);
    }

    public void DisableStun()
    {
        stunnedIndicator.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        Return();
    }
    public void Grow()
    {
        iconAnimations.SetBool("isGrow", true);
    }

    public void Return()
    {
        iconAnimations.SetBool("isGrow", false);
    }
}
