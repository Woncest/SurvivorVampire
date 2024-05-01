using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    [SerializeField] GameObject weapons;

    public void GameOver(){
        GetComponent<PlayerMove>().enabled = false;
        weapons.SetActive(false);
        gameOverPanel.SetActive(true);
    }
}
