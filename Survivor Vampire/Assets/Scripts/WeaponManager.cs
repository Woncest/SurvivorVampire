using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectsContainer;
    [SerializeField] WeaponData startingWeapon;

    List<WeaponBase> weapons;

    private void Awake(){
        weapons = new List<WeaponBase>();
    }

    private void Start(){
        AddWeapon(startingWeapon);
    }

    public void AddWeapon(WeaponData weaponData){
        GameObject weapon = Instantiate(weaponData.weaponBasePrefab, weaponObjectsContainer);

        WeaponBase weaponBase = weapon.GetComponent<WeaponBase>();

        weaponBase.SetData(weaponData);
        weapons.Add(weaponBase);

        Level level = GetComponent<Level>();
        if(level != null){
            level.AddUpgradesIntoList(weaponData.upgrades);
        }
    }

    internal void UpgradeWeapon(UpgradesSO upgrade)
    {
        WeaponBase weaponToUpgrade = weapons.Find(wd => wd.weaponData == upgrade.weaponData);
        weaponToUpgrade.Upgrade(upgrade);
    }
}
