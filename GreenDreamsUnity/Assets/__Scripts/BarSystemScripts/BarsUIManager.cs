using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarsUIManager : MonoBehaviour
{
    [SerializeField] private PlayerStatsManager playerStatsManager;
    [SerializeField] private Image hungerMeter, thirstMeter, staminaMeter, healthMeter;

    private void FixedUpdate()
    {
        hungerMeter.fillAmount = Mathf.Lerp(hungerMeter.fillAmount, playerStatsManager.hungerPercent,1);
        thirstMeter.fillAmount = Mathf.Lerp(thirstMeter.fillAmount, playerStatsManager.thirstPercent, 1);
        staminaMeter.fillAmount = Mathf.Lerp(staminaMeter.fillAmount, playerStatsManager.staminaPercent, 1);
        healthMeter.fillAmount = Mathf.Lerp(healthMeter.fillAmount, playerStatsManager.healthPercent, 1);
    }
}
