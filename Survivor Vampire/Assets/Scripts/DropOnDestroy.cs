using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] List<GameObject> dropItemPrefab;

    bool isQuitting = false;

    private void OnApplicationQuit(){
        isQuitting = true;
    }

    public void CheckDrop(){
        if(isQuitting) {return;}
        
        GameObject toDrop = dropItemPrefab[Random.Range(0, dropItemPrefab.Count)];
        Transform t = Instantiate(toDrop).transform;
        t.position = transform.position;
    }
}
