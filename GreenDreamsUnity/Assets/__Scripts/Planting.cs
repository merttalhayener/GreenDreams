using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planting : MonoBehaviour
{
    public enum GrowthStage { Seed , Seedling, Flowering , Ripe}
    public GrowthStage growthStage = GrowthStage.Seed;
    public float growthTime = 10f;
    public int maxGrowth = 3;
    public int currrentProgression = 0;
    public float waterLevel = 0f;
    public float maxWaterLevel = 10f;
    public float waterConsumptionRate = 0.5f;
    public float waterConsumptionInterval = 1f;


    public bool isPlanted = false;
    public bool isWatering = false;


    private void Start()
    {
        InvokeRepeating("Growth", growthTime, growthTime);
    }


    private void Update()
    {
       
    }

    public void Growth()
    {
        if (currrentProgression != maxGrowth)
        {
            gameObject.transform.GetChild(currrentProgression).gameObject.SetActive(true);
        }
        if (currrentProgression > 0 && currrentProgression < maxGrowth)
        {
            gameObject.transform.GetChild(currrentProgression - 1).gameObject.SetActive(false);
        }
        if (currrentProgression < maxGrowth)
        {
            currrentProgression++;
        }
    }
}
