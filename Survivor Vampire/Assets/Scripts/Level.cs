using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    int level = 1;
    int experience = 0;
    [SerializeField] ExpBar expBar;
    [SerializeField] UpgradePanelManager upgradePanelManager;

    [SerializeField] List<UpgradesSO> upgrades;
    List<UpgradesSO> selectedUpgrades;

    List<UpgradesSO> acquiredUpgrades;

    WeaponManager weaponManager;

    private void Awake(){
        weaponManager = GetComponent<WeaponManager>();
    }

    int TO_LEVEL_UP{
        get{
            return level * 1000;
        }
    }

    private void Start(){
        expBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
        expBar.SetLevelText(level);
    }

    public void AddExperience(int amount){
        experience += amount;
        CheckLevelUp();
        expBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
    }

    public void CheckLevelUp(){
        if(experience >= TO_LEVEL_UP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if(selectedUpgrades == null) { selectedUpgrades = new List<UpgradesSO>(); }
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(GetUpgrades(4));

        upgradePanelManager.OpenPanel(selectedUpgrades);
        experience -= TO_LEVEL_UP;
        level += 1;
        expBar.SetLevelText(level);
    }

    public void Upgrade(int selectedUpgradeID){
        UpgradesSO upgrade = selectedUpgrades[selectedUpgradeID - 1];

        if(acquiredUpgrades == null) { acquiredUpgrades = new List<UpgradesSO>(); }

        switch(upgrade.upgradeType){
            case UpgradeType.WeaponUpgrade:
                break;
            case UpgradeType.ItemUpgrade:
                break;
            case UpgradeType.WeaponUnlock:
                weaponManager.AddWeapon(upgrade.weaponData);
                break;
            case UpgradeType.ItemUnlock:
                break;
        }

        acquiredUpgrades.Add(upgrade);
        upgrades.Remove(upgrade);
    }

    public List<UpgradesSO> GetUpgrades(int count){
        List<UpgradesSO> upgradeList = new List<UpgradesSO>();

        if(count > upgrades.Count){
            count = upgrades.Count;
        }

        for(int i = 0; i < count; i++){
            upgradeList.Add(upgrades[Random.Range(0, upgrades.Count)]);
        }

        return upgradeList;
    }

    internal void AddUpgradesIntoList(List<UpgradesSO> upgradesToAdd)
    {
        upgrades.AddRange(upgradesToAdd);
    }
}