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

    public BuildingTypes buildingType;
    public BuildingTypes otherBuildingType;

    public bool isPlaced;

    void Start()
    {
        isPlaced = false;
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();

        parentTypeManager =parentBuilding.GetComponent<ObjectTypeManager>();
        buildingType = parentTypeManager.buildingType.type;
    }

   
    private void OnTriggerStay(Collider other)
    {
        if (isPlaced == false)
        {
            if (other.gameObject.CompareTag("Objects") && other != null)
            {
                //Debug.Log("Other : " + other.gameObject.name);
                buildingManager.canPlace = false;

                otherBuildingTypeManager = other.GetComponent<ObjectTypeManager>();
                otherBuildingType = otherBuildingTypeManager.buildingType.type;
                buildingAllowedType = parentTypeManager.allowedType;

                Debug.Log("Building Type : " + buildingType);
                Debug.Log("Collide With : " + otherBuildingType);

                for (int i = 0; i < buildingAllowedType.Count; i++)
                {
                    if (buildingType == buildingAllowedType[i])
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
    }
}
