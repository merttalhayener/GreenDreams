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
    public float growthDuration; // B�y�me s�resi, istedi�iniz s�reyi ayarlayabilirsiniz

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
                // B�y�me a�amas�n� g�ncelle
                currentStage = GrowthStage.Sprout;

                // Game object'in 3D model bile�enlerini al
                MeshFilter meshFilter = GetComponent<MeshFilter>();
                MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

                // Sprout prefab�n�n 3D model ve materyallerini al
                Mesh sproutMesh = sproutPrefab.GetComponent<MeshFilter>().sharedMesh;
                Material[] sproutMaterials = sproutPrefab.GetComponent<MeshRenderer>().sharedMaterials;

                // Mesh filter'�n modelini ve materyallerini de�i�tir
                meshFilter.sharedMesh = sproutMesh;
                meshRenderer.sharedMaterials = sproutMaterials;

                break;

            case GrowthStage.Sprout:
                // B�y�me a�amas�n� g�ncelle
                currentStage = GrowthStage.Harvest;

                // Game object'in 3D model bile�enlerini al
                meshFilter = GetComponent<MeshFilter>();
                meshRenderer = GetComponent<MeshRenderer>();

                // Harvest prefab�n�n 3D model ve materyallerini al
                Mesh harvestMesh = harvestPrefab.GetComponent<MeshFilter>().sharedMesh;
                Material[] harvestMaterials = harvestPrefab.GetComponent<MeshRenderer>().sharedMaterials;

                // Mesh filter'�n modelini ve materyallerini de�i�tir
                meshFilter.sharedMesh = harvestMesh;
                meshRenderer.sharedMaterials = harvestMaterials;

                break;

            case GrowthStage.Harvest:
                // Bitki hasat edildi, gerekirse temizleme i�lemleri yap�labilir
                break;

            default:
                break;
        }
    }

   
}
