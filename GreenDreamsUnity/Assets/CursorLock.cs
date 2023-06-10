using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class CursorLock : MonoBehaviour
{
    [SerializeField] BuildingManager buildingManager;

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
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;

        inventoryIsClosed = false;
        equipmentIsClosed = false;
        toolbeltIsClosed = true;
        toolbeltOnScreenIsClosed = false;
        miniMapIsClosed = false;
        questBGIsClosed = false;
        weatherBGIsClosed = false;
        buildingPanelIsClosed = true;



        Inventory.SetActive(true);
        Equipment.SetActive(true);
        Toolbelt.SetActive(false);
        ToolbeltOnScreen.SetActive(true);
        miniMap.SetActive(true);
        questBG.SetActive(true);
        questPersonIMG.SetActive(true);
        weatherBG.SetActive(true);
        buildingCanvas.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryIsClosed && equipmentIsClosed && toolbeltIsClosed && toolbeltOnScreenIsClosed == false && miniMapIsClosed == false && weatherBGIsClosed == false)
            {
                Debug.Log("bast�");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
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
                questBGIsClosed = true;



                ////Alpha de�i�ikli�i
                //Color tmp = Inventory.GetComponent<Image>().color;
                //tmp.a = 1f;
                //Inventory.GetComponent<Image>().color = tmp;
            }
            else
            {
                Debug.Log("bidaha bast�");
                source.PlayOneShot(inventoryOpenSound, 0.3f);
                Inventory.SetActive(false);
                Equipment.SetActive(false);
                Toolbelt.SetActive(false);
                ToolbeltOnScreen.SetActive(true);
                miniMap.SetActive(true);
                weatherBG.SetActive(true);

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
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (buildingPanelIsClosed && inventoryIsClosed == true)
            {

                Player _player = (Player)Component.FindObjectsOfType(typeof(Player), false).FirstOrDefault();
                InventorySlot[] _slots = _player.equipment.GetSlots;
                var setActive = false;
                foreach (var slot in _slots)
                {
                    if (slot.item.Id == 17)
                    {
                        setActive = true;
                        break;
                    }
                }

                //craft men� i�in envanter tarama �rne�i
                ////  Player _player = (Player)Component.FindObjectsOfType(typeof(Player), false).FirstOrDefault();
                //  InventorySlot[] _slotsInv = _player.inventory.GetSlots;
                //  var _hammerAmount = 0;
                //  var _knifeAmount = 0;
                //  //var doluSlotlar = _slotsInv.Where(s => s.item.Id != -1);
                //  //var consumables = doluSlotlar.Where(x => x.item.GetType == ItemType.Consumables);
                //  foreach (var slot in _slotsInv.Where(s=>s.item.Id!=-1))
                //  {
                //      if (slot.item.Id == 17)
                //      {
                //          _hammerAmount = slot.amount;
                //      }
                //      if (slot.item.Id == 18)
                //      {
                //          _knifeAmount = slot.amount;
                //      }
                //  }

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                buildingCanvas.SetActive(setActive);
                buildingPanelIsClosed = false;
            }
            else if (!buildingPanelIsClosed)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                buildingCanvas.SetActive(false);
                buildingPanelIsClosed = true;
            }


        }
    }
}


