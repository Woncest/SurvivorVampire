using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectsContainer;
    [SerializeField] WeaponData startingWeapon;
    [SerializeField] ChargeBars chargeBars;

    List<WeaponBase> weapons;

    Character character;

    private void Awake(){
        weapons = new List<WeaponBase>();
        character = GetComponent<Character>();
    }

    private void Start(){
        AddWeapon(startingWeapon);
    }

    public void AddWeapon(WeaponData weaponData){
        GameObject weapon = Instantiate(weaponData.weaponBasePrefab, weaponObjectsContainer);

        WeaponBase weaponBase = weapon.GetComponent<WeaponBase>();

        weaponBase.SetData(weaponData);
        weapons.Add(weaponBase);
        weaponBase.AddCharacter(character);
        if(weaponData.needsChargeBar){
            GameObject chargeBar = chargeBars.ActivateChargeBar(weaponData.sprite);
            if(chargeBar != null){
                weaponBase.chargeBar = chargeBar.GetComponent<StatusBar>();
            }
        }

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
