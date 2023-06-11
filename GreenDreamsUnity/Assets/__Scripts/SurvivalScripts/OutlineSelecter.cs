using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelecter : MonoBehaviour
{
    public GameObject selectedObject;
   [SerializeField] private GameObject player;
    public float distance;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.CompareTag("Selectable"))
                {
                    //hit.collider.gameObject.GetComponent<MeshFilter>().mesh.UploadMeshData(false);
                    Select(hit.collider.gameObject);
                }
            }
        }

        if (selectedObject != null)
        {
        distance = Vector3.Distance(selectedObject.transform.position, player.transform.position);
        }
       
        if (Input.GetMouseButtonDown(1) && selectedObject != null || distance > 5f)
        {
            Deselect();
        }
    }
    private void Select(GameObject obj)
    {
        if (obj == selectedObject)
        {
            return;
        }

        if (selectedObject != null)
        {
            Deselect();
        }

        Outline outLine = obj.GetComponent<Outline>();

        if (outLine == null)
        {
            obj.AddComponent<Outline>();
        }

        else
        {
            outLine.enabled = true;
        }
       // objNameText.text = obj.name;
       //objectUI.SetActive(true);
        selectedObject = obj;
    }
    private void Deselect()
    {
       // objectUI.SetActive(false);
       if(selectedObject != null)
        {
            selectedObject.GetComponent<Outline>().enabled = false;
            selectedObject = null;
        }
      
    }
}
