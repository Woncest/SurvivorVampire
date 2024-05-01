using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour, IDamageable
{
    public void TakeDamage(float damage){
        Destroy(gameObject);
        GetComponent<DropOnDestroy>().CheckDrop();
    }
}
