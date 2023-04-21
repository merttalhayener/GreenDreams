using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SleepBar : MonoBehaviour
{
    public Slider sleepSlider;

    public void SetMaxSleep(float sleep)
    {
        sleepSlider.maxValue = sleep;
        sleepSlider.value = sleep;
    }

    public void SetSleep(float sleep)
    {
        sleepSlider.value = Mathf.Lerp(sleepSlider.value, sleep, 1);
    }
}
