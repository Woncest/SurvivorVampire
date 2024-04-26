using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float maxHP = 1000;
    public float currentHP = 1000;
    [SerializeField] StatusBar hpBar;
    public void TakeDamage(float damage){
        currentHP -= damage;
        if(currentHP <= 0){
            //TODO implement Game Over
            Destroy(gameObject);
        }
        hpBar.SetState(currentHP, maxHP);
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
