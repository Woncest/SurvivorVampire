using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManager : MonoBehaviour
{
    [SerializeField] StageSO stageSO;
    [SerializeField] EnemiesManager enemiesManager;
    public float timer {get; private set;}
    int eventIndexer;

    private void Update(){
        if (eventIndexer >= stageSO.events.Count) { return;}
        timer += Time.deltaTime;
        if(timer > stageSO.events[eventIndexer].time){
            Debug.Log(stageSO.events[eventIndexer].message);

            for(int i = 0; i < stageSO.events[eventIndexer].count; i++){
                enemiesManager.SpawnEnemy(stageSO.events[eventIndexer].enemyToSpawn);
            }

            eventIndexer++;
        }
    }
}
