using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dputils.Systems.DateTime;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
   // [SerializeField] private LightingPreset Preset;
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    [SerializeField, Range(0, 24)] private float hour;
    [SerializeField, Range(0, 60)] private float minute;

    [SerializeField] private DateTime dateTime;

    private float transitionDuration = 2f; // Geçiþ süresi
    private float transitionSpeed = 1f; // Geçiþ hýzý
    private float targetIntensity; // Hedef intensity deðeri
    private float currentIntensity; // Mevcut intensity deðeri
    private float intensityVelocity; // Intensity deðiþim hýzý

    public Material skyboxMaterial;
    private float targetExposure; // Hedef exposure deðeri
    private float currentExposure; // Mevcut exposure deðeri
    private float exposureVelocity; // Exposure deðiþim hýzý
  
    [SerializeField] private float nightAmbientIntensity = 0f;
    [SerializeField] private float morningAmbientIntensity = 1.1f;
    [SerializeField] private float ambientTransitionDuration = 2f;

    private float targetAmbientIntensity;
    private float currentAmbientIntensity;
    private float ambientIntensityVelocity;

    private void UpdateDateTime(DateTime dateTime)
    {
        hour = (float)dateTime.Hour;
        minute = (float)dateTime.Minutes;

        float saatDakika = hour + (minute / 60f);
        TimeOfDay = saatDakika;
    }

    private void OnEnable()
    {
        TimeManager.OnDateTimeChanged += UpdateDateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnDateTimeChanged -= UpdateDateTime;
    }

    private void Update()
    {
        //if (Preset == null)
        //    return;

        if (Application.isPlaying)
        {
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        if (DirectionalLight != null)
        {
            targetIntensity = CalculateIntensityFromTime(timePercent);
            currentIntensity = Mathf.SmoothDamp(currentIntensity, targetIntensity, ref intensityVelocity, transitionDuration / transitionSpeed);
            DirectionalLight.intensity = currentIntensity;

            targetExposure = CalculateExposureFromTime(timePercent);
            currentExposure = Mathf.SmoothDamp(currentExposure, targetExposure, ref exposureVelocity, transitionDuration / transitionSpeed);
            skyboxMaterial.SetFloat("_Exposure", currentExposure);
            
        }

        float ambientTargetIntensity = CalculateAmbientIntensityFromTime(timePercent);
        RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, ambientTargetIntensity, Time.deltaTime / ambientTransitionDuration);
    }
    private float CalculateAmbientIntensityFromTime(float timePercent)
    {
        if (timePercent >= 0.5f && timePercent <= 0.75f) // Öðlen 12 ile akþam 6 arasýnda
        {
            // Yüksek deðeri
            return 1.2f;
        }
        else if (timePercent > 0.75f || timePercent < 0.25f) // Akþam 6 ile gece 12, gece 12 ile sabah 6 arasýnda
        {
            // Düþük intensity deðeri
            return 0f;
        }
        else
        {
            // Orta intensity deðeri
            return 0.75f;
        }
    }
    private float CalculateIntensityFromTime(float timePercent)
    {
        // Saate göre uygun bir intensity deðeri hesaplayýn.
        // Burada smooth geçiþler için gerekli hesaplamalarý yapýn.
        // Örneðin:
        if (timePercent >= 0.5f && timePercent <= 0.75f) // Öðlen 12 ile akþam 6 arasýnda
        {
            // Yüksek intensity deðeri
          
            return 1.5f;
        }
        else if (timePercent > 0.75f || timePercent < 0.25f) // Akþam 6 ile gece 12, gece 12 ile sabah 6 arasýnda
        {
            // Düþük intensity deðeri
        
            return 0f;
        }
        else
        {
            // Orta intensity deðeri
           
            return 0.75f;
        }
    }

    private float CalculateExposureFromTime(float timePercent)
    {
        // Saate göre uygun bir exposure deðeri hesaplayýn.
        // Burada smooth geçiþler için gerekli hesaplamalarý yapýn.
        // Örneðin:
        if (timePercent >= 0.5f && timePercent <= 0.75f) // Öðlen 12 ile akþam 6 arasýnda
        {
            // Yüksek exposure deðeri
         
            return 0.5f;
        }
        else if (timePercent > 0.75f || timePercent < 0.25f) // Akþam 6 ile gece 12, gece 12 ile sabah 6 arasýnda
        {
            // Düþük exposure deðeri
          

            return 0.005f;
        }
        else
        {
            // Orta exposure deðeri
           
            return 1.0f;
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
