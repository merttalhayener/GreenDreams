using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float tickFrequency = 0.2f;

    private static int currentTick = 0;
    public static int CurrentTick => currentTick;
    private float lastTickTime = 0;

    [SerializeField] private static float currentGameTime;
    public static float CurrentGameTime => currentGameTime;
    public static Action OnTick;


    
    void Update()
    {
        Tick();
    }
    private void Tick()
    {
        currentGameTime += Time.deltaTime;

        if (currentGameTime >= lastTickTime + tickFrequency)
        {
            lastTickTime = currentGameTime;
            OnTick?.Invoke();
            currentTick++;
        }
    }
}
