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
    bool isDead = false;

    private void Awake(){
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }


    public void TakeDamage(float damage){
        if(isDead) return;
        ApplyArmor(ref damage);
        currentHP -= damage;
        if(currentHP <= 0){
            GetComponent<CharacterGameOver>().GameOver();
            isDead = true;
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
