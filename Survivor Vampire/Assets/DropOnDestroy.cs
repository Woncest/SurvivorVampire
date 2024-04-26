using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] GameObject healthPickup;

    private void OnDestroy(){
        Transform t = Instantiate(healthPickup).transform;
        t.position = transform.position;
    }
}
