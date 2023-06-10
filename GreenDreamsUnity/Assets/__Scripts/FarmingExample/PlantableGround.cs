using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlantableGround : MonoBehaviour
{
    public GameObject slot1;
    public GameObject slot2;
    public GameObject targetSlot;

    public bool sulanm��;
    public bool g�brelenmi�;

    public float waterLevel; //Clamp edilmeli
    public float fertilizerLevel; //Clamp edilmeli

    public float b�y�tmeKatsay�s�;

    //Bu script zemindeki slotlar� �ekip o slotlar�n bo� olup olmad���n� kontrol edecek.
    //E�er slotlar bo� ise ekim i�leminde bo� slota tohumun eklenmesini sa�layacak.(Yani hedef bir slot belirleyecek)
    //Zeminin sulanm�� olup olmad��� ve g�brelenmi� olup olmad��� burda tutulacak.

    private void Awake()
    {
        //Ba�lang�� de�erleri atand�
        b�y�tmeKatsay�s� = 0f;
        waterLevel = 50f;
        fertilizerLevel = 50f;

        sulanm�� = false;
        g�brelenmi� = false;
        
    }
    private void Update()
    {
        CalculateGrowthMultiply();
        CheckEmptySlot();
    }

    private void CheckEmptySlot()
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
        else
        {
            targetSlot = null;
        }
    }

    private void CheckWaterAndFertilizerisEnough()
    {
        //Su seviyesi belli bir seviyenin �st�nde ise;

        if (waterLevel >= 50)
        {
          sulanm�� = true;
        }
        else
        {
            sulanm��=false;
        }
       
        //G�bre seviyesi belli bir seviyenin �st�nde ise;
        if(fertilizerLevel >= 50)
        {
           g�brelenmi� = true;
        }
        else
        {
            g�brelenmi�=false;
        }
    }

    public float CalculateGrowthMultiply()
    {
        CheckWaterAndFertilizerisEnough();
        if (sulanm��)
        {
            b�y�tmeKatsay�s� = 50f;

            if (g�brelenmi�)
            {
                b�y�tmeKatsay�s� += 50f;
            }
        }
        return b�y�tmeKatsay�s�;
    }

   
}
