using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject Equipment;
    public GameObject ToolbeltOnScreen;
    public GameObject Toolbelt;
    public bool inventoryIsClosed;
    public bool equipmentIsClosed;
    public bool toolbeltOnScreenIsClosed;
    public bool toolbeltIsClosed;

   
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

                Inventory.SetActive(true);
                Equipment.SetActive(true);
                Toolbelt.SetActive(true);
                ToolbeltOnScreen.SetActive(false);
                inventoryIsClosed = false;
                equipmentIsClosed = false;
                toolbeltIsClosed = false;
                toolbeltOnScreenIsClosed = true;
            }
            else
            {

                Inventory.SetActive(false);
                Equipment.SetActive(false);
                Toolbelt.SetActive(false);
                ToolbeltOnScreen.SetActive(true);
                inventoryIsClosed = true;
                equipmentIsClosed = true;
                toolbeltIsClosed = true;
                toolbeltOnScreenIsClosed = false;
            }
        }

    }
}
