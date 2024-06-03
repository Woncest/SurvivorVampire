using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float maxHP = 1000;
    public float currentHP = 1000;
    public float armor = 0;
    public float xpBonus = 1;

    public float hpRegenerationRate = 1f;
    public float hpRegen = 1f;
    private float hpRegenTimer;

    public float damageBonus;

    [SerializeField] StatusBar hpBar;

    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;
    bool isDead = false;

    [SerializeField] DataContainerSO dataContainer;

    private void Awake(){
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }

    private void Start(){
        ApplyPersistantUpgrades();

        hpBar.SetState(currentHP, maxHP);
    }

    private void Update(){
        hpRegenTimer += Time.deltaTime * hpRegenerationRate;

        if(hpRegenTimer > 1f){
            Heal(hpRegen);
            hpRegenTimer -= 1f;
        }
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

    private void ApplyPersistantUpgrades()
    {
        int hpLevel = dataContainer.GetUpgradeLevel(PersistentUpgrades.HP);

        maxHP += maxHP / 10 * hpLevel;
        currentHP = maxHP;

        int damageLevel = dataContainer.GetUpgradeLevel(PersistentUpgrades.Damage);

        damageBonus = 1f + 0.1f * damageLevel;
    }
}
