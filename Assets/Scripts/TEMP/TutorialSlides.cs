using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSlides : MonoBehaviour
{
    int index = 0;

    [SerializeField] Sprite[] slides;
    [SerializeField] Image imageComponent;

    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject prevButton;
    [SerializeField] GameObject playButton;
    public void NextSlide()
    {
        index++;
        imageComponent.sprite = slides[index];

        if (index == 1)
        {
            prevButton.SetActive(true);
        }
        if(index >= slides.Length-1)
        {
            nextButton.SetActive(false);
            playButton.SetActive(true);
        }
    }

    public void PrevSlide()
    {
        index--;

        if(index == 0)
        {
            prevButton.SetActive(false);
        }

        playButton.SetActive(false);
        nextButton.SetActive(true);
        imageComponent.sprite = slides[index];

    }
}
