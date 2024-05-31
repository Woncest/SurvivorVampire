using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData weaponData;

    public WeaponStats weaponStats;

    public float timer {get; set;}
    private float timerChargeBar;
    public StatusBar chargeBar;

    public Character character;

    public void Update(){
        timer -= Time.deltaTime;
        timerChargeBar += Time.deltaTime;
        chargeBar.SetState(timerChargeBar,weaponStats.timeToAttack);
        if (timer < 0){
            Attack();
            timer = weaponStats.timeToAttack;
            timerChargeBar = 0;
            chargeBar.SetState(timerChargeBar,weaponStats.timeToAttack);
        }
    }

    public virtual void SetData(WeaponData wd){
        weaponData = wd;

        weaponStats = new WeaponStats(wd.stats.damage, wd.stats.timeToAttack, wd.stats.numberOfAttacks);
    }

    public abstract void Attack();

    public void ApplyDamage(Collider2D[] colliders)
    {
        float damage = GetDamage();
        foreach (Collider2D collider in colliders){
            IDamageable i = collider.gameObject.GetComponentInParent<IDamageable>();
            if(i != null){
                PostDamage(damage, collider.transform.position);
                i.TakeDamage(damage);
            }
        }
    }

    public float GetDamage(){
        float damage = weaponStats.damage * character.damageBonus;
        return damage;
    }

    public virtual void PostDamage(float damage, Vector3 targetPosition){
        MessageSystem.instance.PostMessage(damage.ToString(), targetPosition);
    }

    public void Upgrade(UpgradesSO upgrade)
    {
        weaponStats.Sum(upgrade.weaponUpgradeStats);
        timer = weaponStats.timeToAttack;
        timerChargeBar = 0;
        if(chargeBar != null) chargeBar.SetState(timerChargeBar,weaponStats.timeToAttack);
    }

    public void AddCharacter(Character character)
    {
        this.character = character;
    }
}
