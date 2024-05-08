using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Image image;
    public TMPro.TextMeshProUGUI text;

    public void Set(UpgradesSO upgrade){
        image.sprite = upgrade.icon;
        text.text = upgrade.description;
    }

    internal void Clean()
    {
        image.sprite = null;
    }
}
