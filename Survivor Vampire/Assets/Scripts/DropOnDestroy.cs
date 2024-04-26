using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] GameObject dropItemPrefab;

    private void OnDestroy(){
        Transform t = Instantiate(dropItemPrefab).transform;
        t.position = transform.position;
    }
}
