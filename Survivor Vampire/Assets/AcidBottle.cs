using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBottle : MonoBehaviour
{
    [SerializeField] float minForce = 6f;
    [SerializeField] float maxForce = 8f;
    [SerializeField] float angleRange = 30f;
    [SerializeField] float gravity = 9.81f;
    public WeaponStats weaponStats {private get; set;}

    private Vector2 velocity;
    private float elapsedTime;
    [SerializeField] GameObject puddle;

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
            GameObject thrown = Instantiate(puddle, transform.position, transform.rotation);
            thrown.GetComponent<AcidPuddle>().weaponStats = weaponStats;
            Destroy(gameObject);
        }
    }
}
