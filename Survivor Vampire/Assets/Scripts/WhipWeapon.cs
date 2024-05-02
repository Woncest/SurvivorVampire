using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWeapon : WeaponBase
{
    [SerializeField] GameObject leftWhipObject;
    [SerializeField] GameObject rightWhipObject;

    PlayerMove playerMove;
    [SerializeField] Vector2 attackSize = new Vector2(4f, 2f);

    private void Awake(){
        playerMove = GetComponentInParent<PlayerMove>();
    }

    private void ApplyDamage(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders){
            IDamageable i = collider.gameObject.GetComponent<IDamageable>();
            if(i != null){
                PostDamage(weaponStats.damage, collider.transform.position);
                i.TakeDamage(weaponStats.damage);
            }
        }
    }

    public override void Attack()
    {
        if(playerMove.lastHorizontalVector > 0){
            rightWhipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(rightWhipObject.transform.position, attackSize, 0f);
            ApplyDamage(colliders);
        }else{
            leftWhipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(leftWhipObject.transform.position, attackSize, 0f);
            ApplyDamage(colliders);
        }
    }
}