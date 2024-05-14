using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesSpawnGroup{
    public EnemySO enemySO;
    public int count;
    public bool isBoss;

    public float repeatTimer;
    public float timeBetweenSpawn;

    public EnemiesSpawnGroup(EnemySO enemySO, int count, bool isBoss){
        this.enemySO = enemySO;
        this.count = count;
        this.isBoss = isBoss;
    }

    public void SetRepeatSpawn(float timeBetweenSpawn){
        this.timeBetweenSpawn = timeBetweenSpawn;
        repeatTimer = timeBetweenSpawn;
    }

}

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] StageProgress stageProgress;
    [SerializeField] GameObject enemy;
    [SerializeField] Vector2 spawnArea;
    GameObject target;

    List<Enemy> bossEnemiesList;
    float totalBossHealth;
    float currentBossHealth;
    [SerializeField] Slider bossHealthBar;

    List<EnemiesSpawnGroup> enemiesSpawnGroupList;
    List<EnemiesSpawnGroup> repeatedSpawnGroupList;

    private void Start(){
        target = GameManager.instance.playerTransform.gameObject;
        bossHealthBar = FindObjectOfType<BossHpBar>(true).GetComponent<Slider>();
    }

    private void Update(){
        ProcessSpawn();
        ProcessRepeatedSpawnGroups();
        UpdateBossHealth();
    }

    private void UpdateBossHealth()
    {
        if (bossEnemiesList == null || bossEnemiesList.Count == 0){
            bossHealthBar.gameObject.SetActive(false);
            return;
        }
        RemoveDeadEnemies();
        Debug.Log(bossEnemiesList.Count);
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

    public void AddGroupToSpawn(EnemySO enemyToSpawn, int count, bool isBoss){
        EnemiesSpawnGroup newGroupToSpawn = new EnemiesSpawnGroup(enemyToSpawn, count, isBoss);

        if (enemiesSpawnGroupList == null) { enemiesSpawnGroupList = new List<EnemiesSpawnGroup>(); }

        enemiesSpawnGroupList.Add(newGroupToSpawn);
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

        if(isBoss){
            newEnemyComponent.isBoss = isBoss;
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

        totalBossHealth += newBoss.stats.hpMax;

        bossHealthBar.gameObject.SetActive(true);
        bossHealthBar.maxValue = totalBossHealth;
    }

    private void ProcessSpawn()
    {
        if (enemiesSpawnGroupList == null) { return; }

        if (enemiesSpawnGroupList.Count > 0){
            SpawnEnemy(enemiesSpawnGroupList[0].enemySO, enemiesSpawnGroupList[0].isBoss);
            enemiesSpawnGroupList[0].count--;

            if(enemiesSpawnGroupList[0].count <= 0){
                enemiesSpawnGroupList.RemoveAt(0);
            }
        }
    }

    private void ProcessRepeatedSpawnGroups()
    {
        if (repeatedSpawnGroupList == null) { return; }

        for (int i = 0; i < repeatedSpawnGroupList.Count; i++){
            repeatedSpawnGroupList[i].repeatTimer -= Time.deltaTime;
            if(repeatedSpawnGroupList[i].repeatTimer < 0){
                repeatedSpawnGroupList[i].repeatTimer = repeatedSpawnGroupList[i].timeBetweenSpawn;
                AddGroupToSpawn(repeatedSpawnGroupList[i].enemySO, repeatedSpawnGroupList[i].count, repeatedSpawnGroupList[i].isBoss);
            }
        }
    }

    public void AddRepeatedSpawn(StageEvent stageEvent, bool isBoss)
    {
        EnemiesSpawnGroup repeatSpawnGroup = new EnemiesSpawnGroup(stageEvent.enemyToSpawn, stageEvent.count, isBoss);
        repeatSpawnGroup.SetRepeatSpawn(stageEvent.repeatedEverySeconds);

        if(repeatedSpawnGroupList == null){
            repeatedSpawnGroupList = new List<EnemiesSpawnGroup>();
        }

        if(repeatedSpawnGroupList.Count >= 2){
            repeatedSpawnGroupList.RemoveAt(0);
        }
        repeatedSpawnGroupList.Add(repeatSpawnGroup);
    }

    private void RemoveDeadEnemies()
    {
        List<int> indicesToRemove = new List<int>();

        for (int i = 0; i < bossEnemiesList.Count; i++)
        {
            if (bossEnemiesList[i].stats.hp == 0)
            {
                indicesToRemove.Add(i);
            }
        }

        for (int i = indicesToRemove.Count - 1; i >= 0; i--)
        {
            int index = indicesToRemove[i];
            totalBossHealth -= bossEnemiesList[i].stats.hpMax;
            bossEnemiesList.RemoveAt(index);
        }
    }
}
