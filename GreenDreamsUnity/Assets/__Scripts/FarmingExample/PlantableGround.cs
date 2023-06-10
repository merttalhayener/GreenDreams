using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlantableGround : MonoBehaviour
{
    public GameObject slot1;
    public GameObject slot2;
    public GameObject targetSlot;

    public bool sulanmýþ;
    public bool gübrelenmiþ;

    public float waterLevel; //Clamp edilmeli
    public float fertilizerLevel; //Clamp edilmeli

    public float büyütmeKatsayýsý;

    //Bu script zemindeki slotlarý çekip o slotlarýn boþ olup olmadýðýný kontrol edecek.
    //Eðer slotlar boþ ise ekim iþleminde boþ slota tohumun eklenmesini saðlayacak.(Yani hedef bir slot belirleyecek)
    //Zeminin sulanmýþ olup olmadýðý ve gübrelenmiþ olup olmadýðý burda tutulacak.

    private void Awake()
    {
        //Baþlangýç deðerleri atandý
        büyütmeKatsayýsý = 0f;
        waterLevel = 50f;
        fertilizerLevel = 50f;

        sulanmýþ = false;
        gübrelenmiþ = false;
        
    }
    private void Update()
    {
        CalculateGrowthMultiply();
        CheckEmptySlot();
    }

    private void CheckEmptySlot()
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
        else
        {
            targetSlot = null;
        }
    }

    private void CheckWaterAndFertilizerisEnough()
    {
        //Su seviyesi belli bir seviyenin üstünde ise;

        if (waterLevel >= 50)
        {
          sulanmýþ = true;
        }
        else
        {
            sulanmýþ=false;
        }
       
        //Gübre seviyesi belli bir seviyenin üstünde ise;
        if(fertilizerLevel >= 50)
        {
           gübrelenmiþ = true;
        }
        else
        {
            gübrelenmiþ=false;
        }
    }

    public float CalculateGrowthMultiply()
    {
        CheckWaterAndFertilizerisEnough();
        if (sulanmýþ)
        {
            büyütmeKatsayýsý = 50f;

            if (gübrelenmiþ)
            {
                büyütmeKatsayýsý += 50f;
            }
        }
        return büyütmeKatsayýsý;
    }

   
}
