using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickUp : MonoBehaviour, IPickUp
{
    [SerializeField] int amount;
    public void OnPickUp(Character character){
        character.level.AddExperience(amount);
    }
}
