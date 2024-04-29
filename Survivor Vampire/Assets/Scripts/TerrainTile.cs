using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    [SerializeField] Vector2Int tilePositon;
    [SerializeField] List<SpawnObjects> spawnObjects;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<WorldScroll>().Add(gameObject, tilePositon);

        transform.position = new Vector3(-100,-100,0);
    }

    public void Spawn(){
        foreach(SpawnObjects spawnObject in spawnObjects){
            spawnObject.Spawn();
        }
    }
}
