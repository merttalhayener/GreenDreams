using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dputils.Systems.DateTime;
using UnityEngine.UI;


public class ClockManager : MonoBehaviour
{
    public RectTransform ClockFace;
    public TextMeshProUGUI Date, Time, Season, Week;

    public Image weatherSprite;
    public Sprite[] weatherSprites;

    private float startingRotation;

    public Light sunLight;
    public float nightIntensity;
    public float dayIntensity;
    public AnimationCurve dayNightCurve;

    private void Awake()
    {
        startingRotation = ClockFace.localEulerAngles.z;
    }
    private void OnEnable()
    {
        TimeManager.OnDateTimeChanged += UpdateDateTime;
    }
    private void OnDisable()
    {
        TimeManager.OnDateTimeChanged -= UpdateDateTime;
    }


    private void UpdateDateTime(DateTime dateTime)
    {
        Date.text = dateTime.DateToString();
        Time.text = dateTime.TimeToString();
        Season.text = dateTime.Season.ToString();
        Week.text = $"WK: {dateTime.CurrentWeek.ToString()}";
        //weatherSprite.sprite = weatherSprites[(int)WeatherManager.currentWeather];

        float t = (float)dateTime.Hour / 24f;

        float newRotation = Mathf.Lerp(0, 360, t);
        ClockFace.localEulerAngles = new Vector3(0, 0, newRotation + startingRotation);

        //float dayNighT = dayNightCurve.Evaluate(t);

        //sunLight.intensity = Mathf.Lerp( dayIntensity, nightIntensity, dayNighT);
    }
}
