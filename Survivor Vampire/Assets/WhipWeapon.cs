using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWeapon : MonoBehaviour
{
    [SerializeField] float timeToAttack = 4f;
    float timer;

    [SerializeField] GameObject leftWhipObject;
    [SerializeField] GameObject rightWhipObject;

    PlayerMove playerMove;
    [SerializeField] Vector2 whipAttackSize = new Vector2(4f, 2f);
    [SerializeField] float whipDamage = 1f;

    private void Awake(){
        playerMove = GetComponentInParent<PlayerMove>();
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0){
            Attack();
        }
    }

    private void Attack()
    {
        timer = timeToAttack;

        if(playerMove.lastHorizontalVector > 0){
            rightWhipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(rightWhipObject.transform.position, whipAttackSize, 0f);
            ApplyDamage(colliders);
        }else{
            leftWhipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(leftWhipObject.transform.position, whipAttackSize, 0f);
            ApplyDamage(colliders);
        }
    }

    private void ApplyDamage(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders){
            Enemy e = collider.gameObject.GetComponent<Enemy>();
            if(e != null){
                e.TakeDamage(whipDamage);
            }
        }
    }
}
