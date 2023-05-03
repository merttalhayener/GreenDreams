using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject weatherIconParent;
    [SerializeField] private Image[] weatherIconPrefabs;
    [SerializeField] private Text currentTick;
    [SerializeField] private Text currentWeatherText;
    [SerializeField] private Text currentTimeText;

    private void OnEnable()
    {
        WeatherManager.OnWeatherChange += WeatherChanged;
        GameManager.OnTick += Tick;
    }
    private void OnDisable()
    {
        WeatherManager.OnWeatherChange -= WeatherChanged;
        GameManager.OnTick -= Tick;
    }

    private void Update()
    {
        currentTimeText.text = $"TIME:{GameManager.CurrentGameTime.ToString("F2")}s";
    }

    void WeatherChanged(WeatherManager.Weather currentWeather, Queue<WeatherManager.Weather> weatherQueue)
    {
        weatherIconPrefabs[0].sprite = Resources.Load<Sprite>($"Sprites/WeatherSprites/weather-sprite-{currentWeather}");
        currentWeatherText.text = currentWeather.ToString().ToUpper();

        int index = 1;
        foreach (var item in weatherQueue)
        {
            weatherIconPrefabs[index].sprite = Resources.Load<Sprite>($"Sprites/WeatherSprites/weather-sprite-{item}");
            index++;
        }
        
    }
    

    private void Tick()
    {
        currentTick.text = $"TICK: {GameManager.CurrentTick}";
    }




}
