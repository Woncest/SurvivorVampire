using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    PauseManager pm;

    [SerializeField] List<UpgradeButton> upgradeButtons;

    private void Awake(){
        pm = GetComponent<PauseManager>();
    }

    private void Start(){
        HideButtons();
    }

    public void OpenPanel(List<UpgradesSO> upgrades){
        Clean();
        panel.SetActive(true);
        pm.PauseGame();

        for(int i = 0; i < upgrades.Count; i++){
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgrades[i]);
        }
    }

    public void Clean(){
        foreach(UpgradeButton upgrade in upgradeButtons){
            upgrade.Clean();
        }
    }

    public void Upgrade(int PressedButtonID){
        GameManager.instance.playerTransform.GetComponent<Level>().Upgrade(PressedButtonID);
        ClosePanel();
    }

    public void ClosePanel()
    {
        HideButtons();
        panel.SetActive(false);
        pm.UnPauseGame();
    }

    private void HideButtons()
    {
        foreach (UpgradeButton upgradeButton in upgradeButtons)
        {
            upgradeButton.gameObject.SetActive(false);
        }
    }
}
