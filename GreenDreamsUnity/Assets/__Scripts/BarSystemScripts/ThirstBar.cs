using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirstBar : MonoBehaviour
{
    public Slider thirstSlider;

    public void SetMaxThirst(float thirst)
    {
        thirstSlider.maxValue = thirst;
        thirstSlider.value = thirst;
    }

    public void SetThirst(float thirst)
    {
        thirstSlider.value = Mathf.Lerp(thirstSlider.value,thirst,1);
    }
}
