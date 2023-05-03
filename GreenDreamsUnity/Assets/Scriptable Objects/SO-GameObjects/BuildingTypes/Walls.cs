using UnityEngine;

[CreateAssetMenu(fileName = "Walls", menuName = "BuildingObjects/Type/Walls")]

public class Walls : BuildingType
{
  //  public BuildingTypes[] PlaceableTypes;

    public void Awake()
    {
        type = BuildingTypes.Walls;
    }
}
