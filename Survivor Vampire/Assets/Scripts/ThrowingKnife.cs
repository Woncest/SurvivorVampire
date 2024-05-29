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
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            Vector3 direction = (mouseWorldPosition - throwingKnifeProjectile.transform.position).normalized;
            throwingKnifeProjectile.SetDirection(direction.x, direction.y);
            RotateProjectile(throwingKnifeProjectile.gameObject, direction);
            throwingKnifeProjectile.damage = GetDamage();
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.nearClipPlane; // Set this to the distance of your camera to the object if necessary
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    private void RotateProjectile(GameObject projectile, Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}
