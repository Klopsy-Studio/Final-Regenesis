using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum StatusMode
{
    Big, Small
}
public class UnitStatus : MonoBehaviour
{
    [Header("Status Modes")]
    [SerializeField] GameObject bigUnitUI;
    [SerializeField] GameObject smallUnitUI;
    [Space]
    [Header("Unit Status References")]
    
    [SerializeField] Text unitName;
    [SerializeField] Slider unitHealth;
    public Image unitPortrait;
    [SerializeField] Image unitWeapon;


    [Header("Titles")]
    [HideInInspector] public bool updatingValue;
    [Header("UI Animations Variables")]
    [SerializeField] float speed;

    [Header("Status Mode")]
    [SerializeField] StatusMode uiStatus;
    public void SetUnit(PlayerUnit unit)
    {
        //unitName.text = unit.unitName;
        unitHealth.maxValue = unit.maxHealth;
        unitHealth.value = unit.health;



        unitPortrait.sprite = unit.timelineIcon;

        unitWeapon.sprite = unit.weapon.weaponIcon;

        unitName.text = unit.unitName;

        unit.status = this;
    }

    void UpdateSliderValue(Slider currentSlider, int value)
    {
        currentSlider.value = value;
    }


    public void HealthAnimation(int target)
    {
        ChangeToBig();
        StartCoroutine(SliderValueAnimation(unitHealth, target));
    }

    //public void SharpnessAnimation(int target)
    //{
    //    StartCoroutine(SliderValueAnimation(unitSharpness, target));
    //}

    IEnumerator SliderValueAnimation(Slider s, int targetValue)
    {
        updatingValue = true;

        if(s.value >= targetValue)
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

        UpdateSliderValue(unitHealth, targetValue);
        Invoke("ChangeToSmall", 1f);
        updatingValue = false;
    }
         
    public void ChangeToBig()
    {
        if(uiStatus != StatusMode.Big)
        {
            uiStatus = StatusMode.Big;
            bigUnitUI.SetActive(true);
        }
    }

    public void ChangeToSmall()
    {
        if (uiStatus != StatusMode.Small)
        {
            uiStatus = StatusMode.Small;
            bigUnitUI.SetActive(false);
        }
    }
}
