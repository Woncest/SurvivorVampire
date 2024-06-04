using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPuddle : MonoBehaviour
{
    public WeaponStats weaponStats {private get; set;}
    private float ttl = 5f;

    private void Update(){
        ttl -= Time.deltaTime;
        if(ttl < 0){
            Destroy(gameObject);
        }
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
