using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectsContainer;
    [SerializeField] WeaponData startingWeapon;

    private void Start(){
        AddWeapon(startingWeapon);
    }

    public void AddWeapon(WeaponData weaponData){
        GameObject weapon = Instantiate(weaponData.weaponBasePrefab, weaponObjectsContainer);

        weapon.GetComponent<WeaponBase>().SetData(weaponData);
    }
}
