using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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


    #region "CatButtons"
    Button toolsBtn;
    Button farmingBtn;
    Button buildingBtn;
    #endregion

    #region "ItemButtons"
    Button axeBtn;
    Button hammerBtn;
    Button hoeBtn;
    Button wateringCanBtn;
    Button tobaccoBtn;
    Button teaBtn;
    #endregion

    #region "CraftButtons"
    public Button axeCraftBtn;
    Button hammerCraftBtn;
    Button hoeCraftBtn;
    Button wateringCanCraftBtn;
    Button tobaccoCraftBtn;
    Button teaCraftBtn;
    #endregion

    #region "Tarif Fields"
    public TextMeshProUGUI selectedTarif;
    public TextMeshProUGUI axeTarif;
    public TextMeshProUGUI hammerTarif;

    #endregion


    #region "Category"
    public GameObject toolsCategory;
    public GameObject farmingCategory;
    public GameObject buildCategory;
    #endregion
    #region "Descriptions"
    public GameObject axeDescription;
    public GameObject hammerDescription;
    public GameObject hoeDescription;
    public GameObject wateringDescription;
    #endregion


    Item selectedItem = null;
    List<RecipeItem> recipeList = null;
    int craftItemAmount = 0;


    void Start()
    {
        #region "Mert'in amelelikleri"


        toolsBtn = craftingScreenUI.transform.Find("ToolButton").GetComponent<Button>();
        toolsBtn.onClick.AddListener(delegate { OpenToolsCategory(); });
        axeBtn = craftingScreenUI.transform.Find("AxeButtonItem").GetComponent<Button>();
        axeBtn.onClick.AddListener(delegate { OpenAxeDescription(); });
        hammerBtn = craftingScreenUI.transform.Find("HammerButtonItem").GetComponent<Button>();
        hammerBtn.onClick.AddListener(delegate { OpenHammerDescription(); });
        hoeBtn = craftingScreenUI.transform.Find("HoeButtonItem").GetComponent<Button>();
        hoeBtn.onClick.AddListener(delegate { OpenHammerDescription(); });
        wateringCanBtn = craftingScreenUI.transform.Find("WateringButtonItem").GetComponent<Button>();
        wateringCanBtn.onClick.AddListener(delegate { OpenWateringDescription(); });


        #endregion

    }
    void Update()
    {
        setDesciprtionText();
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

        selectedItem = database.ItemObjects[17].data;
        setRecipe(selectedItem);
        selectedTarif = hammerTarif;

    }
    public void OpenAxeDescription()
    {
        hoeDescription.SetActive(false);
        wateringDescription.SetActive(false);
        hammerDescription.SetActive(false);
        axeDescription.SetActive(true);

        selectedItem = database.ItemObjects[16].data;
        setRecipe(selectedItem);
        selectedTarif = axeTarif;


    }



    void setDesciprtionText()
    {
        if (selectedTarif != null)
        {
            selectedTarif.text = "";
            if (recipeList.Count > 0)
            {
                foreach (var recipe in recipeList)
                {
                    var slotItem = inventoryObject.GetSlots.Where(i => i.item.Id == recipe.Item.Id).FirstOrDefault();
                    int slotAmount = 0;

                    if (slotItem != null)
                    {
                        slotAmount = slotItem.amount;
                    }

                    selectedTarif.text += recipe.Quantity.ToString() + " " + recipe.Item.Name + " [" + slotAmount.ToString() + "]\n";
                }
            }

        }

    }

    void setRecipe(Item selectedItem)
    {
        recipeList = new List<RecipeItem>();
        foreach (var recipe in CraftingRecipe.MaterialList)
        {
            if (recipe.Item.Id == selectedItem.Id)
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

    public void onCraftButtonClicked()
    {
        if (selectedItem != null)
        {
            craftItem();
        }
    }

    void craftItem()
    {

        if (recipeList.Count == 0)
        {
            Debug.LogWarning("Recipe Cannot found for: " + selectedItem.Name + "(Id:" + selectedItem.Id + ")");
        }

        if (checkInventorySpace())
        {
            if (removeRecipeItemsInInventory(recipeList))
            {
                inventoryObject.AddItem(selectedItem, craftItemAmount); // inventory'e yeni craftlanmýþ itemi ekler
            }
        }
    }


    bool removeRecipeItemsInInventory(List<RecipeItem> recipeList)
    {

        foreach (var recipe in recipeList)
        {
            var slotItem = inventoryObject.GetSlots.Where(i => i.item.Id == recipe.Item.Id).FirstOrDefault();
            if (slotItem != null)
            {
                if (slotItem.amount > 0 && slotItem.amount >= recipe.Quantity)
                {

                }
                else
                {
                    Debug.LogWarning("You dont have enough required stuff. You need " + recipe.Quantity.ToString() + " piece of " + recipe.Item.Name + ". You have only " + slotItem.amount.ToString() + " piece in your inventory.");
                    return false;
                }
            }
            else
            {
                Debug.LogWarning("You dont have enough required stuff.");
                Debug.LogWarning("You dont have enough required stuff. You need " + recipe.Quantity.ToString() + " piece of " + recipe.Item.Name + ".");
                return false;
            }
        }

        foreach (var recipe in recipeList)
        {
            var slotItem = inventoryObject.GetSlots.Where(i => i.item.Id == recipe.Item.Id).FirstOrDefault();
            inventoryObject.RemoveByItem(recipe.Item, recipe.Quantity);
        }


        return true;
    }

    bool checkInventorySpace()
    {

        if (inventoryObject.EmptySlotCount > 0)
        {
            return true;
        }
        else
        {
            Debug.LogWarning("You don't have any space in your inventory");
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

