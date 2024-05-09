using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] StageProgress stageProgress;
    [SerializeField] GameObject enemy;
    [SerializeField] EnemySO enemyAnimation;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnTimer;
    GameObject target;
    float timer;

    List<Enemy> bossEnemiesList;
    float totalBossHealth;
    float currentBossHealth;
    [SerializeField] Slider bossHealthBar;

    private void Start(){
        target = GameManager.instance.playerTransform.gameObject;
        bossHealthBar = FindObjectOfType<BossHpBar>(true).GetComponent<Slider>();
    }

    private void Update(){
        timer -= Time.deltaTime;
        if (timer < 0){
            SpawnEnemy(enemyAnimation, false);
        }
        UpdateBossHealth();
    }

    private void UpdateBossHealth()
    {
        if (bossEnemiesList == null || bossEnemiesList.Count == 0){
            bossHealthBar.gameObject.SetActive(false);
            return;
        }
        bossHealthBar.gameObject.SetActive(true);

        currentBossHealth = 0;

        foreach (Enemy boss in bossEnemiesList){
            currentBossHealth += boss.stats.hp;
        }

        bossHealthBar.value = currentBossHealth;

        if (currentBossHealth <= 0){
            bossHealthBar.gameObject.SetActive(false);
        }
    }

    public void SpawnEnemy(EnemySO enemyToSpawn, bool isBoss)
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

        if(isBoss){
            SpawnEnemyBoss(newEnemyComponent);
        }

        //spawning sprite object of enemy
        GameObject spriteObject = Instantiate(enemyToSpawn.animatedPrefab);
        spriteObject.transform.parent = newEnemy.transform;
        spriteObject.transform.localPosition = Vector3.zero;
    }

    private void SpawnEnemyBoss(Enemy newBoss)
    {
        if (bossEnemiesList == null) { bossEnemiesList = new List<Enemy>();}

        bossEnemiesList.Add(newBoss);

        totalBossHealth += newBoss.stats.hp;

        bossHealthBar.gameObject.SetActive(true);
        bossHealthBar.maxValue = totalBossHealth;
    }
}
