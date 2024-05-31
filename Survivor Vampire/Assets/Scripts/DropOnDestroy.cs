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
        if(toDrop.tag == "XP"){
            toDrop.GetComponent<GemPickUp>().amount = (int)(gameObject.GetComponent<Enemy>().stats.xpDropMultiplier * toDrop.GetComponent<GemPickUp>().amount);
        }
        SpawnManager.instance.SpawnObject(transform.position, toDrop);
    }
}
