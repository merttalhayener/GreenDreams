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

    private void Start()
    {
        InvokeRepeating("Growth", growthTime, growthTime);
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
