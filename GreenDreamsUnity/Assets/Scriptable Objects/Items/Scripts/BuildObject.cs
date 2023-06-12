using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Build Object", menuName = "Inventory System/Items/Build")]
public class BuildObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Build;
    }
}
