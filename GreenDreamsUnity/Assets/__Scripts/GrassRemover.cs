using UnityEngine;

public class GrassRemover : MonoBehaviour
{
    public Terrain terrain; // Reference to the Terrain component

    [SerializeField]
    private float radius = 5f; // Radius of the area to remove grass

    private bool isPlaced = false; // Flag to track if an object is placed

    private int[][,] originalMaps; // Original detail maps for each detail layer

    private void Awake()
    {
        terrain = FindObjectOfType<Terrain>();
        StoreOriginalMaps();
        RemoveGrassInRadius(terrain, transform.position, radius);
    }

    private void OnDestroy()
    {
        if (!isPlaced && !Application.isPlaying)
        {
            RestoreOriginalMaps();
        }
    }

    private void OnApplicationQuit()
    {
        if (!isPlaced)
        {
            RestoreOriginalMaps();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && !isPlaced)
        {
            RestoreOriginalMaps();
        }
    }

    // Store the original detail maps for each detail layer.
    void StoreOriginalMaps()
    {
        TerrainData terrainData = terrain.terrainData;
        int layerCount = terrainData.detailPrototypes.Length;
        originalMaps = new int[layerCount][,];

        for (int layer = 0; layer < layerCount; layer++)
        {
            originalMaps[layer] = terrainData.GetDetailLayer(0, 0, terrainData.detailWidth, terrainData.detailHeight, layer);
        }
    }

    // Restore the original detail maps for each detail layer.
    void RestoreOriginalMaps()
    {
        TerrainData terrainData = terrain.terrainData;
        int layerCount = terrainData.detailPrototypes.Length;

        for (int layer = 0; layer < layerCount; layer++)
        {
            terrainData.SetDetailLayer(0, 0, layer, originalMaps[layer]);
        }
    }

    // Set the grass in the specified radius around the position to zero for all detail layers.
    void RemoveGrassInRadius(Terrain terrain, Vector3 position, float radius)
    {
        TerrainData terrainData = terrain.terrainData;

        // Iterate over all detail layers.
        for (int layer = 0; layer < terrainData.detailPrototypes.Length; layer++)
        {
            int[,] map = terrainData.GetDetailLayer(0, 0, terrainData.detailWidth, terrainData.detailHeight, layer);

            // Convert the position to terrain local coordinates.
            Vector3 localPosition = position - terrain.transform.position;
            Vector3 normalizedPosition = new Vector3(localPosition.x / terrainData.size.x, 0, localPosition.z / terrainData.size.z);
            Vector2Int detailPosition = new Vector2Int((int)(normalizedPosition.x * terrainData.detailWidth), (int)(normalizedPosition.z * terrainData.detailHeight));

            // Calculate the detail position and radius in detail map coordinates.
            int detailRadius = Mathf.RoundToInt(radius / terrainData.size.x * terrainData.detailWidth);

            // Iterate over the detail map within the specified radius and set the grass to zero.
            for (int y = detailPosition.y - detailRadius; y <= detailPosition.y + detailRadius; y++)
            {
                for (int x = detailPosition.x - detailRadius; x <= detailPosition.x + detailRadius; x++)
                {
                    // Check if the current position is within the detail map bounds.
                    if (x >= 0 && x < terrainData.detailWidth && y >= 0 && y < terrainData.detailHeight)
                    {
                        // Check if the current position is within the specified radius.
                        if (Vector2Int.Distance(detailPosition, new Vector2Int(x, y)) <= detailRadius)
                        {
                            // Set the grass at the current position to zero.
                            map[y, x] = 0;
                        }
                    }
                }
            }

            // Assign the modified map back.
            terrainData.SetDetailLayer(0, 0, layer, map);
        }
    }

    // Draw the radius gizmo for visualization purposes.
    private void OnDrawGizmosSelected()
    {
        if (terrain != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
