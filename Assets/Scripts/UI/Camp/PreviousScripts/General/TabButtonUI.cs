using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TabButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;
    public Image currentImage;

    public Sprite idleImage;
    [SerializeField] Sprite selectedImage;
    [SerializeField] Sprite hoverImage;

    private void Start()
    {
        currentImage = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
        //currentImage.sprite = selectedImage;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
        Debug.Log(gameObject.name);
        //currentImage.sprite = hoverImage; 
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }
}