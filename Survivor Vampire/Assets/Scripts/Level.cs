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
    PassiveItems passiveItems;

    [SerializeField] List<UpgradesSO> startUpgrades;

    private void Awake(){
        weaponManager = GetComponent<WeaponManager>();
        passiveItems = GetComponent<PassiveItems>();
    }

    int TO_LEVEL_UP{
        get{
            return level * 1000;
        }
    }

    private void Start(){
        expBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
        expBar.SetLevelText(level);
        AddUpgradesIntoList(startUpgrades);
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
            weaponManager.UpgradeWeapon(upgrade);
                break;
            case UpgradeType.ItemUpgrade:
                break;
            case UpgradeType.WeaponUnlock:
                weaponManager.AddWeapon(upgrade.weaponData);
                break;
            case UpgradeType.ItemUnlock:
                passiveItems.Equip(upgrade.item);
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

        HashSet<int> chosenIndices = new HashSet<int>(); // Keep track of chosen indices

        for (int i = 0; i < count; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, upgrades.Count); // Get a random index
            } while (chosenIndices.Contains(randomIndex)); // Keep generating until it's unique

            chosenIndices.Add(randomIndex); // Add the index to the chosen set
            upgradeList.Add(upgrades[randomIndex]); // Add the corresponding upgrade to the list
        }

        return upgradeList;
    }

    internal void AddUpgradesIntoList(List<UpgradesSO> upgradesToAdd)
    {
        if(upgradesToAdd.Count == 0) {return;}

        upgrades.AddRange(upgradesToAdd);
    }
}
