using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DamageMessage : MonoBehaviour
{
    [SerializeField] float TimeToLive = 1f;
    float ttl = 1f;
    public float fadeSpeed = 0.01f;
    private TextMeshPro text;

    private void OnEnable(){
        ttl = TimeToLive;
        text = GetComponent<TextMeshPro>();
    }

    private void Update(){
        ttl -= Time.deltaTime;
        if(Time.frameCount %18 == 0){
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f , transform.position.z);
            Color currentColor = text.color;
            float newAlpha = Mathf.Max(0f, currentColor.a - fadeSpeed);
            text.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        }
        if (ttl < 0){
            gameObject.SetActive(false);
            Color currentColor = text.color;
            text.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
        }
    }
}
