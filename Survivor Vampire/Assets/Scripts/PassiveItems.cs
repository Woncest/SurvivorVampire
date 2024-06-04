using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItems : MonoBehaviour
{
    public List<Item> items;

    Character character;

    private void Start(){
        foreach(var item in items){
            item.stats.armor = 0;
            item.stats.damageBonus = 0;
        }
    }

    private void Awake(){
        character = GetComponent<Character>();
    }

    public void Equip(Item item){
        if(items == null){
            items = new List<Item>();
        }
        Item newItemInstance = new Item();
        newItemInstance.Init(item.Name);
        newItemInstance.stats.Sum(item.stats);

        items.Add(newItemInstance);
        newItemInstance.Equip(character);

        Level level = GetComponent<Level>();
        if(level != null){
            level.AddUpgradesIntoList(item.upgrades);
        }
    }

    public void UnEquip(Item item){
        if(items == null){
            items = new List<Item>();
        }
        items.Remove(item);
        item.UnEquip(character);
    }

    internal void UpgradeItem(UpgradesSO upgrade)
    {
        Item itemToUpgrade = items.Find(id => id.Name == upgrade.item.name);
        itemToUpgrade.UnEquip(character);
        itemToUpgrade.stats.Sum(upgrade.itemStats);
        itemToUpgrade.Equip(character);
    }
}
