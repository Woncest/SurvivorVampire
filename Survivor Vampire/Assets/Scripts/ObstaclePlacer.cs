using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePlacer : MonoBehaviour
{
    public List<GameObject> obstaclePrefabs; // List of obstacle prefabs
    private Dictionary<GameObject, List<GameObject>> tileObstacleMap = new Dictionary<GameObject, List<GameObject>>(); // Dictionary to map tiles to obstacles

    // Function to place obstacles on a tile
    public void PlaceObstacles(GameObject tile)
    {
        // Remove existing obstacles if any
        RemoveObstacles(tile);

        // Get the position of the tile
        Vector3 position = tile.transform.position;

        // Get the bounds of the tile
        Bounds tileBounds = tile.GetComponent<Renderer>().bounds;

        // Calculate the position for the left half obstacle with some randomness
        float leftX = Random.Range(tileBounds.min.x + (tileBounds.size.x * 0.25f), tileBounds.center.x);
        float leftY = Random.Range(tileBounds.min.y, tileBounds.max.y);
        Vector3 leftPos = new Vector3(leftX, leftY, position.z);

        // Calculate the position for the right half obstacle with some randomness
        float rightX = Random.Range(tileBounds.center.x, tileBounds.max.x - (tileBounds.size.x * 0.25f));
        float rightY = Random.Range(tileBounds.min.y, tileBounds.max.y);
        Vector3 rightPos = new Vector3(rightX, rightY, position.z);

        // Choose two random obstacles from the list
        GameObject obstacle1 = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
        GameObject obstacle2 = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];

        // Instantiate the obstacles
        GameObject leftObstacle = Instantiate(obstacle1, leftPos, Quaternion.identity, tile.transform);
        GameObject rightObstacle = Instantiate(obstacle2, rightPos, Quaternion.identity, tile.transform);

        // Add the obstacles to the dictionary
        List<GameObject> obstacles = new List<GameObject>();
        obstacles.Add(leftObstacle);
        obstacles.Add(rightObstacle);
        tileObstacleMap[tile] = obstacles;
    }

    // Function to remove obstacles from a tile
    private void RemoveObstacles(GameObject tile)
    {
        if (tileObstacleMap.ContainsKey(tile))
        {
            List<GameObject> obstacles = tileObstacleMap[tile];
            foreach (GameObject obstacle in obstacles)
            {
                Destroy(obstacle);
            }
            tileObstacleMap.Remove(tile);
        }
    }
}
