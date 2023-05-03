using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlaceableOnGround", menuName = "BuildingObjects/Type/PlaceableOnGround")]
public class PlaceableOnGround : BuildingType
{
    //public BuildingTypes[] PlaceableTypes;

    public void Awake()
    {
        type = BuildingTypes.PlaceableOnGround;
    }
}
