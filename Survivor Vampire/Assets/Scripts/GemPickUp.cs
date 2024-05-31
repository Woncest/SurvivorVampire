using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickUp : MonoBehaviour, IPickUp
{
    public int amount;
    public void OnPickUp(Character character){
        character.level.AddExperience(amount);
    }
}
