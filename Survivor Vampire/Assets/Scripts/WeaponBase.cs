using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData weaponData;

    public WeaponStats weaponStats;

    float timer;

    public void Update(){
        timer -= Time.deltaTime;
        if (timer < 0){
            Attack();
            timer = weaponStats.timeToAttack;
        }
    }

    public virtual void SetData(WeaponData wd){
        weaponData = wd;

        weaponStats = new WeaponStats(wd.stats.damage, wd.stats.timeToAttack);
    }

    public abstract void Attack();

    public virtual void PostDamage(float damage, Vector3 targetPosition){
        MessageSystem.instance.PostMessage(damage.ToString(), targetPosition);
    }

    public void Upgrade(UpgradesSO upgrade)
    {
        weaponStats.Sum(upgrade.weaponUpgradeStats);
    }
}
