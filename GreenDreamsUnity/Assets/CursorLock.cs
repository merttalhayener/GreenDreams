using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorLock : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject Equipment;
    public GameObject ToolbeltOnScreen;
    public GameObject Toolbelt;
    public GameObject buildingCanvas;
    public bool inventoryIsClosed;
    public bool equipmentIsClosed;
    public bool toolbeltOnScreenIsClosed;
    public bool toolbeltIsClosed;


    [SerializeField] private AudioClip inventoryOpenSound;
    [SerializeField] private AudioSource source;
   
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        inventoryIsClosed = true;
        equipmentIsClosed = true;
        toolbeltIsClosed = true;
        toolbeltOnScreenIsClosed = false;


        Inventory.SetActive(false);
        Equipment.SetActive(false);
        Toolbelt.SetActive(false);
        ToolbeltOnScreen.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryIsClosed & equipmentIsClosed == true &toolbeltIsClosed ==true && toolbeltOnScreenIsClosed == false)
            {
                Debug.Log("bast�");
                source.PlayOneShot(inventoryOpenSound, 0.3f);
                Inventory.SetActive(true);
                Equipment.SetActive(true);
                Toolbelt.SetActive(true);
                ToolbeltOnScreen.SetActive(false);
                buildingCanvas.SetActive(false);
                inventoryIsClosed = false;
                equipmentIsClosed = false;
                toolbeltIsClosed = false;
                toolbeltOnScreenIsClosed = true;
               
                //Alpha de�i�ikli�i
                Color tmp = Inventory.GetComponent<Image>().color;
                tmp.a = 1f;
                Inventory.GetComponent<Image>().color = tmp;
            }
            else
            {
                Debug.Log("bidaha bast�");
                source.PlayOneShot(inventoryOpenSound, 0.3f);
                Inventory.SetActive(false);
                Equipment.SetActive(false);
                Toolbelt.SetActive(false);
                ToolbeltOnScreen.SetActive(true);
                buildingCanvas.SetActive(true);
                inventoryIsClosed = true;
                equipmentIsClosed = true;
                toolbeltIsClosed = true;
                toolbeltOnScreenIsClosed = false;
            }
        }

    }
}
