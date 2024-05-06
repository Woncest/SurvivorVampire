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
        stageTimer = new StageTimer();
    }

    private void Update(){
        if (eventIndexer >= stageSO.events.Count) { return;}
        if(stageTimer.time > stageSO.events[eventIndexer].time){
            Debug.Log(stageSO.events[eventIndexer].message);

            for(int i = 0; i < stageSO.events[eventIndexer].count; i++){
                enemiesManager.SpawnEnemy(stageSO.events[eventIndexer].enemyToSpawn);
            }

            eventIndexer++;
        }
    }
}
