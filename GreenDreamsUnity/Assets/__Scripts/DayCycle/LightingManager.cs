using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dputils.Systems.DateTime;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    [SerializeField, Range(0, 24)] private float hour;
    [SerializeField, Range(0, 60)] private float minute;

    [SerializeField] private DateTime dateTime;

    //public float rotationSpeed = 1.0f;
    //public float startRotation = 0f;
    //public float endRotation = 360f;
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
        
        
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            UpdateLighting(TimeOfDay/24f);
        }
        else
        {
            UpdateLighting(TimeOfDay/24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        //RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        //RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            //DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170,0));

            //float innerTimePercent = Time.time % rotationSpeed / rotationSpeed;
            //Quaternion startQuaternion = Quaternion.Euler(new Vector3(startRotation, 170, 0));
            //Quaternion endQuaternion = Quaternion.Euler(new Vector3(endRotation, 170, 0));
            //DirectionalLight.transform.localRotation = Quaternion.Lerp(startQuaternion, endQuaternion, timePercent);
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;
        if (RenderSettings.sun !=null)
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
