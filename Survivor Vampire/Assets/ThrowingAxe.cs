using System.Collections;
using UnityEngine;

public class ThrowingAxe : WeaponBase
{
    [SerializeField] GameObject projectile;
    public override void Attack()
    {
        StartCoroutine(ThrowAxes());
    }

    IEnumerator ThrowAxes()
    {
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            GameObject thrown = Instantiate(projectile, transform.parent.position, transform.parent.rotation);
            thrown.GetComponent<ThrowingAxeProjectile>().weaponStats = weaponStats;
            yield return new WaitForSeconds(0.15f);
        }
    }
}
