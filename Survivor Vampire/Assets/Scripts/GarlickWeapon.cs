using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlickWeapon : WeaponBase
{
    [SerializeField] float attackAreaSize = 3f;
    [SerializeField] GameObject radiusCircle;

    private SpriteRenderer radiusCircleRenderer;
    private float initialAlpha = 0.15f; // 15% alpha
    private float maxAlpha = 0.4f; // 40% alpha

    void Start()
    {
        radiusCircleRenderer = radiusCircle.GetComponent<SpriteRenderer>();
        Color color = radiusCircleRenderer.color;
        color.a = initialAlpha;
        radiusCircleRenderer.color = color;
    }

    public override void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackAreaSize);
        for(int i = 0; i < weaponStats.numberOfAttacks; i++){
            ApplyDamage(colliders);
        }
    }

    public new virtual void Update(){
        timer -= Time.deltaTime;
        float alpha = Mathf.Lerp(maxAlpha, initialAlpha, timer / weaponStats.timeToAttack);
        
        Color color = radiusCircleRenderer.color;
        color.a = alpha;
        radiusCircleRenderer.color = color;
        if (timer < 0){
            Attack();
            timer = weaponStats.timeToAttack;

            color.a = initialAlpha;
            radiusCircleRenderer.color = color;
        }
        transform.position = GameManager.instance.playerTransform.position;
    }
}
