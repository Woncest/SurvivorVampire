using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossTimer : MonoBehaviour
{
    TextMeshProUGUI bossTimerText;
    [SerializeField] GameObject killBossText;
    public int numberOfBosses;
    private float timer = 30;
    void Start()
    {
        bossTimerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(numberOfBosses > 0){
            timer -= Time.deltaTime;
            if(timer < 0){
                timer = 30;
                numberOfBosses--;
            }
            bossTimerText.text = "Next Boss: " + (int) (timer % 60f) + "\n Bosses Left: " + numberOfBosses;
        }else{
            bossTimerText.gameObject.SetActive(false);
            killBossText.SetActive(true);
        }
    }
}
