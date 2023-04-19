using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Log Object", menuName = "Inventory System/Items/Logs")]
public class LogObject : ItemObject
{

    public void Awake()
    {
        type = ItemType.Log;
    }
}
