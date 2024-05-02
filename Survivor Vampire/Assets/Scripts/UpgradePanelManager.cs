using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    PauseManager pm;

    private void Awake(){
        pm = GetComponent<PauseManager>();
    }

    public void OpenPanel(){
        panel.SetActive(true);
        pm.PauseGame();
    }

    public void ClosePanel(){
        panel.SetActive(false);
        pm.UnPauseGame();
    }
}
