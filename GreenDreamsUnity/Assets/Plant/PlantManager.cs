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

    public float growthDuration; // Büyüme süresi, istediðiniz süreyi ayarlayabilirsiniz


    public GameObject sproutPrefab;
    public GameObject harvestPrefab;
    private GameObject currentPlant;
    public GameObject harvestedPrefab;

    void Start()
    {
        currentStage = GrowthStage.Seed;
       
        canHarvest = false;

        // currentPlant'e bitkiyi temsil eden GameObject'i atayýn
        currentPlant = this.gameObject;
    }

    public void GrowPlant()
    {
        switch (currentStage)
        {
            case GrowthStage.Seed:
                // Büyüme aþamasýný güncelle
                currentStage = GrowthStage.Sprout;

                // Game object'in 3D model bileþenlerini al
                MeshFilter meshFilter = currentPlant.GetComponent<MeshFilter>();
                MeshRenderer meshRenderer = currentPlant.GetComponent<MeshRenderer>();

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

                // Harvest prefabýnýn 3D model ve materyallerini al
                Mesh harvestMesh = harvestPrefab.GetComponent<MeshFilter>().sharedMesh;
                Material[] harvestMaterials = harvestPrefab.GetComponent<MeshRenderer>().sharedMaterials;

                // Objedeki mesh ve materyalleri deðiþtir
                MeshFilter currentMeshFilter = currentPlant.GetComponent<MeshFilter>();
                MeshRenderer currentMeshRenderer = currentPlant.GetComponent<MeshRenderer>();

                currentMeshFilter.sharedMesh = harvestMesh;
                currentMeshRenderer.sharedMaterials = harvestMaterials;
                break;

            case GrowthStage.Harvest:
                canHarvest = true;
                Debug.Log("Beni topla");
                // Bitki hasat edildi, gerekirse temizleme iþlemleri yapýlabilir
                break;

            default:
                break;
        }
    }

    public void HarvestThis()
    {
        // Bitkinin konumunu ve dönüþünü al
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        // Yeni objeyi instantiate et
        GameObject harvestedObject = Instantiate(harvestedPrefab, position, rotation);

        // Eski bitkiyi sil
        Destroy(this.gameObject);

        // Hasat edilen objeyle istediðiniz iþlemleri yapabilirsiniz
        // Örneðin, harvestedObject üzerindeki bileþenlere eriþebilir veya baþka iþlemler gerçekleþtirebilirsiniz
    }

}