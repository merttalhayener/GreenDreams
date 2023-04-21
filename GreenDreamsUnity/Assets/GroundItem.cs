using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public ItemObject item;
    public bool collected;

    private void Start()
    {
        collected = false;
    }

    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {
        GetComponentInChildren<MeshFilter>().mesh = item.groundItem.GetComponent<MeshFilter>().sharedMesh;
        
        GetComponentInChildren<MeshRenderer>().material = item.groundItem.GetComponent<MeshRenderer>().sharedMaterial;
      

    }
}
