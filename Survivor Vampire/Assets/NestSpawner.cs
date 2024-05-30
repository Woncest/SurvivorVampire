using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestSpawner : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    EnemiesManager enemiesManager;
    [SerializeField] EnemySO enemy;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemiesManager = FindObjectOfType<EnemiesManager>();
    }

    void Update()
    {
        if(spriteRenderer.isVisible){
            for (int i = 0; i < 250; i++){
                enemiesManager.SpawnEnemyAtPoint(enemy, transform.position);
            }
            Destroy(gameObject);
        }
    }
}
