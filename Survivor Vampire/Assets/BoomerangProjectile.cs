using System.Collections;
using UnityEngine;

public class BoomerangProjectile : MonoBehaviour
{
    public WeaponStats weaponStats {private get; set;}
    private Transform playerTransform;
    private Vector3 targetPosition;
    private float speed = 5f;
    private bool returning = false;
    private float returnTimer = 5f;

    void Start()
    {
        playerTransform = GameManager.instance.playerTransform;
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;
        StartCoroutine(ReturnAfterDelay());
    }

    void Update()
    {
        if (!returning)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (transform.position == targetPosition)
                returning = true;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            if (transform.position == playerTransform.position)
                Destroy(gameObject);
        }
    }

    IEnumerator ReturnAfterDelay()
    {
        yield return new WaitForSeconds(returnTimer);
        returning = true;
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
