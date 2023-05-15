using UnityEngine;

[CreateAssetMenu(fileName = "NULLObjects", menuName = "BuildingObjects/Type/NULLObjects")]

public class NULLObject : BuildingType
{
    //  public BuildingTypes[] PlaceableTypes;

    public void Awake()
    {
        type = BuildingTypes.NULLObject;
    }
}
