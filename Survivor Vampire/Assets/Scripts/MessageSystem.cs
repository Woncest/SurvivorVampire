using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem instance;

    private void Awake(){
        instance = this;
    }

    [SerializeField] GameObject damageMessage;

    List<TMPro.TextMeshPro> messagePool;

    int objectCount = 50;
    int count;

    private void Start(){
        messagePool = new List<TMPro.TextMeshPro>();

        for (int i = 0; i < objectCount; i++){
            Populate();
        }
    }

    public void Populate(){
        GameObject go = Instantiate(damageMessage, transform);
        messagePool.Add(go.GetComponent<TMPro.TextMeshPro>());
        go.SetActive(false);
    }

    public void PostMessage(string text, Vector3 worldPositon){
        messagePool[count].gameObject.SetActive(true);
        messagePool[count].transform.position = worldPositon;
        messagePool[count].GetComponent<TMPro.TextMeshPro>().text = text;
        count++;
        if(count >= objectCount){
            count = 0;
        }
    }
}
