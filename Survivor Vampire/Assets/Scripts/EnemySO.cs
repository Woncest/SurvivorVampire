using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemySO : ScriptableObject
{
    public string Name;
    public GameObject animatedPrefab;
    public EnemyStats stats;
}