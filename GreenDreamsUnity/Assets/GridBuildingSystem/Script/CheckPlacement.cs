using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    [SerializeField] private BuildingManager buildingManager;

    [SerializeField]private ObjectTypeManager buildingTypeManager;
    [SerializeField] private ObjectTypeManager otherBuildingTypeManager;

    public BuildingTypes buildingType;
    public BuildingTypes otherBuildingType;
    

    private Collider mainCollider;
    private Collider meshCollider;
    
    public Rigidbody rb;
    [SerializeField] LayerMask groundLayer;

    public bool isPlaced;
    
    void Start()
    {
        isPlaced = false;
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();

        buildingTypeManager = GetComponent<ObjectTypeManager>();
        buildingType = buildingTypeManager.buildingType.type;

    }

    private void FixedUpdate()
    {
        
        //SurfaceAlignment();
        if (isPlaced==false)
        {
            DisableSlots();
        }

        else
        {
            EnableSlots();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ( other.gameObject.CompareTag("Objects")  && isPlaced ==false)
        {
            otherBuildingTypeManager = other.GetComponent<ObjectTypeManager>();
            otherBuildingType = otherBuildingTypeManager.buildingType.type;
          
            Debug.Log( "Building Type : " +  buildingType);
            Debug.Log("Collide With : " + otherBuildingType);


            buildingManager.canPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.gameObject.CompareTag("Objects")  && isPlaced == false)
        {
            otherBuildingTypeManager = null;
            buildingManager.canPlace = true;
        }
    }

    private void SurfaceAlignment()
    {
        Ray ray = new Ray(transform.position,-transform.up);
        RaycastHit info= new RaycastHit();

        if (Physics.Raycast(ray,out info, groundLayer))
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, info.normal);
        }
    }

    private void DisableSlots()
    {
        mainCollider = GetComponent<BoxCollider>();
        mainCollider.isTrigger = true;

        Transform[] allChildren =  GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child != allChildren[0]&& child.gameObject.tag == "Spots")
            {
                child.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    private void EnableSlots()
    {
        mainCollider = GetComponent<BoxCollider>();
        mainCollider.isTrigger = false;

        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child != allChildren[0] && child.gameObject.tag == "Spots")
            {
                child.gameObject.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }
}
