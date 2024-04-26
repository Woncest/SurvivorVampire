using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Transform bar;

    public void SetState(float current, float max){
        current /= max;
        bar.transform.localScale = new Vector3(current, 1f, 1f);
    }
}
