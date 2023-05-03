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
    public GameObject questBG;
    public GameObject questPersonIMG;
    public GameObject miniMap;
    public GameObject weatherBG;
    


    public int activeChildIndex = 1;

    public bool inventoryIsClosed;
    public bool equipmentIsClosed;
    public bool toolbeltOnScreenIsClosed;
    public bool toolbeltIsClosed;
    public bool miniMapIsClosed;
    public bool questBGIsClosed;
    public bool weatherBGIsClosed;
    public bool buildingPanelIsClosed;
    

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
        miniMapIsClosed = false;
        questBGIsClosed = false;
        weatherBGIsClosed = false;
        buildingPanelIsClosed = false;


        Inventory.SetActive(false);
        Equipment.SetActive(false);
        Toolbelt.SetActive(false);
        ToolbeltOnScreen.SetActive(true);
        miniMap.SetActive(true);
        questBG.SetActive(true);
        questPersonIMG.SetActive(true);
        weatherBG.SetActive(true);
        buildingCanvas.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryIsClosed && equipmentIsClosed && toolbeltIsClosed && toolbeltOnScreenIsClosed == false && miniMapIsClosed == false &&weatherBGIsClosed==false)
            {
                Debug.Log("bastý");
                source.PlayOneShot(inventoryOpenSound, 0.3f);
                Inventory.SetActive(true);
                Equipment.SetActive(true);
                Toolbelt.SetActive(true);
                ToolbeltOnScreen.SetActive(false);
                miniMap.SetActive(false);
                questBG.SetActive(false);
                questPersonIMG.SetActive(false);
                weatherBG.SetActive(false);
                buildingCanvas.SetActive(false);
                inventoryIsClosed = false;
                equipmentIsClosed = false;
                toolbeltIsClosed = false;
                toolbeltOnScreenIsClosed = true;
                miniMapIsClosed = true;
                questBGIsClosed = true;
                weatherBGIsClosed = true;


                //Alpha deðiþikliði
                Color tmp = Inventory.GetComponent<Image>().color;
                tmp.a = 1f;
                Inventory.GetComponent<Image>().color = tmp;
            }
            else
            {
                Debug.Log("bidaha bastý");
                source.PlayOneShot(inventoryOpenSound, 0.3f);
                Inventory.SetActive(false);
                Equipment.SetActive(false);
                Toolbelt.SetActive(false);
                ToolbeltOnScreen.SetActive(true);
                miniMap.SetActive(true);
                weatherBG.SetActive(true);
                buildingCanvas.SetActive(true);
                inventoryIsClosed = true;
                equipmentIsClosed = true;
                toolbeltIsClosed = true;
                toolbeltOnScreenIsClosed = false;
                miniMapIsClosed = false;
                weatherBGIsClosed = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (questBGIsClosed == true && inventoryIsClosed == true)
            {
                questBG.SetActive(true);
                questPersonIMG.SetActive(true);
                questBGIsClosed = false;

            }
            else if (questBGIsClosed == false)
            {
                questBG.SetActive(false);
                questPersonIMG.SetActive(true);
                questBGIsClosed = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            if (buildingPanelIsClosed && inventoryIsClosed == true)
            {
                buildingCanvas.SetActive(true);
                buildingPanelIsClosed = false;
            }
            else if(!buildingPanelIsClosed )
            {
                buildingCanvas.SetActive(false);
                buildingPanelIsClosed = true;
            }
        }

       
    }
    
}
