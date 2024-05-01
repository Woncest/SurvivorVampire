using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject panel;
    private PauseManager pm;

    private void Awake(){
        pm = GetComponent<PauseManager>();
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(panel.activeInHierarchy == false){
                OpenMenu();
                pm.PauseGame();
            }else{
                CloseMenu();
                pm.UnPauseGame();
            }
        }
    }

    public void CloseMenu(){
        panel.SetActive(false);
    }

    public void OpenMenu(){
        panel.SetActive(true);
    }
}
