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

    public void CheckDrop(){
        if(isQuitting) {return;}
        
        Transform t = Instantiate(dropItemPrefab).transform;
        t.position = transform.position;
    }
}
