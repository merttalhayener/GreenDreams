using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public Slider hungerSlider;

    public void SetMaxHunger(float hunger)
    {
        hungerSlider.maxValue = hunger;
        hungerSlider.value = hunger;
    }

    public void SetHunger(float hunger)
    {
        hungerSlider.value = Mathf.Lerp(hungerSlider.value, hunger, 1);
    }
}
