using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnife : WeaponBase
{
    PlayerMove playerMove;

    [SerializeField] GameObject knifePrefab;

    private void Awake(){
        playerMove = GetComponentInParent<PlayerMove>();
    }

    public override void Attack()
    {
        for (int i = 0; i < weaponStats.numberOfAttacks; i++){
            GameObject thrownKnife = Instantiate(knifePrefab);
            thrownKnife.transform.position = transform.position;
            Vector3 newKnifePosition = transform.position;
            if(weaponStats.numberOfAttacks != 1){
                newKnifePosition.y -= (0.5f * (weaponStats.numberOfAttacks - 1)) / 2;
                newKnifePosition.y += i * 0.5f;
            }
            thrownKnife.transform.position = newKnifePosition;
            ThrowingKnifeProjectile throwingKnifeProjectile = thrownKnife.GetComponent<ThrowingKnifeProjectile>();
            throwingKnifeProjectile.SetDirection(playerMove.lastHorizontalVector, 0f);
            throwingKnifeProjectile.damage = GetDamage();
        }
    }
}
