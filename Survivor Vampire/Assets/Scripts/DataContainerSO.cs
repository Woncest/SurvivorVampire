using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PersistentUpgrades{
    HP,
    Damage
}

[Serializable]
public class PlayerUpgrade{
    public PersistentUpgrades persistentUpgrades;
    public int level = 0;
    public int maxLevel = 10;
    public int costToUpgrade;
}

[CreateAssetMenu]
public class DataContainerSO : ScriptableObject
{
    public int coins; 

    public List<bool> stageCompletion;

    public List<PlayerUpgrade> upgrades;

    public void StageCompletion(int i){
        stageCompletion[i] = true;
    }

    internal int GetUpgradeLevel(PersistentUpgrades upgrade)
    {
        return upgrades[(int)upgrade].level;
    }
}
