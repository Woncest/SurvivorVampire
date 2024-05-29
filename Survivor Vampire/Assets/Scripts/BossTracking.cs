using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTracking : MonoBehaviour
{
    public GameObject redTrianglePrefab; // Assign the red triangle prefab in the Unity Editor

    private GameObject redTriangle; // Reference to the instantiated red triangle GameObject

    public void TrackBoss(){
        Transform playerTransform = GameManager.instance.playerTransform;
        if (redTriangle == null)
        {
            redTriangle = Instantiate(redTrianglePrefab, playerTransform.position, Quaternion.identity);
        }
        else
        {
            Vector3 direction = (transform.position - playerTransform.position).normalized;

            Vector3 triangleTipPosition = playerTransform.position + direction;

            redTriangle.transform.position = triangleTipPosition;

            redTriangle.transform.up = direction;
        }
    }

    public void StopTrackBoss(){
        if (redTriangle != null){
            Destroy(redTriangle.gameObject);
        }
    }
}
