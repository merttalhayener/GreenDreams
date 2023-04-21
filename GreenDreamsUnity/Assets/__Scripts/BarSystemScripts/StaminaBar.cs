using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;

    public void SetMaxStamina(float stamina)
    {
        staminaSlider.maxValue = stamina;
        staminaSlider.value = stamina;
    }

    public void SetStamina(float stamina)
    {
        staminaSlider.value = Mathf.Lerp(staminaSlider.value, stamina, 1);
    }
}
