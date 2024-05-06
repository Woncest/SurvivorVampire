using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StageTimer : MonoBehaviour
{
    public float time { get; private set; }

    private void Update(){
        time += Time.deltaTime;
    }
}
