using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] DataContainerSO data;
    [SerializeField] TMPro.TextMeshProUGUI coinsCountText;

    private void Start(){
        coinsCountText.text = "Coins: " + data.coins;
    }

    public void AddCoin(int count){
        data.coins += count;
        coinsCountText.text = "Coins: " + data.coins;
    }
}
