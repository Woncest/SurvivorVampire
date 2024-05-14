using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[Serializable]
public class EnemyStats{
    public float hp = 4;
    public float hpMax {get; set;} = 4 ;
    public float damage = 1;
    public int experience_reward = 100;
    public float speed = 1;

    public EnemyStats(EnemyStats stats)
    {
        this.hp = stats.hp;
        this.hpMax = stats.hp;
        this.damage = stats.damage;
        this.experience_reward = stats.experience_reward;
        this.speed = stats.speed;
    }

    internal void ApplyProgress(float progress)
    {
        this.hp *= progress;
        this.hpMax *= progress;
        this.damage *= progress;
    }
}

public class Enemy : MonoBehaviour, IDamageable
{
    Transform targetDestionation;
    GameObject targetGameObject;
    Character targetCharacter;

    Rigidbody2D rgdbd2d;

    public EnemyStats stats;

    public bool isBoss = false;
    private Collider2D bossCollider;

    private void Awake(){
        rgdbd2d = GetComponent<Rigidbody2D>();
    }

    private void Start(){
        bossCollider = GetComponentInChildren<Collider2D>();
    }

    public void SetTarget(GameObject target){
        targetGameObject = target;
        targetDestionation = target.transform;
        targetCharacter = targetGameObject.GetComponent<Character>();
    }

    private void FixedUpdate(){
        Vector3 direction = (targetDestionation.position - transform.position).normalized;
        rgdbd2d.velocity = direction * stats.speed;
    }

    private void OnCollisionStay2D(Collision2D coll){
        if(coll.gameObject == targetGameObject){
            Attack();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isBoss) { return; }
        Collider2D otherCollider = collision.collider;
        if (otherCollider != null && otherCollider != bossCollider)
        {
            Physics2D.IgnoreCollision(otherCollider, bossCollider, true);
        }
    }

    private void Attack(){
        targetCharacter.TakeDamage(stats.damage);
    }

    public void TakeDamage(float damage){
        stats.hp -= damage;
        if(stats.hp < 0){ stats.hp = 0;}
        if(stats.hp <= 0){
            targetGameObject.GetComponent<Level>().AddExperience(stats.experience_reward);
            GetComponent<DropOnDestroy>().CheckDrop();
            Destroy(gameObject);
        }
    }

    internal void SetStats(EnemyStats stats)
    {
        this.stats = new EnemyStats(stats);
    }

    internal void UpdateStatsForProgress(float progress)
    {
        stats.ApplyProgress(progress);
    }
}
