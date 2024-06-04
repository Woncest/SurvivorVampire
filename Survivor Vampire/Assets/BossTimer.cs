using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class BossTimer : MonoBehaviour
{
    TextMeshProUGUI bossTimerText;
    [SerializeField] GameObject killBossText;
    public int numberOfBosses;
    [SerializeField] float timeBetweenBosses = 60;
    private float time = 0f;
    void Start()
    {
        bossTimerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(numberOfBosses > 0){
            time -= Time.deltaTime;
            if(time < 0){
                time = timeBetweenBosses;
                numberOfBosses--;
            }
            bossTimerText.text = "Next Boss: " + (int) (time % 60f) + "\n Bosses Left: " + numberOfBosses;
        }else{
            bossTimerText.gameObject.SetActive(false);
            killBossText.SetActive(true);
        }
    }
}
