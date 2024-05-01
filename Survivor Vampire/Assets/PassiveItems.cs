using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItems : MonoBehaviour
{
    [SerializeField] List<Item> items;

    Character character;

    [SerializeField] Item testArmor;

    private void Start(){
        Equip(testArmor);
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

    }
}
