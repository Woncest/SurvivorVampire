using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    TextMeshProUGUI text;

    public float time;

    private void Awake(){
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update(){
        //a bit meh counting where you print the text but, had problems counting inside the map Scene
        time += Time.deltaTime;
        int minutes = (int) (time / 60f);
        int seconds = (int) (time % 60f);

        text.text = minutes.ToString() + ":" + seconds.ToString("00");
    }
}
