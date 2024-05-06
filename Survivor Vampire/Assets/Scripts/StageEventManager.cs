using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManager : MonoBehaviour
{
    [SerializeField] StageSO stageSO;
    [SerializeField] EnemiesManager enemiesManager;
    private StageTimer stageTimer;
    int eventIndexer;

    private void Awake(){
        stageTimer = GetComponent<StageTimer>();
    }

    private void Update(){
        if (eventIndexer >= stageSO.events.Count) { return;}
        if(stageTimer.time > stageSO.events[eventIndexer].time){
            Debug.Log(stageSO.events[eventIndexer].message);

            switch(stageSO.events[eventIndexer].eventType){
                case StageEventType.SpawnEnemy:
                    SpawnEnemies();
                    break;
                case StageEventType.SpawnObject:
                    SpawnObjects();
                    break;
                case StageEventType.WinStage:
                    break;
            }

            eventIndexer++;
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < stageSO.events[eventIndexer].count; i++)
        {
            enemiesManager.SpawnEnemy(stageSO.events[eventIndexer].enemyToSpawn);
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
