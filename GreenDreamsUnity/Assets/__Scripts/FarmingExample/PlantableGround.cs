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

    public bool sulanm��;
  

    [Range(0f, 100f)] // waterLevel de�eri 0 ile 100 aras�nda olacak
    public float waterLevel;
    private float minWaterLevel=0f;
    private float maxWaterLevel=100f;
    public GameObject ekilenBitki;

    [Range(0f, 100f)] // b�y�tmeKatsay�s� de�eri 0 ile 100 aras�nda olacak
    public float b�y�tmeKatsay�s�;

    //Bu script zemindeki slotlar� �ekip o slotlar�n bo� olup olmad���n� kontrol edecek.
    //E�er slotlar bo� ise ekim i�leminde bo� slota tohumun eklenmesini sa�layacak.(Yani hedef bir slot belirleyecek)
    //Zeminin sulanm�� olup olmad��� ve g�brelenmi� olup olmad��� burda tutulacak.
   
    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        //waterLevel = Mathf.Clamp(waterLevel, minWaterLevel, maxWaterLevel);
        //Ba�lang�� de�erleri atand�
        b�y�tmeKatsay�s� = 0f;
        sulanm�� = false;
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
        //Bu script ekim i�lemi s�ras�nda PlayerPlantingManagerdan �ekilecek.
        //Bu script zemindeki slotlar� �ekip o slotlar�n bo� olup olmad���n� kontrol edecek.
        //Bo� olan slotu targetSlot belirleyecek.


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
        //Su seviyesi belli bir seviyenin �st�nde ise;

        if (waterLevel >= 25)
        {
          sulanm�� = true;
        }
        else
        {
            sulanm��=false;
        }
    }

    public float CalculateGrowthMultiply()
    {
        CheckWaterisEnough();
        if (sulanm��)
        {
            b�y�tmeKatsay�s� = 100f;
        }
        else
        {
            b�y�tmeKatsay�s� = 0f;
        }
        return b�y�tmeKatsay�s�;
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
