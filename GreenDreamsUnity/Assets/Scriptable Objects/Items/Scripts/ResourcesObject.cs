using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Log Object", menuName = "Inventory System/Items/Resources")]
public class ResourcesObject : ItemObject
{

    public void Awake()
    {
        type = ItemType.Resources;
    }
}
