using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemyAnimation;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnTimer;
    GameObject target;
    float timer;

    private void Start(){
        target = GameManager.instance.playerTransform.gameObject;
    }

    private void Update(){
        timer -= Time.deltaTime;
        if (timer < 0){
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        Vector3 positon = GenerateRandomPosition();

        positon += target.transform.position;

        //spawning main object of enemy
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = positon;
        newEnemy.GetComponent<Enemy>().SetTarget(target);
        newEnemy.transform.parent = transform;
        timer = spawnTimer;

        //spawning sprite object of enemy
        GameObject spriteObject = Instantiate(enemyAnimation);
        spriteObject.transform.parent = newEnemy.transform;
        spriteObject.transform.localPosition = Vector3.zero;
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = new Vector3();

        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;
        if(UnityEngine.Random.value > 0.5f){
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }else{
            position.x = spawnArea.x * f;
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
        }

        return position;
    }
}
