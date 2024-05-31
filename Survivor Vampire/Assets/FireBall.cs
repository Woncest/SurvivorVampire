using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : WeaponBase
{
    public float radius = 2.5f;
    public float speed = 1.0f;
    private float angle = 0.0f;
    private Collider2D attackTarget;
    private GameObject secondFireBall;
    public bool setSecondFireBall = false;
    public bool isSecondFireBall = false;
    public override void Attack()
    {
        ApplyDamage(new Collider2D[] { attackTarget });
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            attackTarget = other;
            Attack();
        }
    }

    public new virtual void Update()
    {
        CheckForExtraFireBalls();

        // Adjust angle increment based on whether this is the second fireball or not
        if (!isSecondFireBall)
        {
            angle += speed * Time.deltaTime * 1.5f;
        }
        else
        {
            angle -= speed * Time.deltaTime * 1.5f;
        }

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        Vector2 directionFireBall = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

        transform.localPosition = new Vector3(x, y, transform.localPosition.z);

        // Update rotation
        float angleDegrees = Mathf.Atan2(directionFireBall.y, directionFireBall.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleDegrees + (isSecondFireBall ? -90 : 90));
    }

    private void CheckForExtraFireBalls()
    {
        if(weaponStats.numberOfAttacks == 2 && !setSecondFireBall){
            secondFireBall = Instantiate(gameObject);
            secondFireBall.GetComponent<FireBall>().setSecondFireBall = true;
            secondFireBall.GetComponent<FireBall>().isSecondFireBall = true;
            secondFireBall.GetComponent<WeaponBase>().weaponStats = weaponStats;
            secondFireBall.GetComponent<WeaponBase>().character = character;
            setSecondFireBall = true;
            secondFireBall.transform.parent = transform.parent;
        }
    }
}
