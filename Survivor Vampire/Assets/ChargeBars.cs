using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBars : MonoBehaviour
{
    [SerializeField] List<GameObject> chargeBars;

    public GameObject ActivateChargeBar(){
        foreach (var chargeBar in chargeBars){
            if(!chargeBar.activeInHierarchy){
                chargeBar.SetActive(true);
                return chargeBar;
            }
        }
        return null;
    }
}
