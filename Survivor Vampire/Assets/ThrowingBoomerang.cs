using System.Collections;
using UnityEngine;

public class ThrowingBoomerang : WeaponBase
{
    [SerializeField] GameObject projectile;
    public override void Attack()
    {
        StartCoroutine(ThrowBoomerangs());
    }

    IEnumerator ThrowBoomerangs()
    {
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            GameObject thrown = Instantiate(projectile, transform.parent.position, transform.parent.rotation);
            thrown.GetComponent<BoomerangProjectile>().weaponStats = weaponStats;
            yield return new WaitForSeconds(0.25f);
        }
    }
}
