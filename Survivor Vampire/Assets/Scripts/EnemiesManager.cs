using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] StageProgress stageProgress;
    [SerializeField] GameObject enemy;
    [SerializeField] EnemySO enemyAnimation;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnTimer;
    GameObject target;
    float timer;

    private void Start(){
        target = GameManager.instance.playerTransform.gameObject;
    }

    private void Update(){
        timer -= Time.deltaTime;
        if (timer < 0){
            SpawnEnemy(enemyAnimation);
        }
    }

    public void SpawnEnemy(EnemySO enemyToSpawn)
    {
        Vector3 positon = UtilityTools.GenerateRandomPosition(spawnArea);

        positon += target.transform.position;

        //spawning main object of enemy
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = positon;

        Enemy newEnemyComponent = newEnemy.GetComponent<Enemy>();
        newEnemyComponent.SetTarget(target);
        newEnemyComponent.SetStats(enemyToSpawn.stats);
        newEnemyComponent.UpdateStatsForProgress(stageProgress.Progress);
        newEnemy.transform.parent = transform;
        timer = spawnTimer;

        //spawning sprite object of enemy
        GameObject spriteObject = Instantiate(enemyToSpawn.animatedPrefab);
        spriteObject.transform.parent = newEnemy.transform;
        spriteObject.transform.localPosition = Vector3.zero;
    }
}
