using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public GrowthStage currentStage;
    public enum GrowthStage
    {
        Seed,
        Sprout,
        Harvest
    }

    private float growthTimer;
    public float growthDuration; // Büyüme süresi, istediðiniz süreyi ayarlayabilirsiniz

    public GameObject seedPrefab;
    public GameObject sproutPrefab;
    public GameObject harvestPrefab;

    private GameObject currentPlant;

    void Start()
    {
        currentStage = GrowthStage.Seed;
        growthTimer = 0f;
    
    }

    void Update()
    {
        if (currentStage != GrowthStage.Harvest)
        {
            growthTimer += Time.deltaTime;

            if (growthTimer >= growthDuration)
            {
                GrowPlant();
                growthTimer = 0f;
            }
        }
    }

    public void GrowPlant()
    {
        switch (currentStage)
        {
            case GrowthStage.Seed:
                // Büyüme aþamasýný güncelle
                currentStage = GrowthStage.Sprout;

                // Game object'in 3D model bileþenlerini al
                MeshFilter meshFilter = GetComponent<MeshFilter>();
                MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

                // Sprout prefabýnýn 3D model ve materyallerini al
                Mesh sproutMesh = sproutPrefab.GetComponent<MeshFilter>().sharedMesh;
                Material[] sproutMaterials = sproutPrefab.GetComponent<MeshRenderer>().sharedMaterials;

                // Mesh filter'ýn modelini ve materyallerini deðiþtir
                meshFilter.sharedMesh = sproutMesh;
                meshRenderer.sharedMaterials = sproutMaterials;

                break;

            case GrowthStage.Sprout:
                // Büyüme aþamasýný güncelle
                currentStage = GrowthStage.Harvest;

                // Game object'in 3D model bileþenlerini al
                meshFilter = GetComponent<MeshFilter>();
                meshRenderer = GetComponent<MeshRenderer>();

                // Harvest prefabýnýn 3D model ve materyallerini al
                Mesh harvestMesh = harvestPrefab.GetComponent<MeshFilter>().sharedMesh;
                Material[] harvestMaterials = harvestPrefab.GetComponent<MeshRenderer>().sharedMaterials;

                // Mesh filter'ýn modelini ve materyallerini deðiþtir
                meshFilter.sharedMesh = harvestMesh;
                meshRenderer.sharedMaterials = harvestMaterials;

                break;

            case GrowthStage.Harvest:
                // Bitki hasat edildi, gerekirse temizleme iþlemleri yapýlabilir
                break;

            default:
                break;
        }
    }

   
}
