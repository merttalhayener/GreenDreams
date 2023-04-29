using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wall", menuName = "BuildingObjects/Type/Wall")]
public class Wall : BuildingType
{
    public BuildingTypes[] PlaceableTypes;

    public void Awake()
    {
        type = BuildingTypes.Wall;
    }
}
