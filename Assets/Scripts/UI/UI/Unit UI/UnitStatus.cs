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
    
    [SerializeField] Text bigUnitName;
    [SerializeField] Slider smallUnitHealth;
    [SerializeField] Slider bigUnitHealth;
    [SerializeField] Image smallUnitPortrait;
    [SerializeField] Image bigUnitPortrait;
    [SerializeField] Image smallUnitWeapon;
    [SerializeField] Image bigUnitWeapon;


    [Header("Titles")]
    [HideInInspector] public bool updatingValue;
    [Header("UI Animations Variables")]
    [SerializeField] float speed;

    [Header("Status Mode")]
    [SerializeField] StatusMode uiStatus;
    public void SetUnit(PlayerUnit unit)
    {
        //unitName.text = unit.unitName;
        smallUnitHealth.maxValue = unit.health.baseValue;
        smallUnitHealth.value = unit.health.baseValue;

        bigUnitHealth.maxValue = unit.health.baseValue;
        bigUnitHealth.value = unit.health.baseValue;

        smallUnitPortrait.sprite = unit.unitPortrait;
        bigUnitPortrait.sprite = unit.unitPortrait;

        //unitSharpness.maxValue = unit.weapon.planticidaPoints;
        //unitSharpness.value = unit.weapon.planticidaPoints;

        smallUnitWeapon.sprite = unit.weapon.weaponIcon;
        bigUnitWeapon.sprite = unit.weapon.weaponIcon;

        bigUnitName.text = unit.unitName;

        unit.status = this;
    }

    void UpdateSliderValue(Slider currentSlider, int value)
    {
        currentSlider.value = value;
    }


    public void HealthAnimation(int target)
    {
        switch (uiStatus)
        {
            case StatusMode.Big:
                StartCoroutine(SliderValueAnimation(bigUnitHealth, target));
                break;
            case StatusMode.Small:
                StartCoroutine(SliderValueAnimation(smallUnitHealth, target));
                break;
            default:
                break;
        }
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

        UpdateSliderValue(bigUnitHealth, targetValue);
        UpdateSliderValue(smallUnitHealth, targetValue);
        updatingValue = false;
    }
         
    public void ChangeToBig()
    {
        if(uiStatus != StatusMode.Big)
        {
            uiStatus = StatusMode.Big;
            smallUnitUI.SetActive(false);
            bigUnitUI.SetActive(true);
        }
    }

    public void ChangeToSmall()
    {
        if (uiStatus != StatusMode.Small)
        {
            uiStatus = StatusMode.Small;
            smallUnitUI.SetActive(true);
            bigUnitUI.SetActive(false);
        }
    }
}
