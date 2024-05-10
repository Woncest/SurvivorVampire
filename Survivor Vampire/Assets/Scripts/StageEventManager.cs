using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManager : MonoBehaviour
{
    [SerializeField] StageSO stageSO;
    [SerializeField] EnemiesManager enemiesManager;
    private StageTimer stageTimer;
    int eventIndexer;
    PlayerWinManager playerWin;

    private void Awake(){
        stageTimer = GetComponent<StageTimer>();
    }

    private void Start(){
        playerWin = FindObjectOfType<PlayerWinManager>();
    }

    private void Update(){
        if (eventIndexer >= stageSO.events.Count) { return;}
        if(stageTimer.time > stageSO.events[eventIndexer].time){
            Debug.Log(stageSO.events[eventIndexer].message);

            switch(stageSO.events[eventIndexer].eventType){
                case StageEventType.SpawnEnemy:
                    SpawnEnemies(false);
                    break;
                case StageEventType.SpawnEnemyBoss:
                    SpawnEnemies(true);
                    break;
                case StageEventType.SpawnObject:
                    SpawnObjects();
                    break;
                case StageEventType.WinStage:
                    WinStage();
                    break;
            }

            eventIndexer++;
        }
    }

    private void WinStage()
    {
        playerWin.Win();
    }

    private void SpawnEnemies(bool boss)
    {
        StageEvent currentEvent = stageSO.events[eventIndexer];
        enemiesManager.AddGroupToSpawn(currentEvent.enemyToSpawn, currentEvent.count, boss);

        if(currentEvent.isRepeatedEvent){
            enemiesManager.AddRepeatedSpawn(currentEvent, false);
        }
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < stageSO.events[eventIndexer].count; i++)
        {
            Vector3 positionToSpawn = GameManager.instance.playerTransform.position;
            positionToSpawn += UtilityTools.GenerateRandomPosition(new Vector2(5f, 5f));
            SpawnManager.instance.SpawnObject(positionToSpawn, stageSO.events[eventIndexer].objectToSpawn);
        }
    }
}
