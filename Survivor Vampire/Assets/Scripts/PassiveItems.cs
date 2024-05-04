using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItems : MonoBehaviour
{
    List<Item> items;

    Character character;

    private void Start(){
    }

    private void Awake(){
        character = GetComponent<Character>();
    }

    public void Equip(Item item){
        if(items == null){
            items = new List<Item>();
        }
        items.Add(item);
        item.Equip(character);
    }

    public void UnEquip(Item item){
        if(items == null){
            items = new List<Item>();
        }
        items.Remove(item);
        item.UnEquip(character);
    }
}
