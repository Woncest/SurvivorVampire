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
        GameObject drop = SpawnManager.instance.SpawnObject(transform.position, toDrop);
        if(drop.tag == "XP"){
            drop.GetComponent<GemPickUp>().amount = (int)(gameObject.GetComponent<Enemy>().stats.xpDropMultiplier * drop.GetComponent<GemPickUp>().amount);
        }
    }
}
