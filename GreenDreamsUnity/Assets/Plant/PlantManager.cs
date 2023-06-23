using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlantManager : MonoBehaviour
{
    public GrowthStage currentStage;
    public bool canHarvest;
    public enum GrowthStage
    {
        Seed,
        Sprout,
        Harvest
    }

    public float growthDuration; // B�y�me s�resi, istedi�iniz s�reyi ayarlayabilirsiniz


    public GameObject sproutPrefab;
    public GameObject harvestPrefab;
    private GameObject currentPlant;
    public GameObject harvestedPrefab;

    void Start()
    {
        currentStage = GrowthStage.Seed;
       
        canHarvest = false;

        // currentPlant'e bitkiyi temsil eden GameObject'i atay�n
        currentPlant = this.gameObject;
    }

    public void GrowPlant()
    {
        switch (currentStage)
        {
            case GrowthStage.Seed:
                // B�y�me a�amas�n� g�ncelle
                currentStage = GrowthStage.Sprout;

                // Game object'in 3D model bile�enlerini al
                MeshFilter meshFilter = currentPlant.GetComponent<MeshFilter>();
                MeshRenderer meshRenderer = currentPlant.GetComponent<MeshRenderer>();

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

                // Harvest prefab�n�n 3D model ve materyallerini al
                Mesh harvestMesh = harvestPrefab.GetComponent<MeshFilter>().sharedMesh;
                Material[] harvestMaterials = harvestPrefab.GetComponent<MeshRenderer>().sharedMaterials;

                // Objedeki mesh ve materyalleri de�i�tir
                MeshFilter currentMeshFilter = currentPlant.GetComponent<MeshFilter>();
                MeshRenderer currentMeshRenderer = currentPlant.GetComponent<MeshRenderer>();

                currentMeshFilter.sharedMesh = harvestMesh;
                currentMeshRenderer.sharedMaterials = harvestMaterials;
                break;

            case GrowthStage.Harvest:
                canHarvest = true;
                Debug.Log("Beni topla");
                // Bitki hasat edildi, gerekirse temizleme i�lemleri yap�labilir
                break;

            default:
                break;
        }
    }

    public void HarvestThis()
    {
        // Bitkinin konumunu ve d�n���n� al
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        // Yeni objeyi instantiate et
        GameObject harvestedObject = Instantiate(harvestedPrefab, position, rotation);

        // Eski bitkiyi sil
        Destroy(this.gameObject);

        // Hasat edilen objeyle istedi�iniz i�lemleri yapabilirsiniz
        // �rne�in, harvestedObject �zerindeki bile�enlere eri�ebilir veya ba�ka i�lemler ger�ekle�tirebilirsiniz
    }

}