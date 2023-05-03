using System.Collections.Generic;
using UnityEngine;

public class ObjectTypeManager : MonoBehaviour
{
   [SerializeField] public ObjectType objectType;
   [SerializeField] public BuildingType buildingType;
   [SerializeField] public List<BuildingTypes> allowedType;

    private void FixedUpdate()
    {
        if (buildingType != null)
        {
            allowedType = buildingType.allowedType;
            
        }
    }

}
