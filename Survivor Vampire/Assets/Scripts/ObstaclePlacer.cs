using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
        Vector3 leftPos = CalculatePosition(tileBounds.min.x + (tileBounds.size.x * 0.25f), tileBounds.center.x, tileBounds.min.y, tileBounds.max.y, position.z);

        // Calculate the position for the right half obstacle with some randomness
        Vector3 rightPos = CalculatePosition(tileBounds.center.x, tileBounds.max.x - (tileBounds.size.x * 0.25f), tileBounds.min.y, tileBounds.max.y, position.z);

        // Choose two random obstacles from the list
        GameObject obstacle1 = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
        GameObject obstacle2 = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];

        // Instantiate the obstacles, ensuring they don't collide with existing obstacles
        GameObject leftObstacle = InstantiateWithCollisionCheck(obstacle1, leftPos, tile.transform);
        CheckIfLeftObstaclePlaced(tile, position, ref tileBounds, ref leftPos, ref obstacle1, ref leftObstacle);
        GameObject rightObstacle = InstantiateWithCollisionCheck(obstacle2, rightPos, tile.transform);
        CheckIfRightObstaclePlaced(tile, position, tileBounds, ref rightPos, ref obstacle2, ref rightObstacle);

        // Add the obstacles to the dictionary
        List<GameObject> obstacles = new List<GameObject>();
        obstacles.Add(leftObstacle);
        obstacles.Add(rightObstacle);
        tileObstacleMap[tile] = obstacles;
    }

    // Function to calculate position with bias towards the center
    private Vector3 CalculatePosition(float min, float max, float minY, float maxY, float zPos)
    {
        // Calculate a random position within the allowed area with bias towards the center
        float rand = Random.Range(0f, 1f);
        float center = (min + max) / 2;
        float positionX;
        if (rand < 0.5f)
        {
            // Closer to the center
            positionX = Mathf.Lerp(center, max, Random.Range(0f, 1f));
        }
        else
        {
            // Closer to the min or max edge
            positionX = Mathf.Lerp(min, center, Random.Range(0f, 1f));
        }

        float positionY = Random.Range(minY, maxY);

        return new Vector3(positionX, positionY, zPos);
    }

    // Function to instantiate an obstacle with collision check
    private GameObject InstantiateWithCollisionCheck(GameObject obstaclePrefab, Vector3 position, Transform parent)
    {
        // Check if there's already an obstacle at the intended position
        Collider2D collider = Physics2D.OverlapBox(position, obstaclePrefab.GetComponent<Renderer>().bounds.size, 0);
        if (collider != null && collider.CompareTag("Obstacle"))
        {
            return null; // Don't instantiate obstacle if there's already one in the way
        }

        // Instantiate the obstacle
        GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity, parent);
        obstacle.GetComponent<Collider2D>().isTrigger = false; // Ensure isTrigger is false
        return obstacle;
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

    private void CheckIfLeftObstaclePlaced(GameObject tile, Vector3 position, ref Bounds tileBounds, ref Vector3 leftPos, ref GameObject obstacle1, ref GameObject leftObstacle)
    {
        while (leftObstacle == null)
        {
            obstacle1 = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
            leftPos = CalculatePosition(tileBounds.min.x + (tileBounds.size.x * 0.25f), tileBounds.center.x, tileBounds.min.y, tileBounds.max.y, position.z);
            leftObstacle = InstantiateWithCollisionCheck(obstacle1, leftPos, tile.transform);
        }
    }

    private void CheckIfRightObstaclePlaced(GameObject tile, Vector3 position, Bounds tileBounds, ref Vector3 rightPos, ref GameObject obstacle2, ref GameObject rightObstacle)
    {
        while (rightObstacle == null)
        {
            obstacle2 = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
            rightPos = CalculatePosition(tileBounds.center.x, tileBounds.max.x - (tileBounds.size.x * 0.25f), tileBounds.min.y, tileBounds.max.y, position.z);
            rightObstacle = InstantiateWithCollisionCheck(obstacle2, rightPos, tile.transform);
        }
    }
}
