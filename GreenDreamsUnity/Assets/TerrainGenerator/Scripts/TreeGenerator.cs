
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class TreeGenerator : MonoBehaviour
{
    [SerializeField] private bool createOnStart;
    [SerializeField] private List<GameObject> trees;
    [SerializeField] private float minSize, maxSize;

    [SerializeField] private GameObject nextTree;
    private void Start()
    {
        if (!createOnStart) return;
        var terrain = GetComponent<Terrain>();
        var terrainData = terrain.terrainData;
        foreach (var terrainTree in terrainData.treeInstances)
        {
           
            Debug.Log(terrainTree.prototypeIndex);

            nextTree = trees.ElementAt(terrainTree.prototypeIndex);
              
            var worldTreePos = Vector3.Scale(terrainTree.position, terrainData.size) + Terrain.activeTerrain.transform.position;
            var tree = Instantiate(nextTree, worldTreePos, Quaternion.identity, transform);
            tree.transform.localScale = Vector3.one * Random.Range(minSize, maxSize);
            tree.transform.rotation = Quaternion.AngleAxis(Random.Range(-360f, 360f), Vector3.up);
        }
        terrain.treeDistance = 0;
    }
}
