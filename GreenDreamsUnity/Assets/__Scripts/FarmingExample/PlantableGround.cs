using Dputils.Systems.DateTime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.Examples.TMP_ExampleScript_01;


public class PlantableGround : MonoBehaviour
{
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject slot4;
    public GameObject targetSlot;
    public TimeManager timeManager;
    
    public List<GameObject> plants;
    public QuestManager questManager;

    public bool sulanmýþ;
  

    [Range(0f, 100f)] // waterLevel deðeri 0 ile 100 arasýnda olacak
    public float waterLevel;
    private float minWaterLevel=0f;
    private float maxWaterLevel=100f;
    public GameObject ekilenBitki;

    [Range(0f, 100f)] // büyütmeKatsayýsý deðeri 0 ile 100 arasýnda olacak
    public float büyütmeKatsayýsý;

    //Bu script zemindeki slotlarý çekip o slotlarýn boþ olup olmadýðýný kontrol edecek.
    //Eðer slotlar boþ ise ekim iþleminde boþ slota tohumun eklenmesini saðlayacak.(Yani hedef bir slot belirleyecek)
    //Zeminin sulanmýþ olup olmadýðý ve gübrelenmiþ olup olmadýðý burda tutulacak.
   
    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        //waterLevel = Mathf.Clamp(waterLevel, minWaterLevel, maxWaterLevel);
        //Baþlangýç deðerleri atandý
        büyütmeKatsayýsý = 0f;
        sulanmýþ = false;
        SetTag("Selectable");
        waterLevel = Mathf.Clamp(waterLevel, minWaterLevel, maxWaterLevel);
        timeManager = FindObjectOfType<TimeManager>();

    }

    private void Update()
    {
        CalculateGrowthMultiply();
        waterLevel -= 2f * Time.deltaTime;
    }
    public void SetTag(string newTag)
    {
        gameObject.tag = newTag;
    }
    public void CheckEmptySlot()
    {
        //Bu script ekim iþlemi sýrasýnda PlayerPlantingManagerdan çekilecek.
        //Bu script zemindeki slotlarý çekip o slotlarýn boþ olup olmadýðýný kontrol edecek.
        //Boþ olan slotu targetSlot belirleyecek.


        if(slot1.transform.childCount == 0)
        {
           targetSlot = slot1;
           
        }
        else if (slot2.transform.childCount == 0)
        {
            targetSlot = slot2;
            
        }
        else if (slot3.transform.childCount == 0)
        {
            targetSlot = slot3;
           
        }
        else if (slot4.transform.childCount == 0)
        {
            targetSlot = slot4;
            
        }
        else
        {
            targetSlot = null;

        }

    }

    private void CheckWaterisEnough()
    {
        //Su seviyesi belli bir seviyenin üstünde ise;

        if (waterLevel >= 25)
        {
          sulanmýþ = true;
        }
        else
        {
            sulanmýþ=false;
        }
    }

    public float CalculateGrowthMultiply()
    {
        CheckWaterisEnough();
        if (sulanmýþ)
        {
            büyütmeKatsayýsý = 100f;
        }
        else
        {
            büyütmeKatsayýsý = 0f;
        }
        return büyütmeKatsayýsý;
    }

    public void PlantSeedToFarm(GameObject targetSeed)
    {
        GameObject ekilenBitkiObjesi = Instantiate(targetSeed, targetSlot.transform.position, targetSlot.transform.rotation, targetSlot.transform);
        ekilenBitki = ekilenBitkiObjesi;
        timeManager.plantList.Add(ekilenBitki);
        plants.Add(ekilenBitki);
        questManager.HandleFarmQuest();


    }

   
}
