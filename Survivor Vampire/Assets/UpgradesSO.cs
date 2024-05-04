using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType{
    WeaponUpgrade,
    ItemUpgrade,
    WeaponUnlock,
    ItemUnlock
}

[CreateAssetMenu]
public class UpgradesSO : ScriptableObject
{
    public UpgradeType upgradeType;
    public String upgradeName;
    public Sprite icon;

    public WeaponData weaponData;
    public WeaponStats weaponUpgradeStats;

    public Item item;

}
