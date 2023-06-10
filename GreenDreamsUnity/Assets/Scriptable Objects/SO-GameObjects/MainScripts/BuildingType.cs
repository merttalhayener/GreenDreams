using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum BuildingTypes
    {
       Ground,
       Walls,
       PlaceableOnGround,
       Farm,
       NULLObject
    }

    public class BuildingType : ScriptableObject
    {
        public BuildingTypes type;
        public List<BuildingTypes> allowedType;
    }

