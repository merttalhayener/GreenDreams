using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPlacement : MonoBehaviour
{
    public GameObject parentBuilding;

    [SerializeField] private BuildingManager buildingManager;

    [SerializeField] private ObjectTypeManager parentTypeManager;
    [SerializeField] private ObjectTypeManager otherBuildingTypeManager;
    [SerializeField] public List<BuildingTypes> buildingAllowedType;

    public BuildingTypes parentbuildingType;
    public BuildingTypes otherBuildingType;

    public bool isPlaced;

    void Start()
    {
        isPlaced = false;
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();

        parentTypeManager =parentBuilding.gameObject.GetComponent<ObjectTypeManager>();
        parentbuildingType = parentTypeManager.buildingType.type;
        buildingAllowedType = parentTypeManager.allowedType;
    }

   
    private void OnTriggerStay(Collider other)
    {
        if (isPlaced == false)
        {
            if (other.gameObject.CompareTag("Objects") && other.gameObject != null)
            {
                otherBuildingTypeManager = other.GetComponent<ObjectTypeManager>();
                otherBuildingType = otherBuildingTypeManager.buildingType.type;
                

                

                for (int i = 0; i < buildingAllowedType.Count; i++)
                {
                    if (otherBuildingType == buildingAllowedType[i])
                    {
                        Debug.Log("You can build here");
                        buildingManager.canPlace = true;
                    }
                    else
                    {
                        Debug.Log("You cant build here");
                        buildingManager.canPlace = false;
                    }
                }
            }
        }
        else
        {
            return;
        }
    }
}
