using System.Collections.Generic;
using UnityEngine;

public class ObstaclePlacer : MonoBehaviour
{
    public List<GameObject> obstaclesToPlace;
    public LayerMask collisionLayer;

    public void PlaceObstacles(Vector3 tilePosition, GameObject tileObject = null)
    {
        foreach (Vector3 offset in GetRandomOffsets(2))
        {
            Vector3 obstaclePosition = tilePosition + offset;
            GameObject obstaclePrefab = obstaclesToPlace[Random.Range(0, obstaclesToPlace.Count)];
            GameObject placedObstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);

            // Check for collisions and reposition the object if needed
            while (Physics.CheckBox(placedObstacle.transform.position, Vector3.one / 2f, Quaternion.identity))
            {
                Collider[] colliders = Physics.OverlapBox(placedObstacle.transform.position, Vector3.one / 2f, Quaternion.identity);
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Obstacle"))
                    {
                        Debug.Log("Had to change position due to overlap");
                        if (tileObject != null)
                        {
                            Bounds tileBounds = tileObject.GetComponent<Renderer>().bounds;
                            float minX = tileBounds.min.x;
                            float maxX = tileBounds.max.x;
                            float minZ = tileBounds.min.z;
                            float maxZ = tileBounds.max.z;

                            Vector3 newPosition = placedObstacle.transform.position;
                            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
                            placedObstacle.transform.position = newPosition;

                            if (newPosition == placedObstacle.transform.position)
                            {
                                break; // If the object is no longer colliding and is within the tile bounds, exit the loop
                            }
                        }
                        else
                        {
                            placedObstacle.transform.position += Vector3.up; // Move the object up by one unit
                        }
                    }
                }
            }
        }
    }

    private IEnumerable<Vector3> GetRandomOffsets(int count)
    {
        List<Vector3> offsets = new List<Vector3>();
        for (int i = 0; i < count; i++)
        {
            float offsetX = Random.Range(0f, 1f);
            float offsetZ = Random.Range(0f, 1f);
            offsets.Add(new Vector3(offsetX, 0f, offsetZ));
        }
        return offsets;
    }
}
