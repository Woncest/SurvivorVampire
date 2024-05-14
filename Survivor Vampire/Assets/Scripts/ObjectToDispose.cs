using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToDispose : MonoBehaviour
{
    Transform playerTransform;
    float maxDistance = 25f;
    private Enemy enemy;

    private void Start(){
        playerTransform = GameManager.instance.playerTransform;
        enemy = GetComponent<Enemy>();
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance > maxDistance && enemy == null){
            Destroy(gameObject);
            return;
        }
        if(distance > maxDistance && enemy != null && !enemy.isBoss){
            Destroy(gameObject);
        }
    }
}
