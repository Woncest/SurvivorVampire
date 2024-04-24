using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform targetDestionation;
    GameObject targetGameObject;
    Character targetCharacter;
    [SerializeField] float speed;

    Rigidbody2D rgdbd2d;

    [SerializeField] float hp = 4;
    [SerializeField] float damage = 1;


    private void Awake(){
        rgdbd2d = GetComponent<Rigidbody2D>();
        targetGameObject = targetDestionation.gameObject;
        targetCharacter = targetGameObject.GetComponent<Character>();
    }

    private void FixedUpdate(){
        Vector3 direction = (targetDestionation.position - transform.position).normalized;
        rgdbd2d.velocity = direction * speed;
    }

    private void OnCollisionStay2D(Collision2D coll){
        if(coll.gameObject == targetGameObject){
            Attack();
        }
    }

    private void Attack(){
        targetCharacter.TakeDamage(damage);
    }

    public void TakeDamage(float damage){
        hp -= damage;
        if(hp <= 0){
            Destroy(gameObject);
        }
    }
}
