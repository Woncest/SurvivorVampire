using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if(spriteRenderer.isVisible && enemiesManager.repeatedSpawnGroupList.First() != null){
            for (int i = 0; i < 250; i++){
                enemiesManager.SpawnEnemyAtPoint(enemiesManager.repeatedSpawnGroupList.First().enemySO, transform.position);
            }
            Destroy(gameObject);
        }
    }
}
