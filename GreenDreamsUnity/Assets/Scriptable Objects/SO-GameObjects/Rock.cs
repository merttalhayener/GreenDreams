using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rock", menuName = "GameObjects/Type/Rocks")]
public class Rock : ObjectType
{
    public float hardness;

    private void Awake()
    {
        hardness = 1500f;
        
    }
}
