using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FarmZone", menuName = "GameObjects/Type/FarmZone")]
public class FarmZone : ObjectType
{
  

    private void Awake()
    {
        type = Types.FarmZone;
    }
}
