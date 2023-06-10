
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    // Start is called before the first frame update

    [Serializable]
    public struct CraftItem
    {
        public Item Item;
        [Range(1, 999)]
        public int Amount;
        public List<CraftItem> Metarials;
    }


    public List<CraftItem> MaterialList;
    // public List<ItemAmount> Results;

}




