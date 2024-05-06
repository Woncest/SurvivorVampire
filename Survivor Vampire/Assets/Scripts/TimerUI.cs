using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    TextMeshProUGUI text;
    StageTimer stageTimer;

    private void Awake(){
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start(){
        stageTimer = FindObjectOfType<StageTimer>();
    }

    private void Update(){
        int minutes = (int) (stageTimer.time / 60f);
        int seconds = (int) (stageTimer.time % 60f);

        text.text = minutes.ToString() + ":" + seconds.ToString("00");
    }
}
