using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageEventType{
    SpawnEnemy,
    SpawnEnemyBoss,
    SpawnObject,
    WinStage
}

[Serializable]
public class StageEvent{
    public StageEventType eventType;

    public float time;
    public String message;

    public EnemySO enemyToSpawn;
    public GameObject objectToSpawn;
    public int count;
}

[CreateAssetMenu]
public class StageSO : ScriptableObject
{
    public List<StageEvent> events;
}
