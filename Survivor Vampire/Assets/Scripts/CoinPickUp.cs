using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour, IPickUp
{
    [SerializeField] int amount;
    public void OnPickUp(Character character)
    {
        character.coins.AddCoin(amount);
    }
}
