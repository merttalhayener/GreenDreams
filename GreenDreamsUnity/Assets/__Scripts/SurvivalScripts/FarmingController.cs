using Dputils.Systems.DateTime;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FarmingController : MonoBehaviour
{
    [SerializeField] private OutlineSelecter selector;
    [SerializeField] private PlantableGround plantableGround;
    [SerializeField] private TimeManager timeManager;
    public ObjectTypeManager typeManager;
    public GameObject seed;
    

    private void Start()
    {
         timeManager = FindObjectOfType<TimeManager>();
         SetTag("Selectable");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && selector.selectedObject != null )
        {
            plantableGround = selector.selectedObject.GetComponent<PlantableGround>();
            typeManager = selector.selectedObject.GetComponent<ObjectTypeManager>();
            
            CheckHarvestPlants();
            Plant();

        }
    }
    public void SetTag(string newTag)
    {
        gameObject.tag = newTag;
    }

    public void Plant()
    {
        if (typeManager.objectType.type.ToString() == "FarmZone")
        {
            //Equip kontrolü
            plantableGround.CheckEmptySlot(); 
            plantableGround.PlantSeedToFarm(seed);
        }
        else
        {
            return;
        }
    }

    public void CheckHarvestPlants()
    {
        if (typeManager.objectType.type.ToString() == "FarmZone" )
        {
            plantableGround = selector.selectedObject.GetComponent<PlantableGround>();
            typeManager = selector.selectedObject.GetComponent<ObjectTypeManager>();

            foreach (GameObject plant in plantableGround.plants)
            {
                PlantManager plantManager = plant.GetComponent<PlantManager>();

                if (plantManager != null && plantManager.canHarvest)
                {
                    plantableGround.plants.Remove(plant);
                    timeManager.plantList.Remove(plant);
                    plantManager.HarvestThis();
                }
            }
        }
        else
        {
            return;
        }
    }
}
