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
    
    public GameObject buildingCanvas;
    public GameObject questBG;
    public GameObject questPersonIMG;
    public GameObject miniMap;
    public GameObject weatherBG;
    public GameObject craftBG;




    public int activeChildIndex = 1;

    public bool inventoryIsClosed;
    public bool equipmentIsClosed;
    public bool toolbeltOnScreenIsClosed;
  
    public bool miniMapIsClosed;
    public bool questBGIsClosed;
    public bool weatherBGIsClosed;
    public bool buildingPanelIsClosed;
    public bool craftingBGIsClosed;



    [SerializeField] private AudioClip inventoryOpenSound;
    [SerializeField] private AudioSource source;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;

        inventoryIsClosed = false;
        equipmentIsClosed = false;
       
        //toolbeltOnScreenIsClosed = false;
        miniMapIsClosed = false;
        questBGIsClosed = false;
        weatherBGIsClosed = false;
        buildingPanelIsClosed = true;
        craftingBGIsClosed = false;
        



        Inventory.SetActive(true);
        Equipment.SetActive(true);
        
        //ToolbeltOnScreen.SetActive(true);
        miniMap.SetActive(true);
        questBG.SetActive(true);
        questPersonIMG.SetActive(true);
        weatherBG.SetActive(true);
        buildingCanvas.SetActive(false);
        craftBG.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryIsClosed && equipmentIsClosed && miniMapIsClosed == false && weatherBGIsClosed == false)
            {
                Debug.Log("bastý");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                source.PlayOneShot(inventoryOpenSound, 0.3f);
                Inventory.SetActive(true);
                Equipment.SetActive(true);
               
                //ToolbeltOnScreen.SetActive(true);
                miniMap.SetActive(false);
                questBG.SetActive(false);
                questPersonIMG.SetActive(false);
                weatherBG.SetActive(false);
                buildingCanvas.SetActive(false);
                craftBG.SetActive(false);

                inventoryIsClosed = false;
                equipmentIsClosed = false;
                
                //toolbeltOnScreenIsClosed = false;
                miniMapIsClosed = true;
                questBGIsClosed = true;
                weatherBGIsClosed = true;
                questBGIsClosed = true;
                craftingBGIsClosed = true;



                ////Alpha deðiþikliði
                //Color tmp = Inventory.GetComponent<Image>().color;
                //tmp.a = 1f;
                //Inventory.GetComponent<Image>().color = tmp;
            }
            else
            {
                Debug.Log("bidaha bastý");
                source.PlayOneShot(inventoryOpenSound, 0.3f);
                Inventory.SetActive(false);
                Equipment.SetActive(false);
                
                //ToolbeltOnScreen.SetActive(true);
                miniMap.SetActive(true);
                weatherBG.SetActive(true);
                

                inventoryIsClosed = true;
                equipmentIsClosed = true;
                
                //toolbeltOnScreenIsClosed = false;
                miniMapIsClosed = false;
                weatherBGIsClosed = false;
                
            }

        }
         if (Input.GetKeyDown(KeyCode.C))
        {
            if (craftingBGIsClosed)
            {
                craftBG.SetActive(true);
                craftingBGIsClosed = false;
            }
            else if (craftingBGIsClosed == false)
            {
                craftBG.SetActive(false);
                craftingBGIsClosed = true;
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

                //craft menü için envanter tarama örneði
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


