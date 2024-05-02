using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Image image;

    public void Set(UpgradesSO upgrade){
        image.sprite = upgrade.icon;
    }

    internal void Clean()
    {
        image.sprite = null;
    }
}
