using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingAxeProjectile : MonoBehaviour
{
    [SerializeField] float minForce = 6f;
    [SerializeField] float maxForce = 8f;
    [SerializeField] float angleRange = 30f;
    [SerializeField] float gravity = 9.81f;
    public WeaponStats weaponStats {private get; set;}

    private Vector2 velocity;
    private float elapsedTime;

    void Start()
    {
        float force = Random.Range(minForce, maxForce);
        float angle = Random.Range(-angleRange / 2, angleRange / 2);
        Vector2 forceDirection = Quaternion.Euler(0, 0, angle) * Vector2.up;

        velocity = forceDirection * force;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        Vector2 displacement = velocity * elapsedTime + 0.5f * Vector2.down * gravity * elapsedTime * elapsedTime;
        transform.position += new Vector3(displacement.x, displacement.y, 0) * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable e = other.GetComponentInParent<IDamageable>();
            if(e != null){
                MessageSystem.instance.PostMessage(weaponStats.damage.ToString(), other.transform.position);
                e.TakeDamage(weaponStats.damage);
            }
        }
    }
}
