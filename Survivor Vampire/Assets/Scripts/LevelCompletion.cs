using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletion : MonoBehaviour
{
    [SerializeField] float timeToCompleteLevel;

    [SerializeField] TimerUI timer;
    PauseManager pm;

    [SerializeField] GameObject levelCompletePanel;

    private void Awake(){
        pm = GetComponent<PauseManager>();
    }

    private void Update(){
        if(timer.time > timeToCompleteLevel){
            pm.PauseGame();
            levelCompletePanel.gameObject.SetActive(true);
        }
    }
}
