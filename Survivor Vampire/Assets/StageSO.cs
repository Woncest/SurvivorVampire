using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageEvent{
    public float time;
    public String message;
    public EnemySO enemyToSpawn;
    public int count;
}

[CreateAssetMenu]
public class StageSO : ScriptableObject
{
    public List<StageEvent> events;
}
