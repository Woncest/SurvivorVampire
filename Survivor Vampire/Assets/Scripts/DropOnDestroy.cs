using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] GameObject dropItemPrefab;

    bool isQuitting = false;

    private void OnApplicationQuit(){
        isQuitting = true;
    }

    private void OnDestroy(){
        if(isQuitting) {return;}
        
        Transform t = Instantiate(dropItemPrefab).transform;
        t.position = transform.position;
    }
}
