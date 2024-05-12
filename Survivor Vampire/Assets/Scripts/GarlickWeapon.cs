using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlickWeapon : WeaponBase
{
    [SerializeField] float attackAreaSize = 3f;

    public override void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackAreaSize);
        for(int i = 0; i < weaponStats.numberOfAttacks; i++){
            ApplyDamage(colliders);
        }
    }

    public new virtual void Update(){
        timer -= Time.deltaTime;
        if (timer < 0){
            Attack();
            timer = weaponStats.timeToAttack;
        }
        transform.position = GameManager.instance.playerTransform.position;
    }
}