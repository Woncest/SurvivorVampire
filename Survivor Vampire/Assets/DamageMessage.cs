using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DamageMessage : MonoBehaviour
{
    [SerializeField] float TimeToLive = 1f;
    float ttl = 1f;

    private void OnEnable(){
        ttl = TimeToLive;
    }

    private void Update(){
        ttl -= Time.deltaTime;
        if (ttl < 0){
            gameObject.SetActive(false);
        }
    }
}
