using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

[CreateAssetMenu(fileName = "Prefab brush " , menuName ="Brushes/Prefab brush")]
[CustomGridBrush(hideAssetInstances:false,hideDefaultInstance: true,defaultBrush: false,defaultName: "Prefab Brush")]

public class PrefabBrush : GameObjectBrush
{
    
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}
