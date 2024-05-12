using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUpgradeUIElement : MonoBehaviour
{
    [SerializeField] PersistentUpgrades upgrade;

    [SerializeField] TextMeshProUGUI upgradeName;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI price;

    [SerializeField] DataContainerSO dataContainer;

    private void Start(){
        UpdateElement();
    }

    public void Upgrade(){
        PlayerUpgrade playerUpgrade = dataContainer.upgrades[(int) upgrade];

        if (playerUpgrade.level >= playerUpgrade.maxLevel) { return; }

        if(dataContainer.coins >= playerUpgrade.costToUpgrade){
            dataContainer.coins -= playerUpgrade.costToUpgrade;
            playerUpgrade.level++;
            UpdateElement();
        }
    }

    void UpdateElement(){
        PlayerUpgrade playerUpgrade = dataContainer.upgrades[(int) upgrade];

        upgradeName.text = upgrade.ToString();
        level.text = playerUpgrade.level.ToString();
        price.text = playerUpgrade.costToUpgrade.ToString();
    }

}
