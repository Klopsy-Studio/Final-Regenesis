using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitStatus : MonoBehaviour
{
    [SerializeField] Text unitName;
    [SerializeField] Slider unitHealth;
    //[SerializeField] Slider unitSharpness;
    [SerializeField] Image unitPortrait;
    [SerializeField] Image unitWeapon;

    [Header("Titles")]
    [SerializeField] GameObject sharpnessTitle;
    [HideInInspector] public bool updatingValue;
    [Header("UI Animations Variables")]
    [SerializeField] float speed;

    public void SetUnit(PlayerUnit unit)
    {
        unitName.text = unit.unitName;
        unitHealth.maxValue = unit.health.baseValue;
        unitHealth.value = unit.health.baseValue;

        unitPortrait.sprite = unit.unitPortrait;
        //unitSharpness.maxValue = unit.weapon.planticidaPoints;
        //unitSharpness.value = unit.weapon.planticidaPoints;

        unitWeapon.sprite = unit.weapon.weaponIcon;


        unit.status = this;
    }

    void UpdateSliderValue(Slider currentSlider, int value)
    {
        currentSlider.value = value;
    }


    public void HealthAnimation(int target)
    {
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

        updatingValue = false;
    }
         
    
}
