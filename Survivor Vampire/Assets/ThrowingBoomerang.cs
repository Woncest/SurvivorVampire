using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingBoomerang : WeaponBase
{
    [SerializeField] GameObject projectile;
    public override void Attack()
    {
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            GameObject thrown = Instantiate(projectile, transform.parent.position, transform.parent.rotation);
            thrown.GetComponent<BoomerangProjectile>().weaponStats = weaponStats;
        }
    }
}
