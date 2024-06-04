using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidThrowing : WeaponBase
{
    [SerializeField] GameObject projectile;
    public override void Attack()
    {
        StartCoroutine(ThrowAcid());
    }

    IEnumerator ThrowAcid()
    {
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            GameObject thrown = Instantiate(projectile, transform.parent.position, transform.parent.rotation);
            thrown.GetComponent<AcidBottle>().weaponStats = weaponStats;
            yield return new WaitForSeconds(0.15f);
        }
    }
}
