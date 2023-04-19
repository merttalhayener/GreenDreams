using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Tree",menuName = "GameObjects/Type/Trees")]
public class Tree : ObjectType
{
    public float health;

    public void Awake()
    {
        type =Types.Tree;
        health = 100f;
    }
}
