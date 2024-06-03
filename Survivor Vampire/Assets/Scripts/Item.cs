using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemStats{
    public int armor;
    public float damageBonus;
    public float hpRegen;
    public float xpBonus;

    internal void Sum(ItemStats stats)
    {
        armor += stats.armor;
        damageBonus += stats.damageBonus;
        hpRegen += stats.hpRegen;
        xpBonus += stats.xpBonus;
        Debug.Log(xpBonus);
    }
}

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string Name;
    public ItemStats stats;
    public List<UpgradesSO> upgrades;

    public void Init(String Name){
        this.Name = Name;
        stats = new ItemStats();
        upgrades = new List<UpgradesSO>();
    }

    public void Equip(Character character){
        character.armor += stats.armor;
        character.damageBonus += stats.damageBonus;
        character.hpRegen += stats.hpRegen;
        character.xpBonus += stats.xpBonus;
    }

    public void UnEquip(Character character){
        character.armor -= stats.armor;
        character.damageBonus -= stats.damageBonus;
        character.hpRegen -= stats.hpRegen;
        character.xpBonus -= stats.xpBonus;
    }
}
