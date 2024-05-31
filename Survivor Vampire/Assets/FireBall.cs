using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : WeaponBase
{
    public float radius = 2.5f;
    public float speed = 1.0f;
    private float angle = 0.0f;
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        
    }

    public new virtual void Update()
    {
        angle += speed * Time.deltaTime;

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

        transform.localPosition = new Vector3(x, y, transform.localPosition.z);

        float angleDegrees = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleDegrees + 90);
    }
}
