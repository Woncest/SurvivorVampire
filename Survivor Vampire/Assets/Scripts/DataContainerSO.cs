using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DataContainerSO : ScriptableObject
{
    public int coins; 

    public List<bool> stageCompletion;

    public void StageCompletion(int i){
        stageCompletion[i] = true;
    }
}
