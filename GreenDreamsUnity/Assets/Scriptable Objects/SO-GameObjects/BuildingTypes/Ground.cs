using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ground", menuName = "BuildingObjects/Type/Ground")]
public class Ground : BuildingType
{
  //  public BuildingTypes[] PlaceableTypes;

    public void Awake()
    {
        type = BuildingTypes.Ground;
    }
}
