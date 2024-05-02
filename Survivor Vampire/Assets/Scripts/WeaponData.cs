using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponStats{
    public float damage;
    public float timeToAttack;
    public int numberOfAttacks;

    public WeaponStats(float damage, float timeToAttack, int numberOfAttacks){
        this.damage = damage;
        this.timeToAttack = timeToAttack;
        this.numberOfAttacks = numberOfAttacks;
    }

    internal void Sum(WeaponStats weaponToUpgradeStats)
    {
        this.damage += weaponToUpgradeStats.damage;
        this.timeToAttack += weaponToUpgradeStats.timeToAttack;
        this.numberOfAttacks += weaponToUpgradeStats.numberOfAttacks;

    }
}

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public String weaponName;
    public WeaponStats stats;
    public GameObject weaponBasePrefab;
    public List<UpgradesSO> upgrades;
}
