using Dputils.Systems.DateTime;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FarmingController : MonoBehaviour
{
    [SerializeField] private OutlineSelecter selector;
    [SerializeField] private PlantableGround plantableGround;
    public ObjectTypeManager typeManager;
    public GameObject seed;
  


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && selector.selectedObject != null )
        {
            plantableGround = selector.selectedObject.GetComponent<PlantableGround>();
            typeManager = selector.selectedObject.GetComponent<ObjectTypeManager>();
            Plant();
        }
    }

    public void Plant()
    {
        if (typeManager.objectType.type.ToString() == "FarmZone")
        {
            //Equip kontrolü
            plantableGround.CheckEmptySlot(); 
            plantableGround.PlantSeedToFarm(seed);
        }
    }
   
}
