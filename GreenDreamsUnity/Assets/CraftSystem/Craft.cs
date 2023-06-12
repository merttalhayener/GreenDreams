using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Craft : MonoBehaviour
{

    public Button _craftButton;
    public GameObject craftingScreenUI;
    public GameObject textObject;
    public CraftingRecipe CraftingRecipe;
    public InventoryObject inventoryObject;
    public InventorySlot inventorySlot;

    public ItemDatabaseObject database;

    //CatButtons
    Button toolsBtn;
    Button farmingBtn;
    Button buildingBtn;
    //ItemButton
    Button axeBtn;
    Button hammerBtn;
    Button hoeBtn;
    Button wateringCanBtn;
    Button tobaccoBtn;
    Button teaBtn;
    //CraftButton
    Button axeCraftBtn;
    Button hammerCraftBtn;
    Button hoeCraftBtn;
    Button wateringCanCraftBtn;
    Button tobaccoCraftBtn;
    Button teaCraftBtn;

    //Category
    public GameObject toolsCategory;
    public GameObject farmingCategory;
    public GameObject buildCategory;
    //Descriptions
    public GameObject axeDescription;
    public GameObject hammerDescription;
    public GameObject hoeDescription;
    public GameObject wateringDescription;
    // Start is called before the first frame update
    void Start()
    {
        //_craftButton = craftingScreenUI.transform.Find("CraftButton").GetComponent<Button>();
        //_craftButton = GameObject.Find("CraftButton").GetComponent<Button>();
        //_craftButton.onClick.AddListener(delegate { onCraftButtonClicked(); });

        toolsBtn = craftingScreenUI.transform.Find("ToolButton").GetComponent<Button>();
        toolsBtn.onClick.AddListener(delegate { OpenToolsCategory(); });
        axeBtn = craftingScreenUI.transform.Find("AxeButtonItem").GetComponent<Button>();
        axeBtn.onClick.AddListener(delegate { OpenAxeDescription(); });
        hammerBtn = craftingScreenUI.transform.Find("HammerButtonItem").GetComponent<Button>();
        hammerBtn.onClick.AddListener(delegate{OpenHammerDescription(); });
        hoeBtn = craftingScreenUI.transform.Find("HoeButtonItem").GetComponent<Button>();
        hoeBtn.onClick.AddListener(delegate { OpenHammerDescription(); });
        wateringCanBtn = craftingScreenUI.transform.Find("WateringButtonItem").GetComponent<Button>();
        wateringCanBtn.onClick.AddListener(delegate { OpenWateringDescription(); });
        
       
    }
    void Update()
    {

    }

    public void OpenWateringDescription()
    {
        axeDescription.SetActive(false);
        hammerDescription.SetActive(false);
        hoeDescription.SetActive(false);
        wateringDescription.SetActive(true);
    }
    public void OpenHoeDescription()
    {
        wateringDescription.SetActive(false);
        axeDescription.SetActive(false);
        hammerDescription.SetActive(false);
        hoeDescription.SetActive(true);
        
    }
    public void OpenHammerDescription()
    {
        hoeDescription.SetActive(false);
        wateringDescription.SetActive(false);
        axeDescription.SetActive(false);
        hammerDescription.SetActive(true);
        
    }
    public void OpenAxeDescription()
    {
        hoeDescription.SetActive(false);
        wateringDescription.SetActive(false);
        hammerDescription.SetActive(false);
        axeDescription.SetActive(true);
    }
    public void OpenToolsCategory()
    {
        buildCategory.SetActive(false);
        farmingCategory.SetActive(false);
        toolsCategory.SetActive(true);
    }
    public void OpenFarmingCategory()
    {
        buildCategory.SetActive(false);
        toolsCategory.SetActive(false);
        farmingCategory.SetActive(true);
    }
    public void OpenBuildingCategory()
    {
        toolsCategory.SetActive(false);
        farmingCategory.SetActive(false);
        buildCategory.SetActive(true);
    }

    void onCraftButtonClicked()
    {
        int itemId = int.Parse(GameObject.Find("itemId").GetComponent<TMPro.TextMeshProUGUI>().text);
        var item = database.ItemObjects[itemId].data;
        if (item != null)
        {
            craftItem(item);
        }
    }

    void craftItem(Item item)
    {
        int craftItemAmount = 0;
        List<RecipeItem> recipeList = new List<RecipeItem>();

        foreach (var recipe in CraftingRecipe.MaterialList)
        {
            if (recipe.Item.Id == item.Id)
            {
                craftItemAmount = recipe.Amount;
                foreach (var metarialItem in recipe.Metarials)
                {
                    using (RecipeItem recipeItem = new RecipeItem())
                    {
                        recipeItem.Item = database.ItemObjects[metarialItem.Item.Id].data;
                        recipeItem.Quantity = metarialItem.Amount;
                        recipeList.Add(recipeItem);
                    }
                }
            }
        }


        int slotItemCount = getRecipeItemCountInInventory(recipeList);

        if (slotItemCount>0)
        {
            if (checkInventorySpace())
                {
                foreach (var recipe in recipeList)
                {
                   // inventorySlot.UpdateSlot(recipe.Item, slotItemCount-recipe.Quantity); //inventory slotunda ki gerekli itemlar� kald�r�r

                    inventoryObject.RemoveByItem(recipe.Item, slotItemCount - recipe.Quantity);
                }
                inventoryObject.AddItem(item, craftItemAmount); // inventory'e yeni craftlanm�� itemi ekler
            }
            else
            {
                Debug.LogWarning("You don't have the required materials.");
            }
        }
        else
        {
            Debug.LogWarning("You don't have any space");
        }
    }






    int getRecipeItemCountInInventory(List<RecipeItem> recipeList)
    {
        //inventory kontrol edilecek
        Player _player = (Player)Component.FindObjectsOfType(typeof(Player), false).FirstOrDefault();
        InventorySlot[] _slots = _player.inventory.GetSlots;
        foreach (var recipeItem in recipeList)
        {
            foreach (var slot in _slots)
            {
                if (slot.item.Id == recipeItem.Item.Id && slot.amount >= recipeItem.Quantity)
                {
                   return slot.amount;
                }
            }
        }

        return 0;
    }




    bool checkInventorySpace()
    {

        if (inventoryObject.EmptySlotCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

       

    }

}


public class RecipeItem : IDisposable
{
    private bool disposedValue;

    public Item Item { get; set; }
    public int Quantity { get; set; }



    #region Dispose methods
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~RecipeItem()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    void IDisposable.Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion
}

