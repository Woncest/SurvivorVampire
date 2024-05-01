using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float maxHP = 1000;
    public float currentHP = 1000;
    public float armor = 0;
    [SerializeField] StatusBar hpBar;

    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;

    private void Awake(){
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }


    public void TakeDamage(float damage){
        ApplyArmor(ref damage);
        currentHP -= damage;
        if(currentHP <= 0){
            //TODO implement Game Over
            Destroy(gameObject);
        }
        hpBar.SetState(currentHP, maxHP);
    }

    private void ApplyArmor(ref float damage)
    {
        damage -= armor;
        if(damage < 0){ 
            damage = 0;
        }
    }

    public void Heal(float amount){
        if(currentHP <= 0){ return; }
        currentHP += amount;
        if(currentHP > maxHP){
            currentHP = maxHP;
        }
        hpBar.SetState(currentHP, maxHP);
    }
}
