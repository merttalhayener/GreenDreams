using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Farm", menuName = "BuildingObjects/Type/Farm")]
public class Farm : BuildingType
{
    //  public BuildingTypes[] PlaceableTypes;

    public void Awake()
    {
        type = BuildingTypes.Farm;
    }
}
