using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class SelectManager : MonoBehaviour
{
    public GameObject selectedObject;
    public TextMeshProUGUI objNameText;
    private BuildingManager buildingManager;
    public  GameObject objectUI;
    public LayerMask buildingLayer;
    public CheckPlacement checkPlacement;
    

    private void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>() ;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
           
            if (Physics.Raycast(ray,out hit , 1000 , buildingLayer))
            {
             if (hit.collider.gameObject.CompareTag("Objects")&& buildingManager.pendingObject == null && !IsMouseOverUI())
             {
                    hit.collider.gameObject.GetComponent<MeshFilter>().mesh.UploadMeshData(false);
                    Select(hit.collider.gameObject);
                   
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && selectedObject!=null)
        {
            Deselect();
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    private void Select (GameObject obj)
    {
        if (obj==selectedObject)
        {
            return;
        }

        if (selectedObject !=null)
        {
            Deselect();
        }

        Outline outLine = obj.GetComponent<Outline>();
      
        if (outLine==null )
        {
            obj.AddComponent<Outline>();
        }

        else if(outLine!=null )
        {
            outLine.enabled = true;
        }
            
            objNameText.text = obj.name;
            objectUI.SetActive(true);
            selectedObject = obj;
       
    }

    private void Deselect()
    {
        if (selectedObject != null)
        {
            objectUI.SetActive(false);
            selectedObject.GetComponent<Outline>().enabled = false;
            selectedObject = null;
            
          
        }
    }

    public void Move()
    {
        buildingManager.pendingObject = selectedObject;
        
        checkPlacement = selectedObject.GetComponent<CheckPlacement>();
        selectedObject.GetComponent<Outline>().enabled = false;

        if (checkPlacement != null) 
        {
            checkPlacement.isPlaced = false;
        }
    }

    public void Delete()
    {
        GameObject objToDestroy = selectedObject;
        Deselect();
        Destroy(objToDestroy);
    }
}
