using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public enum Types
    {
        Tree,
        Rock,
        Default
    }

public class ObjectType : ScriptableObject
{
    public Types type;
}
