using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [Header("----Weather Management-----")]
    [SerializeField] private int ticksBetweenWeather = 10;
    [SerializeField] private int weatherQueueSize = 3;
    private int currentWeatherTick = 0;
    [SerializeField] private Weather currentWeather = Weather.Sunny;
    public Weather CurrentWeather => currentWeather;
    private Queue<Weather> weatherQueue;

    [Header("-----Weather VFX-------")]
    [SerializeField] ParticleSystem rainParticles;
    [SerializeField] ParticleSystem snowParticles;

    [Header("------Debug Options-----")]
    public bool forceRain = false;
    public static Action<Weather, Queue<Weather>> OnWeatherChange;


    void Start()
    {
        rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        snowParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        FillWeatherQueue();
        ChangeWeather();
    }
    private void OnEnable()
    {
        GameManager.OnTick += Tick;
    }
    private void OnDisable()
    {
        GameManager.OnTick -= Tick;
    }
    private void Tick()
    {
        currentWeatherTick++;
        if (currentWeatherTick >= ticksBetweenWeather)
        {
            currentWeatherTick = 0;
            ChangeWeather();
        }
    }
    void FillWeatherQueue()
    {
        weatherQueue = new Queue<Weather>();

        for (int i = 0; i < weatherQueueSize; i++)
        {
            Weather tempWeather = GetRandomWeather();
            weatherQueue.Enqueue(tempWeather);
            Debug.Log("Weather is {tempWeather} at index {i}");
        }
    }

    private Weather GetRandomWeather()
    {
        int randomWeather = 0;

        if (!forceRain) randomWeather = UnityEngine.Random.Range(0, (int)Weather.WEATHER_MAX + 1);
        else randomWeather = 2;

        return (Weather)randomWeather;
    }
    void ChangeWeather()
    {
        currentWeather = weatherQueue.Dequeue();
        weatherQueue.Enqueue(GetRandomWeather());

        OnWeatherChange?.Invoke(currentWeather, weatherQueue);

        switch (currentWeather)
        {
            case Weather.Sunny:
                rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                snowParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                break;
            case Weather.Rain:
                rainParticles.Play();
                snowParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                break;
            case Weather.Snow:
                rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                snowParticles.Play();
                break;
            default:
                break;
            
               
        }
    }

    public enum Weather
    {
        Sunny = 0,
        Rain = 2,
        Snow = 3,
        WEATHER_MAX =Snow
    }



}
